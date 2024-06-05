using System.Net;
using Entity.DTO;
using Entity.DTO.Login;
using Entity.DTO.Patient;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace API.Controllers;

[ApiController]
[Route("api/Patient")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly ICommunicationService _communicationService;
    private APIResponse _response;
    public PatientController(IPatientService patientService, ICommunicationService communicationService)
    {
        _patientService = patientService;
        _communicationService = communicationService;
        _response = new();
    }

    //this link will be work for the limited time with token.
    //on page hit check token from table wether that is valid period for time or not!
    [HttpPost("CreateAccount")]
    public async Task<ActionResult<APIResponse>> CreateAccount(LoginDTO loginDetails)
    {
        try
        {
            await _patientService.CreateAccountPatient(loginDetails);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            // _response.Result = loginResponse;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.Forbidden;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
    }

    [HttpPost("ForgotPassword")]
    public async Task<ActionResult<APIResponse>> ForgotPassword(string Email)
    {
        try
        {
            ResetPasswordVM resetPassword = await _patientService.ForgotPassword(Email);
            if (!resetPassword.isExist)
            {
                _response.HttpStatusCode = HttpStatusCode.NotFound;
                _response.Error = "User does Not Exists!";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.Result = "Successfull!";
            _response.IsSuccess = false;
            // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "");
            string resetLink = "reset password link";
            string Body = $"You can reset your password by {resetLink}";
            string Subject = "Reset Your Password";
            // id will be 0 for Patient
            //might not need to add this entry in email log
            await _communicationService.GenericSendEmail(Email,Body,Subject,resetPassword.RoleId,resetPassword.id,1,"");
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.Forbidden;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
    }

    //here by srs create account and reset password will be same by logic.
    //on successfull just redirect to login!
    [HttpPost("ResetPassword")]
    public async Task<ActionResult<APIResponse>> ResetPassword(LoginDTO loginDetails)
    {
        try
        {
            await _patientService.CreateAccountPatient(loginDetails);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = "Password Reset Successfull!";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.Forbidden;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
    }

    [HttpGet("GetRegionList")]
    public async Task<ActionResult<APIResponse>> getAllRegionList()
    {
        try
        {
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = false;
            _response.Result = await _patientService.getAllRegionList();
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return BadRequest(_response);
        }
    }

    //patient request make the seperate validator for it. if user already a patient or not.
    [HttpPost("PatientRequest")]
    public async Task<ActionResult<APIResponse>> CreateRequestPatient(PatientDetails requestData)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = await _patientService.CreateRequest(requestData);
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return BadRequest(_response);
                }

                // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "")
                string Body = $"{requestData.Email} Your Request Has been Successfully Created! ";
                string Subject = "";
                await _communicationService.GenericSendEmail(requestData.Email, Body, Subject, 3, requestId, 0, "");

                return Ok(_response);
            }
            else
            {
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Error = "Model State Invalid!";
                return BadRequest(_response);
            }
        }
        catch (BadHttpRequestException ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return BadRequest(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return BadRequest(_response);
        }
    }

    [HttpPost("FamilyRequest")]
    public async Task<ActionResult<APIResponse>> OtherRequest(OtherRequest requestData)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = 0;
                bool isExists = false;
                (requestId, isExists) = await _patientService.OtherRequests(requestData);
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return BadRequest(_response);
                }

                // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "")
                string Body = "";
                string Subject = "";
                if (!isExists)
                {
                    Body = $"You can Create Your Account By .Your Request Has been Successfully Created! by {requestData.YFirstName} ";
                    Subject = "Create Account";
                }
                else
                {
                    Body = $"Your Request Has been Successfully Created! by {requestData.YFirstName} ";
                    Subject = "New Request";
                }

                await _communicationService.GenericSendEmail(requestData.Email, Body, Subject, 3, requestId, 0, "");

                return Ok(_response);
            }
            else
            {
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Error = "Model State Invalid!";
                return BadRequest(_response);
            }
        }
        catch (BadHttpRequestException ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return BadRequest(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return BadRequest(_response);
        }
    }

}

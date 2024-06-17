using System.Net;
using AutoMapper;
using Entity.DTO;
using Entity.DTO.Login;
using Entity.DTO.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IService;
using API.CustomAuthorizeMiddleware;
using System.IO.Compression;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Controllers;

[ApiController]
[Route("api/Patient")]


public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly ICommunicationService _communicationService;
    private readonly IAuthService _authService;
    private APIResponse _response;
    private readonly IMapper _mapper;
    public PatientController(IPatientService patientService, ICommunicationService communicationService, IMapper mapper, IAuthService authService)
    {
        _patientService = patientService;
        _communicationService = communicationService;
        _authService = authService;
        _mapper = mapper;
        _response = new();
    }

    [HttpPost("ValidateEmail")]
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> ValidateEmail(ValidateEmailDTO validateEmail)
    {
        try
        {
            LoginDTO loginDetails = new()
            {
                Email = validateEmail.email,
                Password = ""
            };
            LoginUserStatus userStatus = await _authService.IsUserExists(loginDetails);

            //is User Exist or not
            if (userStatus == null)
            {
                _response.HttpStatusCode = HttpStatusCode.NotFound;
                _response.Error = "User Does not Exist!";
                _response.IsSuccess = false;
                // return NotFound(_response);
                return Ok(_response);
            }

            LoginUserDTO userEmailRole = new()
            {
                Email = loginDetails.Email,
                Role = userStatus.Role,
            };

            LoginResponseDTO loginResponse = new()
            {
                Email = loginDetails.Email,
                token = "",
                Role = userStatus.Role
            };
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.Forbidden;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return Ok(_response);
        }
    }

    // #frontend #backend things which are left behind or future purpose

    //this link will be work for the limited time with token.
    //on page hit check token from table wether that is valid period for time or not!
    [HttpPost("CreateAccount")]
    [EnableCors("corsPolicy")]
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
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> ForgotPassword(ForgetPasswordDTO forgetPasswordDTO)
    {
        try
        {
            ResetPasswordVM resetPassword = await _patientService.ForgotPassword(forgetPasswordDTO.Email);
            if (!resetPassword.isExist || (resetPassword.isExist && resetPassword.RoleId != 3))
            {
                _response.HttpStatusCode = HttpStatusCode.NotFound;
                _response.Error = "User does Not Exists!";
                _response.IsSuccess = false;
                return Ok(_response);
            }
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.Result = "Successfull!";
            _response.IsSuccess = true;

            // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "");
            // string resetLink = "http://localhost:4200/patient/resetpassword";
            string resetLink = $"http://localhost:4200/patient/resetpassword?token={resetPassword.token}";
            string Body = $"You can reset your password by {resetLink}";
            string Subject = "Reset Your Password";

            // id will be 0 for Patient
            //might not need to add this entry in email log
            await _communicationService.GenericSendEmail(forgetPasswordDTO.Email, Body, Subject, resetPassword.RoleId, resetPassword.id, 1, "");
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return Ok(_response);
        }
    }

    //here by srs create account and reset password will be same by logic.
    //on successfull just redirect to login!
    [HttpGet("ResetPassword")]
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> ResetPassword(string token)
    {
        try
        {
            resetPasswordGetDTO resetPasswordGetDTO = await _patientService.GetResetPasswordData(token);
            if (resetPasswordGetDTO == null)
            {
                _response.HttpStatusCode = HttpStatusCode.NotFound;
                _response.Error = "User does not Exists!";
                _response.IsSuccess = false;
                return Ok(_response);
            } 
            else if(resetPasswordGetDTO!=null && !resetPasswordGetDTO.isValidated){
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                _response.Error = "Token time Expired!";
                _response.IsSuccess = false;
                return Ok(_response);
            }
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = resetPasswordGetDTO;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return Ok(_response);
        }
    }
    
    //here by srs create account and reset password will be same by logic.
    //on successfull just redirect to login!
    [HttpPost("ResetPassword")]
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> ResetPassword(LoginDTO loginDetails)
    {
        try
        {
            //check token valid for only 24 hours, doubt! but if he do not create at that time then request?
            //just simply resetting/Creating the password even if 24 validity is expired, he can reset password. by request!
            // for create account will be the same logic. because request is already registered for new Patient. so aspnetuser,user,request,requestclient
            await _patientService.CreateAccountPatient(loginDetails);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = "Password Reset Successfull!";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.Error = ex.ToString();
            _response.IsSuccess = false;
            return Ok(_response);
        }
    }

    //this list will be managed at front end for the list binding to specific model.
    // #fronend
    [HttpGet("GetRegionList")]
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> getAllRegionList()
    {
        try
        {
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = await _patientService.getAllRegionList();
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return Ok(_response);
        }
    }

    //patient request make the seperate validator for it. if user already a patient or not.
    [EnableCors("corsPolicy")]
    [HttpPost("PatientRequest")]
    public async Task<ActionResult<APIResponse>> CreateRequestPatient([FromBody] PatientDetails requestData)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = await _patientService.CreateRequest(requestData);
                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return Ok(_response);
                }
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

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
                return Ok(_response);
            }
        }
        catch (BadHttpRequestException ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.HttpStatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Error = ex.ToString();
            return Ok(_response);
        }
    }

    [EnableCors("corsPolicy")]
    [HttpPost("OtherRequest")]
    public async Task<ActionResult<APIResponse>> OtherRequest([FromBody] OtherRequest requestData)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = 0;
                bool isExists = false;
                (requestId, isExists) = await _patientService.OtherRequests(requestData);
                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return BadRequest(_response);
                }
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

                // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "")
                string Body = $"";
                string Subject = "";
                if (!isExists)
                {
                    ResetPasswordVM resetPassword = await _patientService.ForgotPassword(requestData.Email);
                    string createLink = $"http://localhost:4200/patient/createaccount?token={resetPassword.token}"; //here will be create account page Url
                    Body = $"You can Create Your Account By {createLink} .Your Request Has been Successfully Created! by {requestData.YFirstName} ";
                    Subject = "Create Account";
                    await _communicationService.GenericSendEmail(requestData.Email, Body, Subject, 3, 0, 1, "");
                }
                else
                {
                    Body = $"Your Request Has been Successfully Created! by {requestData.YFirstName} ";
                    Subject = "New Request";
                    await _communicationService.GenericSendEmail(requestData.Email, Body, Subject, 3, requestId, 0, "");
                }
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

    //Patient Dashboard
    [CustomAuthorize("3")]
    [HttpGet(Name = "GetPatientProfile")]
    public async Task<ActionResult<APIResponse>> GetPatientProfile(string userEmail)
    {
        try
        {
            //user definetely exists so not checking nullable
            PatientProfile patientDetails = await _patientService.GetPatientProfile(userEmail);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.Result = patientDetails;
            _response.IsSuccess = true;
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

    [CustomAuthorize("3")]
    [HttpPut("EditPatientProfile")]
    public async Task<ActionResult<APIResponse>> EditPatientProfile(PatientProfile patientDetails)
    {
        try
        {
            //user definetely exists so not checking nullable
            // PatientProfile patientDetails = await _patientService.GetPatientProfile(userEmail); 
            await _patientService.UpdatePatientProfile(patientDetails);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.Result = "Profile Updated";
            _response.IsSuccess = true;
            return Ok(_response);
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

    [CustomAuthorize("3")]
    [HttpGet("PatientDashboard")]
    public async Task<ActionResult<APIResponse>> PatientDashboard(string email)
    {
        try
        {
            //service to get dashboard content
            PatientDashboard patientDashboardData = await _patientService.GetDashboardContent(email);
            if (patientDashboardData == null)
            {
                _response.HttpStatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = "No Data Found";
                return Ok(_response);
            }
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = patientDashboardData.dashboardContent;
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

    [CustomAuthorize("3")]
    [HttpGet("SingleRequestView")]
    public async Task<ActionResult<APIResponse>> SingleRequestView(int reqeustId)
    {
        try
        {
            //service to get dashboard content
            SingleRequest singleRequestDetails = await _patientService.GetSingleRequestDetails(reqeustId);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = singleRequestDetails;
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

    // [CustomAuthorize("3")]
    [HttpPost("UploadDocument")]
    public async Task<ActionResult<APIResponse>> UploadDocument(UploadDocument documentData)
    {
        try
        {
            //service to upload Documents
            await _patientService.UploadDocuments(documentData);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = "uploaded Successfully";
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

    // [CustomAuthorize("3")]
    [HttpPost("DownloadDocuments")]
    public async Task<ActionResult<APIResponse>> DownloadDocuments(DownloadRWF downloadRWF)
    {
        try
        {
            //login to download document it will be done after frontend

            var zipMemoryStream = new MemoryStream();
            DownloadRWFResponse downloadRWFResponse = await _patientService.DownloadDocuments(downloadRWF);

            using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in downloadRWFResponse.RequestWiseFileList)
                {
                    string filePath = file.Replace("/", "\\");
                    string[] fileString = file.Split("/").ToArray();
                    string fileName = fileString[fileString.Length - 1];
                    var entry = zipArchive.CreateEntry(fileName);
                    using (var entryStream = entry.Open())
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(entryStream);
                    }
                }
            }
            zipMemoryStream.Seek(0, SeekOrigin.Begin);
            return File(zipMemoryStream.ToArray(), "application/zip", "DownloadedFiles.zip");

            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = "uploaded Successfully";
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

    // it will take user email and will get data from userTable
    [CustomAuthorize("3")]
    [HttpGet("ForMeRequestGetData")]
    public async Task<ActionResult<APIResponse>> ForMeRequestGetData(string userEmail)
    {
        try
        {
            //Email will must be there so no nullable checks!!!
            PatientDetails oldPatientDetails = await _patientService.GetForMePatientRequestData(userEmail);
            _response.HttpStatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = oldPatientDetails;
            return Ok(_response);
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

    [CustomAuthorize("3")]
    [HttpPost("ForMeRequest")]
    public async Task<ActionResult<APIResponse>> ForMeRequest(PatientDetails requestData)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = await _patientService.CreateRequest(requestData);
                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return BadRequest(_response);
                }
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

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

    //For somoneElse Request
    //Requestor will be always type of and Relation family/friend and the relation and requested by will be get by email stored in session at frontend!
    //YFirstName required Field, also requestType = 2 will be also assigned at front end
    //there will be getAllregion controller will be called to get the region list #frontend
    [CustomAuthorize("3")]
    [HttpPost]
    public async Task<ActionResult<APIResponse>> ForSomeOneElseRequest(OtherRequest someOneElseRequestDetails)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int requestId = 0;
                bool isExists = false;
                (requestId, isExists) = await _patientService.OtherRequests(someOneElseRequestDetails);
                if (requestId == 0)
                {
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Error = "Something Went Wrong!";
                    return BadRequest(_response);
                }
                _response.HttpStatusCode = HttpStatusCode.Created;
                _response.Result = "Reqeust Created SuccessFully";
                _response.IsSuccess = true;

                // GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "")
                string createLink = ""; //here will be create account page Url
                string Body = $"";
                string Subject = "";
                if (!isExists)
                {
                    Body = $"You can Create Your Account By .Your Request Has been Successfully Created! by {someOneElseRequestDetails.YFirstName} ";
                    Subject = "Create Account";
                    await _communicationService.GenericSendEmail(someOneElseRequestDetails.Email, Body, Subject, 3, 0, 1, "");
                }
                else
                {
                    Body = $"Your Request Has been Successfully Created! by {someOneElseRequestDetails.YFirstName} ";
                    Subject = "New Request";
                    await _communicationService.GenericSendEmail(someOneElseRequestDetails.Email, Body, Subject, 3, requestId, 0, "");
                }
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
    //Review Agreement

}

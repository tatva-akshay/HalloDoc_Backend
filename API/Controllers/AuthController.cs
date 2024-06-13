using System.Net;
using Entity.DTO;
using Entity.DTO.Login;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace API.Controllers;

[ApiController]
[Route("api/Login")]
public class AddController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IAuthService _authService;
    private APIResponse _response;
    public AddController(IPatientService patientService, IAuthService authService)
    {
        _patientService = patientService;
        _authService = authService;
        _response = new();
    }

    [HttpPost]
    [EnableCors("corsPolicy")]
    public async Task<ActionResult<APIResponse>> Login(LoginDTO loginDetails)
    {
        try
        {
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
            if (!userStatus.IsUserCreated)
            {
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                _response.Error = "Create/Reset Your Password!";
                _response.IsSuccess = false;
                // return BadRequest(_response);
                return Ok(_response);
            }
            if (!userStatus.PasswordMatched)
            {
                _response.HttpStatusCode = HttpStatusCode.Forbidden;
                _response.Error = "Invalid Password!";
                _response.IsSuccess = false;
                // return BadRequest(_response);
                return Ok(_response);
            }

            LoginUserDTO userEmailRole = new()
            {
                Email = loginDetails.Email,
                Role = userStatus.Role,
            };

            string token = await _authService.GenerateToken(userEmailRole);
            LoginResponseDTO loginResponse = new()
            {
                Email = loginDetails.Email,
                token = token,
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

}

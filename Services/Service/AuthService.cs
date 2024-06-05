using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity.DTO.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;
using Repository.Repository;
using Services.IService;

namespace Services.Service;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;
    public AuthService(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    public async Task<LoginUserStatus> IsUserExists(LoginDTO loginDetails){
        LoginUserStatus userStatus = await _authRepository.IsUserExists(loginDetails.Email);
        if(userStatus == null){
            return userStatus;
        }
        if(userStatus.Password!=null && BCrypt.Net.BCrypt.Verify(loginDetails.Password,userStatus.Password)){
            userStatus.PasswordMatched = true;
        } 
        else if(userStatus.Password == null){
            userStatus.PasswordMatched = false;
            userStatus.IsUserCreated = false;
        }
        userStatus.IsExists = true;
        return userStatus;        
    }

    public async Task<string> GenerateToken(LoginUserDTO LoginDetails)
    {
        var JwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secretkey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
                new(ClaimTypes.Email,LoginDetails.Email),
                new(ClaimTypes.Role,LoginDetails.Role),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = JwtTokenHandler.CreateToken(tokenDescriptor);
        return JwtTokenHandler.WriteToken(token);
    } 

}

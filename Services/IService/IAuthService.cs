using Entity.DTO.Login;

namespace Services.IService;

public interface IAuthService
{
    Task<string> GenerateToken(LoginUserDTO LoginDetails);
    Task<LoginUserStatus> IsUserExists(LoginDTO loginDetails);
}

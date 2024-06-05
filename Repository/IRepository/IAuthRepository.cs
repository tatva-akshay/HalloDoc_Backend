using Entity.DataContext;
using Entity.DTO.Login;

namespace Repository.IRepository;

public interface IAuthRepository
{
    Task<LoginUserStatus> IsUserExists(string Email);

}

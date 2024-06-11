using Repository.IRepository;
using Services.IService;

namespace Services.Service;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IAuthRepository _authRepository;

    public AdminService(IAdminRepository adminRepository, ITableRepository tableRepository, IAuthRepository authRepository)
    {
        _adminRepository = adminRepository;
        _tableRepository = tableRepository;
        _authRepository = authRepository;
    }
    

}

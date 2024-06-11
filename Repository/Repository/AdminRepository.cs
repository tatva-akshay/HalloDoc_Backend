using Entity.DataContext;
using Repository.IRepository;

namespace Repository.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ITableRepository _tableRepository;
    public AdminRepository(ApplicationDbContext context, ITableRepository tableRepository)
    {
        _context = context;
        _tableRepository = tableRepository;
    }

}

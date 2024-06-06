using Entity.DataContext;
using Entity.DTO.General;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository;

public class TableRepository : ITableRepository
{
    private readonly ApplicationDbContext _context;
    public TableRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task AddAspNetUser(AspNetUser newAspNetUser)
    {
        await _context.AspNetUsers.AddAsync(newAspNetUser);
    }

    public async Task AddAspNetUserRole(AspNetUserRole newAspNetUserRole)
    {
        await _context.AspNetUserRoles.AddAsync(newAspNetUserRole);
    }

    public async Task AddRequest(Request newRequest)
    {
        await _context.Requests.AddAsync(newRequest);
    }

    public async Task AddRequestClient(RequestClient newRequestClient)
    {
        await _context.RequestClients.AddAsync(newRequestClient);
    }

    public async Task AddUserTable(User newUser)
    {
        await _context.Users.AddAsync(newUser);
    }

    public async Task AddRequestWiseFile(RequestWiseFile newRequestWiseFile)
    {
        await _context.RequestWiseFiles.AddAsync(newRequestWiseFile);
    }

    public async Task AddEmailLog(EmailLog newEmailLog){
        await _context.EmailLogs.AddAsync(newEmailLog);

    }
    public async Task AddSMSLog(Smslog newSMSLog){
        await _context.Smslogs.AddAsync(newSMSLog);
    }

    public async Task AddPasswordReset(PasswordReset newPasswordReset){
        await _context.PasswordResets.AddAsync(newPasswordReset);
    }


    // Get Methods
    public async Task<List<Region>> getAllRegionList()
    {
        return await _context.Regions.ToListAsync();
    }

    public async Task<List<RegionsDropDown>> GetRegionsDropDowns(){
        return await _context.Regions.Select(a=>new RegionsDropDown(){
            RegionId = a.RegionId,
            Name = a.Name
        }).ToListAsync();
    }   

}

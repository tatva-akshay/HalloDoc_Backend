using Entity.DTO.General;
using Entity.Models;

namespace Repository.IRepository;

public interface ITableRepository
{
    Task SaveChanges();
    Task AddAspNetUser(AspNetUser newAspNetUser);
    Task AddAspNetUserRole(AspNetUserRole newAspNetUserRole);
    Task AddRequest(Request newRequest);
    Task AddRequestClient(RequestClient newRequestClient);
    Task AddUserTable(User newUser);
    Task AddRequestWiseFile(RequestWiseFile newRequestWiseFile);
    Task AddEmailLog(EmailLog newEmailLog);
    Task AddSMSLog(Smslog newSMSLog);
    Task AddPasswordReset(PasswordReset newPasswordReset);

    // Get Methods
    Task<List<RegionsDropDown>> getAllRegionList(); 
    Task<List<RegionsDropDown>> GetRegionsDropDowns();
}

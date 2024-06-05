using Entity.Models;

namespace Repository.IRepository;

public interface ICommunicationRepository
{
    Task<int> CreateEmailLog(EmailLog newEmailLog);
    Task<int> CreateSMSLog(Smslog newSMSLog);
}

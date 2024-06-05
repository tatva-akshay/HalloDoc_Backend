using Entity.DataContext;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Repository.IRepository;

namespace Repository.Repository;

public class CommunicationRepository : ICommunicationRepository
{
     private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public CommunicationRepository(ApplicationDbContext context, IConfiguration configuration){
        _context = context;
        _configuration = configuration;
    }

    public async Task<int> CreateEmailLog(EmailLog newEmailLog)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                if (newEmailLog.EmailLogId == 0)
                {
                    //For patient email requestId will be present so no need to change it;
                    switch (newEmailLog.RoleId)
                    {
                        case 1: newEmailLog.AdminId = newEmailLog.RequestId; newEmailLog.RequestId = null; break;
                        case 2: newEmailLog.PhysicianId = newEmailLog.RequestId; newEmailLog.RequestId = null; break;
                    }
                    newEmailLog.CreateDate = DateTime.Now;
                    newEmailLog.IsEmailSent = false;
                    newEmailLog.Action = 1;
                    //Action ???
                    await context.EmailLogs.AddAsync(newEmailLog);
                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                    return newEmailLog.EmailLogId;
                }
                else
                {
                    EmailLog updateEmaillog = context.EmailLogs.FirstOrDefault(a => a.EmailLogId == newEmailLog.EmailLogId);
                    updateEmaillog.SentDate = DateTime.Now;
                    updateEmaillog.SentTries = newEmailLog.SentTries;
                    updateEmaillog.IsEmailSent = false;
                    updateEmaillog.Action = 1;
                    //Action ???
                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                    return updateEmaillog.EmailLogId;
                }
            }
        }

        //Create SMSLog
        public async Task<int> CreateSMSLog(Smslog newSMSLog)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                if (newSMSLog.SmslogId == 0)
                {
                    //For patient email requestId will be present so no need to change it;
                    switch (newSMSLog.RoleId)
                    {
                        case 1: newSMSLog.AdminId = newSMSLog.RequestId; newSMSLog.RequestId = null; break;
                        case 2: newSMSLog.PhysicianId = newSMSLog.RequestId; newSMSLog.RequestId = null; break;
                    }
                    newSMSLog.CreateDate = DateTime.Now;
                    newSMSLog.IsSmssent = false;
                    newSMSLog.Action = 1;
                    //Action ???
                    await context.Smslogs.AddAsync(newSMSLog);
                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                    return (int)newSMSLog.SmslogId;
                }
                else
                {
                    Smslog updateSMSlog = context.Smslogs.FirstOrDefault(a => a.SmslogId == newSMSLog.SmslogId);
                    updateSMSlog.SentDate = DateTime.Now;
                    updateSMSlog.SentTries = newSMSLog.SentTries;
                    updateSMSlog.IsSmssent =false;
                    updateSMSlog.Action = 1;
                    //Action ???
                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                    return (int)newSMSLog.SmslogId;
                }
            }
        }

}

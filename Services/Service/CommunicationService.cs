using System.Net;
using System.Net.Mail;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Repository.IRepository;
using Repository.Repository;
using Services.IService;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services.Service;

public class CommunicationService : ICommunicationService
{
    private readonly IConfiguration _configuration;
    private readonly ICommunicationRepository _communicationRepository;
    public CommunicationService( IConfiguration configuration, ICommunicationRepository communicationRepository)
    {
        _configuration = configuration;
        _communicationRepository = communicationRepository;
    }

    public async Task GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "")
    {
        string senderEmail = "tatva.dotnet.dhruvrpatel@outlook.com";
        string senderPassword = "Dhruv@123";

        //string resetLink = $"{Request.Scheme}://{Request.Host}/Home/Resetpassword?token={Token}";
        //string resetLink = $"{Scheme}://{Host}/Admin/AdminResetPassword?token={Token}";

        if (isPassReset == 1)
        {
            string Token = Guid.NewGuid().ToString();
            // await _adminRepository.ResetPasswordToken(ToEmail, Token);
        }

        //To create New Email Log, assigning requestid
        EmailLog newEmaillog = new()
        {
            EmailId = ToEmail,
            EmailTemplate = Body,
            SubjectName = Subject,
            RoleId = RoleId,
            //RequestId = id
        };
        // Send Link Email RequestId?
        if (id != 0 && isPassReset != 1)
        {
            newEmaillog.RequestId = id;
        }
        string newDocuments = "";
       
        SmtpClient client = new SmtpClient("smtp.office365.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(senderEmail, senderPassword),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };

        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail, "HalloDoc"),
            Subject = Subject,
            IsBodyHtml = true,
            Body = Body
        };

        mailMessage.To.Add(ToEmail);

        if (Documents.Length > 0)
        {
            var fileArray = Documents.Split(',')
                   .Select(x => x.Trim())
                   .ToArray();
            foreach (var filePath in fileArray)
            {
                // Attach each file
                newDocuments += filePath;
                newDocuments += ",";
                mailMessage.Attachments.Add(new Attachment(filePath));
            }
            newEmaillog.FilePath = newDocuments;
        }

        int EmailLogId = await _communicationRepository.CreateEmailLog(newEmaillog);
        newEmaillog.EmailLogId = EmailLogId;

        bool isSent = false;
        int maxTries = 3;
        int TryCount = 1;
        while (!isSent && TryCount < maxTries)
        {
            try
            {
                await client.SendMailAsync(mailMessage);
                isSent = true;
                //to Update the SentDate
                newEmaillog.SentTries = TryCount;
                int Ok = await _communicationRepository.CreateEmailLog(newEmaillog);
            }
            catch (Exception ex)
            {
                TryCount++;
            }
        }
    }
    public async Task GenericSendSMS(string ToMobile, string Body, int RoleId, int id, int isPassReset)
    {
        //string resetLink = $"{Request.Scheme}://{Request.Host}/Home/Resetpassword?token={Token}";
        //string resetLink = $"{Scheme}://{Host}/Admin/AdminResetPassword?token={Token}";

        if (isPassReset == 1)
        {
            string Token = Guid.NewGuid().ToString();
            // await _communicationRepository.ResetPasswordToken(ToMobile, Token);
        }

        //To create New SMS Log, assigning requestid 
        Smslog newSMSLog = new()
        {
            Smstemplate = Body,
            RoleId = RoleId,
            CreateDate = DateOnly.FromDateTime(DateTime.Now),
            MobileNumber = ToMobile,
        };

        // Send SMS RequestId?
        if (id != 0)
        {
            newSMSLog.RequestId = id;
        }

        int SMSLogId = await _communicationRepository.CreateSMSLog(newSMSLog);
        newSMSLog.SmslogId = SMSLogId;

        var accountSid = _configuration["SMS:accountSid"];
        var authToken = _configuration["SMS:authToken"];
        TwilioClient.Init(accountSid, authToken);

        //var messageOptions = new CreateMessageOptions(
        //  new PhoneNumber("+919327082280")
        //);
        var messageOptions = new CreateMessageOptions(
         new PhoneNumber("+91" + ToMobile)
       );

        messageOptions.From = new PhoneNumber("+15713503421");
        messageOptions.Body = Body;

        bool isSent = false;
        int maxTries = 3;
        int TryCount = 1;
        while (!isSent && TryCount < maxTries)
        {
            try
            {
                var message = MessageResource.Create(messageOptions);
                isSent = true;
                //to Update the SentDate
                newSMSLog.SentTries = TryCount;
                int Ok = await _communicationRepository.CreateSMSLog(newSMSLog);
            }
            catch (Exception ex)
            {
                TryCount++;
            }
        }
    }
}

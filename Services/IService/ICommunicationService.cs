namespace Services.IService;

public interface ICommunicationService
{
    Task GenericSendEmail(string ToEmail, string Body, string Subject, int RoleId, int id, int isPassReset, string Documents = "");
    Task GenericSendSMS(string ToMobile, string Body, int RoleId, int id, int isPassReset);
}

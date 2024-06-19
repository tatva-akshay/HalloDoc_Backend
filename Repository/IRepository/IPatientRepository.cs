using Entity.DTO.Patient;
using Entity.Models;
using Microsoft.AspNetCore.Http;

namespace Repository.IRepository;

public interface IPatientRepository
{
    Task<int> GetNumbferOfRequestOnTheDay(DateTime Date);
    Task<VerifyPatientRole> VerifyRoleIfPatient(string emailId);
    Task<string> GetStateName(int regionId);
    Task<User> GetUserByEmail(string userEmail);
    Task<User> GetUserByUserId(int userId);
    Task<AspNetUser> GetAspNetUserByEmail(string userEmail);
    Task<Admin> GetAdminByEmail(string emailId);
    Task<Physician> GetPhysicianByEmail(string emailId);
    Task<PasswordReset> GetPasswordResetByEmail(string userEmail);
    Task<PasswordReset> GetPasswordResetByToken(string token);
    Task<PatientDashboard> GetDashboardContent(int userId);
    Task<SingleRequest> GetSingleRequestDetails(int requestId);
    Task<DownloadRWFResponse> DownloadRequestWiseFileDocuments(DownloadRWF downloadRWF);
    Task<Request> GetRequestByRequestId(int requestId);
    Task<RequestWiseFile> GetRequestWiseFileById(int requestWiseFileId);
    Task deleteDocument(int requestWiseFileId);
    // Task<PatientDetails> GetForMeRequestDetails();
}

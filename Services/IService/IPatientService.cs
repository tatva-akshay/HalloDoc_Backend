using Entity.DTO.Login;
using Entity.DTO.Patient;
using Entity.Models;
using Microsoft.AspNetCore.Http;

namespace Services.IService;

public interface IPatientService
{
    Task<VerifyPatientRole> VerifyRoleIfPatient(string emailId);
    Task<string> ConfirmationNumber(PatientDetails requestData);
    Task<int> CreateRequest(PatientDetails requestData);
    Task<(int, bool)> OtherRequests(OtherRequest requestData);
    // Task<(int,bool)> CreatefamilyRequest(FamilyRequest requestData);
    // Task<(int,bool)> CreateBusinessRequest(BusinessRequest requestData);
    // Task<(int,bool)> CreateConciergeRequest(ConciergeRequest requestData);
    Task uploadFile(List<IFormFile> files, int requestId);
    Task<List<Region>> getAllRegionList();
    Task CreateAccountPatient(LoginDTO loginDetails);
    Task<ResetPasswordVM> ForgotPassword(string Email);
}

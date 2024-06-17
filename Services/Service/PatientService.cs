using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;
using CloudinaryDotNet.Actions;
using Entity.DTO.General;
using Entity.DTO.Login;
using Entity.DTO.Patient;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Repository.IRepository;
using Services.IService;

namespace Services.Service;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IAuthRepository _authRepository;

    public PatientService(IPatientRepository patientRepository, ITableRepository tableRepository, IAuthRepository authRepository)
    {
        _patientRepository = patientRepository;
        _tableRepository = tableRepository;
        _authRepository = authRepository;
    }

    public async Task CreateAccountPatient(LoginDTO loginDetails)
    {
        try
        {
            AspNetUser oldAspNetUser = await _patientRepository.GetAspNetUserByEmail(loginDetails.Email);
            oldAspNetUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(loginDetails.Password);
            oldAspNetUser.ModifiedDate = DateTime.Now;

            //btw no need to do this if not checking 24 hours validation
            PasswordReset passwordReset = await _patientRepository.GetPasswordResetByEmail(loginDetails.Email);
            passwordReset.IsUpdated = true;

            await _tableRepository.SaveChanges();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<ResetPasswordVM> ForgotPassword(string Email)
    {
        AspNetUser oldAspNetUser = await _patientRepository.GetAspNetUserByEmail(Email);
        ResetPasswordVM resetPassword = new ResetPasswordVM();

        LoginUserStatus userStatus = await _authRepository.IsUserExists(Email);
        if (userStatus == null)
        {
            resetPassword.Email = Email;
            resetPassword.isExist = false;
            return resetPassword;
        }
        int id = 0;
        switch (userStatus.Role)
        {
            case "1":
                Admin admin = await _patientRepository.GetAdminByEmail(Email);
                id = admin.AdminId;
                break;
            case "2":
                Physician pysician = await _patientRepository.GetPhysicianByEmail(Email);
                id = pysician.PhysicianId;
                break;
            default: break;
        }
        resetPassword.id = id;
        resetPassword.RoleId = int.Parse(userStatus.Role);

        //repo method to make entry in passwordReset Table
        PasswordReset passwordReset = await _patientRepository.GetPasswordResetByEmail(Email);
        string Token = Guid.NewGuid().ToString();
        if (passwordReset == null)
        {
            PasswordReset newResetPassword = new()
            {
                Token = Token,
                CreatedDate = DateTime.Now,
                Email = Email,
                IsUpdated = false
            };
            await _tableRepository.AddPasswordReset(newResetPassword);
        }
        else
        {
            passwordReset.Token = Token;
            passwordReset.CreatedDate = DateTime.Now;
        }
        await _tableRepository.SaveChanges();
        resetPassword.isValidated = true;
        resetPassword.token = Token;
        return resetPassword;
    }

    public async Task<resetPasswordGetDTO> GetResetPasswordData(string token)
    {
        try
        {
            PasswordReset passwordReset = await _patientRepository.GetPasswordResetByToken(token);
            resetPasswordGetDTO resetPasswordGetDTO = new resetPasswordGetDTO();
            if (resetPasswordGetDTO == null)
            {
                return resetPasswordGetDTO;
            }
            if (DateTime.Now.Day - passwordReset.CreatedDate.Day > 1)
            {
                resetPasswordGetDTO.isValidated = false;
            }
            else
            {
                resetPasswordGetDTO.isValidated = true;
            }
            resetPasswordGetDTO.Email = passwordReset.Email;
            return resetPasswordGetDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> ConfirmationNumber(PatientDetails requestData)
    {
        string CNumber = "";
        CNumber += requestData.City.Substring(0, 2);

        string day = $"{requestData.Bdate.Day:00}";
        string month = $"{requestData.Bdate.Month:00}";
        // string year = $"{(requestData.Bdate.Year):0000}";

        CNumber += day;
        CNumber += month;
        // CNumber += year.ToString().Substring(2);

        CNumber += requestData.LastName.Substring(0, 2);
        CNumber += requestData.FirstName.Substring(0, 2);

        int noOfRequests = await _patientRepository.GetNumbferOfRequestOnTheDay(DateTime.Now);
        string NoRequest = $"{noOfRequests:0000}";
        CNumber += NoRequest;

        return CNumber;
    }

    public async Task<int> CreateRequest(PatientDetails requestData)
    {
        try
        {
            if (!requestData.isPatientExist)
            {
                AspNetUser aspuser = new AspNetUser()
                {
                    UserName = requestData.FirstName + requestData.LastName,
                    Email = requestData.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestData.PasswordHash),
                    Phonenumber = requestData.Mobile,
                    CreatedDate = DateTime.Now
                };

                await _tableRepository.AddAspNetUser(aspuser);
                await _tableRepository.SaveChanges();

                User newUser = new User();
                newUser.AspNetUserId = aspuser.Id;
                newUser.IsDeleted = false;
                newUser.FirstName = requestData.FirstName;
                newUser.LastName = requestData.LastName;
                newUser.Email = requestData.Email;
                newUser.Mobile = requestData.Mobile;
                newUser.Street = requestData.Street;
                newUser.City = requestData.City;
                newUser.RegionId = requestData.regionId;
                newUser.ZipCode = requestData.ZipCode.ToString();
                newUser.CreatedDate = DateTime.Now;
                newUser.CreatedBy = requestData.FirstName;
                newUser.Status = 1;
                newUser.StrMonth = requestData.Bdate.Month.ToString();
                newUser.IntDate = requestData.Bdate.Day;
                newUser.IntYear = requestData.Bdate.Year;

                await _tableRepository.AddUserTable(newUser);
                await _tableRepository.SaveChanges();

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspuser.Id,
                    RoleId = 3
                };

                await _tableRepository.AddAspNetUserRole(aspNetUserRole);
                await _tableRepository.SaveChanges();
            }

            User oldUser = await _patientRepository.GetUserByEmail(requestData.Email);

            Request newRequest = new Request()
            {
                RequestTypeId = 2,
                UserId = oldUser?.UserId,
                FirstName = requestData.FirstName,
                LastName = requestData.LastName,
                PhoneNumber = requestData.Mobile,
                Email = requestData.Email,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = false,
                IsDeleted = false
            };
            newRequest.ConfirmationNumber = await ConfirmationNumber(requestData);

            await _tableRepository.AddRequest(newRequest);
            await _tableRepository.SaveChanges();

            string userState = await _patientRepository.GetStateName(requestData.regionId);

            RequestClient newRequestClient = new RequestClient()
            {
                RequestId = newRequest.RequestId,
                RegionId = requestData.regionId,
                FirstName = requestData.FirstName,
                LastName = requestData.LastName,
                PhoneNumber = requestData.Mobile,
                Email = requestData.Email,
                Street = requestData.Street,
                City = requestData.City,
                State = userState,
                ZipCode = requestData.ZipCode.ToString(),
                Address = requestData.Street + ',' + requestData.City + ',' + userState + "," + requestData.ZipCode,
                Notes = requestData.Symptoms,
                StrMonth = requestData.Bdate.Month.ToString(),
                IntDate = requestData.Bdate.Day,
                IntYear = requestData.Bdate.Year
            };

            await _tableRepository.AddRequestClient(newRequestClient);
            await _tableRepository.SaveChanges();

            if (requestData.File != null)
            {
                foreach (var file in requestData.File)
                {
                    string path = Path.Combine(@"D:/Project/Angular/HalloDoc/Documents/" + "Request" + newRequest.RequestId);

                    // Create folder if not exist
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string fileNameWithPath = Path.Combine(path, file.FileName);

                    RequestWiseFile newRequestWiseFile = new RequestWiseFile()
                    {
                        RequestId = newRequest.RequestId,
                        FileName = fileNameWithPath,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _tableRepository.AddRequestWiseFile(newRequestWiseFile);
                }
                await _tableRepository.SaveChanges();
            }
            return newRequest.RequestId;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<(int, bool)> OtherRequests(OtherRequest requestData)
    {
        try
        {
            AspNetUser oldAspNetUser = await _patientRepository.GetAspNetUserByEmail(requestData.Email);
            string userState = await _patientRepository.GetStateName(requestData.regionId);
            bool isExists = oldAspNetUser == null ? false : true;
            //this is for the specific Role email. is email exist will be checked at frontend call wether it is admin or physician. if it is. inalid
            if (oldAspNetUser == null)
            {
                AspNetUser newAspNetUser = new AspNetUser();
                newAspNetUser.UserName = requestData.FirstName;
                newAspNetUser.Email = requestData.Email;
                newAspNetUser.Phonenumber = requestData.Mobile.ToString();

                await _tableRepository.AddAspNetUser(newAspNetUser);
                await _tableRepository.SaveChanges();

                User newUser = new User();
                newUser.AspNetUserId = newAspNetUser.Id;
                newUser.IsDeleted = false;
                newUser.Email = requestData.Email;
                newUser.FirstName = requestData.FirstName;
                newUser.LastName = requestData.LastName;
                newUser.CreatedBy = requestData.YFirstName;
                newUser.CreatedBy = requestData.YFirstName;
                newUser.CreatedDate = DateTime.Now;
                newUser.Street = requestData.Street;
                newUser.State = userState;
                newUser.City = requestData.City;
                newUser.ZipCode = requestData.ZipCode.ToString();
                if (requestData.Bdate != null)
                {
                    newUser.StrMonth = requestData.Bdate.Month.ToString();
                    newUser.IntDate = requestData.Bdate.Day;
                    newUser.IntYear = requestData.Bdate.Year;
                }
                newUser.RegionId = requestData.regionId;
                newUser.Mobile = requestData.Mobile.ToString();

                await _tableRepository.AddUserTable(newUser);
                await _tableRepository.SaveChanges();

                AspNetUserRole newAspNetUserRole = new AspNetUserRole();
                newAspNetUserRole.UserId = newAspNetUser.Id;
                newAspNetUserRole.RoleId = 3;

                await _tableRepository.AddAspNetUserRole(newAspNetUserRole);
                await _tableRepository.SaveChanges();

                string Token = Guid.NewGuid().ToString();
                PasswordReset newResetPassword = new()
                {
                    Token = Token,
                    CreatedDate = DateTime.Now,
                    Email = requestData.Email,
                    IsUpdated = false
                };

                await _tableRepository.AddPasswordReset(newResetPassword);
                await _tableRepository.SaveChanges();
            }

            User oldUser = await _patientRepository.GetUserByEmail(requestData.Email);

            Request newRequest = new Request()
            {
                RequestTypeId = requestData.RequestType,
                UserId = oldUser.UserId,
                FirstName = requestData.YFirstName,
                LastName = requestData.YLastName,
                PhoneNumber = requestData.YMobile.ToString(),
                Email = requestData.YEmail,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = false,
                RelationName = requestData.RequestType == 2 ? requestData.RelationName : "Other",
                IsDeleted = false
            };

            await _tableRepository.AddRequest(newRequest);
            await _tableRepository.SaveChanges();

            RequestClient newRequestClient = new RequestClient()
            {
                RequestId = newRequest.RequestId,
                RegionId = requestData.regionId,
                FirstName = requestData.FirstName,
                LastName = requestData.LastName,
                PhoneNumber = requestData.Mobile.ToString(),
                Email = requestData.Email,
                Street = requestData.Street,
                City = requestData.City,
                State = userState,
                ZipCode = requestData.ZipCode.ToString(),
                Address = requestData.Street + ',' + requestData.City + ',' + userState + "," + requestData.ZipCode,
                Notes = requestData.Symptoms,
            };

            if (requestData.Bdate != null)
            {
                newRequestClient.StrMonth = requestData.Bdate.Month.ToString();
                newRequestClient.IntDate = requestData.Bdate.Day;
                newRequestClient.IntYear = requestData.Bdate.Year;
            }

            await _tableRepository.AddRequestClient(newRequestClient);
            await _tableRepository.SaveChanges();

            if (requestData.File != null && requestData.RequestType == 2)
            {
                foreach (var file in requestData.File)
                {
                    string path = Path.Combine(@"D:/Project/Angular/HalloDoc/Documents/" + "Request" + newRequest.RequestId);

                    // Create folder if not exist
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string fileNameWithPath = Path.Combine(path, file.FileName);

                    RequestWiseFile newRequestWiseFile = new RequestWiseFile()
                    {
                        RequestId = newRequest.RequestId,
                        FileName = fileNameWithPath,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _tableRepository.AddRequestWiseFile(newRequestWiseFile);
                }
                await _tableRepository.SaveChanges();
            }
            return (newRequest.RequestId, isExists);
        }
        catch (Exception ex)
        {
            return (0, false);
        }
    }

    public async Task<VerifyPatientRole> VerifyRoleIfPatient(string emailId)
    {
        VerifyPatientRole userStatus = await _patientRepository.VerifyRoleIfPatient(emailId);
        if (userStatus == null)
        {
            return userStatus;
        }
        if (userStatus.IsPatient)
        {

        }
        return userStatus;
    }

    public Task uploadFile(List<IFormFile> files, int requestId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RegionsDropDown>> getAllRegionList()
    {
        return await _tableRepository.getAllRegionList();
    }

    public async Task<PatientProfile> GetPatientProfile(string Email)
    {
        try
        {
            User user = await _patientRepository.GetUserByEmail(Email);
            PatientProfile patientDetails = new()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                regionId = (int)user.RegionId,
            };
            patientDetails.allRegion = await _tableRepository.GetRegionsDropDowns();

            if (user.StrMonth != null && user.IntDate != null && user.IntYear != null)
            {
                string day = $"{user.IntDate:00}";
                string month = $"{int.Parse(user.StrMonth):00}";
                string year = $"{(user.IntYear):0000}";

                DateTime parseDate = DateTime.ParseExact(day + month + year, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                patientDetails.Bdate = parseDate;
                return patientDetails;
            }
            return patientDetails;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task UpdatePatientProfile(PatientProfile patientDetails)
    {
        try
        {
            User user = await _patientRepository.GetUserByEmail(patientDetails.Email);
            string userState = await _patientRepository.GetStateName(patientDetails.regionId);

            user.FirstName = patientDetails.FirstName;
            user.LastName = patientDetails.LastName;
            user.Mobile = patientDetails.Mobile;
            user.Street = patientDetails.Street;
            user.City = patientDetails.City;
            user.State = patientDetails.State;
            user.ZipCode = patientDetails.ZipCode;
            user.RegionId = patientDetails.regionId;
            user.State = userState;

            if (patientDetails.Bdate != null)
            {
                user.IntDate = int.Parse(patientDetails.Bdate.Day.ToString());
                user.IntYear = int.Parse(patientDetails.Bdate.Year.ToString());
                user.StrMonth = patientDetails.Bdate.Month.ToString();
            }
            await _tableRepository.SaveChanges();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<PatientDashboard> GetDashboardContent(string userEmail)
    {
        try
        {
            User user = await _patientRepository.GetUserByEmail(userEmail);
            return await _patientRepository.GetDashboardContent(user.UserId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<SingleRequest> GetSingleRequestDetails(int requestId)
    {
        try
        {
            return await _patientRepository.GetSingleRequestDetails(requestId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task UploadDocuments(UploadDocument userUploadedDocuments)
    {
        try
        {
            foreach (var file in userUploadedDocuments.uploadedDocumentList)
            {
                string path = Path.Combine(@"D:/Project/Angular/HalloDoc/Documents/Request" + userUploadedDocuments.RequestId);

                // Create folder if not exist
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, file.FileName);
                fileNameWithPath = fileNameWithPath.Replace("\\", "/");
                RequestWiseFile newRequestWiseFile = new RequestWiseFile()
                {
                    RequestId = userUploadedDocuments.RequestId,
                    FileName = fileNameWithPath,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                await _tableRepository.AddRequestWiseFile(newRequestWiseFile);
            }
            await _tableRepository.SaveChanges();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<DownloadRWFResponse> DownloadDocuments(DownloadRWF downloadRWF)
    {
        try
        {
            return await _patientRepository.DownloadRequestWiseFileDocuments(downloadRWF);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public async Task<PatientDetails> GetForMePatientRequestData(string email)
    {
        try
        {
            PatientDetails patientDetails = new PatientDetails();
            PatientProfile oldPatientProfile = await GetPatientProfile(email);

            patientDetails.FirstName = oldPatientProfile.FirstName;
            patientDetails.LastName = oldPatientProfile.LastName;
            patientDetails.Email = email;
            patientDetails.Bdate = oldPatientProfile.Bdate;
            patientDetails.Mobile = oldPatientProfile.Mobile;
            patientDetails.Street = oldPatientProfile.Street;
            patientDetails.City = oldPatientProfile.City;
            patientDetails.State = oldPatientProfile.State;
            patientDetails.ZipCode = decimal.Parse(oldPatientProfile.ZipCode);
            patientDetails.regionId = oldPatientProfile.regionId;

            return patientDetails;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


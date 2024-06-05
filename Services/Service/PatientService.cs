using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;
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
        string Token = Guid.NewGuid().ToString();
        PasswordReset newResetPassword = new()
        {
            Token = Token,
            CreatedDate = DateTime.Now,
            Email = Email,
            IsUpdated = false
        };

        await _tableRepository.AddPasswordReset(newResetPassword);
        await _tableRepository.SaveChanges();

        resetPassword.isValidated = true;
        return resetPassword;
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
                    PhoneNumber = requestData.Mobile,
                    CreatedDate = DateTime.Now
                };

                await _tableRepository.AddAspNetUser(aspuser);
                await _tableRepository.SaveChanges();

                User newUser = new User();
                newUser.AspNetUserId = aspuser.Id;
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
                    RoleId = "3"
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
                        file.CopyToAsync(stream);
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
                newAspNetUser.PhoneNumber = requestData.Mobile;

                await _tableRepository.AddAspNetUser(newAspNetUser);
                await _tableRepository.SaveChanges();

                User newUser = new User();
                newUser.AspNetUserId = newAspNetUser.Id;
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
                    newUser.StrMonth = requestData.Bdate.Value.Month.ToString();
                    newUser.IntDate = requestData.Bdate.Value.Day;
                    newUser.IntYear = requestData.Bdate.Value.Year;
                }
                newUser.RegionId = requestData.regionId;
                newUser.Mobile = requestData.Mobile;

                await _tableRepository.AddUserTable(newUser);
                await _tableRepository.SaveChanges();

                AspNetUserRole newAspNetUserRole = new AspNetUserRole();
                newAspNetUserRole.UserId = newAspNetUser.Id;
                newAspNetUserRole.RoleId = "3";

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
                PhoneNumber = requestData.YMobile,
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
                PhoneNumber = requestData.Mobile,
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
                newRequestClient.StrMonth = requestData.Bdate.Value.Month.ToString();
                newRequestClient.IntDate = requestData.Bdate.Value.Day;
                newRequestClient.IntYear = requestData.Bdate.Value.Year;
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
                        file.CopyToAsync(stream);
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

    public async Task<List<Region>> getAllRegionList()
    {
        return await _tableRepository.getAllRegionList();
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
}


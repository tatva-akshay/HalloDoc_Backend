using System.Collections;
using Entity.DataContext;
using Entity.DTO.Login;
using Entity.DTO.Patient;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ITableRepository _tableRepository;
    public PatientRepository(ApplicationDbContext context, ITableRepository tableRepository)
    {
        _context = context;
        _tableRepository = tableRepository;
    }

    public async Task<VerifyPatientRole> VerifyRoleIfPatient(string emailId)
    {
        VerifyPatientRole user = await _context.AspNetUserRoles?.Include(a => a.User)
        .Where(a => a.User.Email == emailId)
        .Select(a => new VerifyPatientRole()
        {
            aspNetUser = a.User,
            IsPatient = a.RoleId == 3 ? true : false,
            Role = a.RoleId.ToString()
        }).FirstOrDefaultAsync();
        return user;
    }

    public async Task<int> GetNumbferOfRequestOnTheDay(DateTime Date)
    {
        return await _context.Requests.Where(a => a.CreatedDate == Date).CountAsync();
    }

    public async Task<string> GetStateName(int regionId)
    {
        string stateName = await _context.Regions.Where(a => a.RegionId == regionId).Select(x => x.Name).FirstOrDefaultAsync();
        return stateName;
    }

    public async Task<User> GetUserByEmail(string userEmail)
    {
        User oldUser = await _context.Users.FirstOrDefaultAsync(m => m.Email == userEmail);
        return oldUser;
    }

    public async Task<AspNetUser> GetAspNetUserByEmail(string userEmail)
    {
        AspNetUser oldAspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Email == userEmail);
        return oldAspNetUser;
    }

    public async Task<Admin> GetAdminByEmail(string emailId)
    {
        Admin admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == emailId);
        return admin;
    }
    public async Task<Physician> GetPhysicianByEmail(string emailId)
    {
        Physician physician = await _context.Physicians.FirstOrDefaultAsync(a => a.Email == emailId);
        return physician;
    }

    public async Task<PasswordReset> GetPasswordResetByEmail(string userEmail)
    {
        PasswordReset oldPasswordReset = await _context.PasswordResets.FirstOrDefaultAsync(m => m.Email == userEmail);
        return oldPasswordReset;
    }

    public async Task<PatientDashboard> GetDashboardContent(int userId)
    {
        Dictionary<int, string>
    statusMapping = new Dictionary<int, string>
                    {
        { 1, "Unassigned" },
        { 2, "Accepted" },
        { 3, "Cancelled" },
        { 4, "Active" },
        { 5, "Active" },
        { 6, "Conclude" },
        { 7, "CancelledByPatient" },
        { 8, "Closed" },
        { 9, "UnPaid" },
        { 11, "Blocked" },
        { 10, "Clear" },


                    };
        PatientDashboard patientDashboardData = new PatientDashboard();
        patientDashboardData.dashboardContent = await _context.Requests.Where(a => a.UserId == userId)
        .Select(c => new PatientDashboardContent()
        {
            RequestId = c.RequestId,
            Status = c.Status,
            DocumentCount = _context.RequestWiseFiles.Where(a => a.RequestId == c.RequestId).Count(),
            StatusName = (c.Status > 10 || c.Status < 1) ? "Error" : statusMapping[c.Status],
        }).ToListAsync();
        return patientDashboardData;
    }

    public async Task<SingleRequest> GetSingleRequestDetails(int requestId)
    {
        SingleRequest requestData = new SingleRequest();
        requestData.RequestId = requestId;
        requestData.DocumentList = await _context.RequestWiseFiles.Where(a => a.RequestId == requestId)
        .Select(c => new DocumentList()
        {
            RequestId = c.RequestId,
            RequestWiseFileId = c.RequestWiseFileId,
            CreatedDate = c.CreatedDate,
            FileName = c.FileName,
            Uploader = c.AdminId == null ?
            (c.PhysicianId == null ?
            "Patient" + _context.RequestClients.FirstOrDefault(x => x.RequestId == requestId).FirstName
            : "Provider" + _context.Physicians.FirstOrDefault(x => x.PhysicianId == c.PhysicianId).FirstName)
             : "Admin" + _context.Admins.FirstOrDefault(x => x.AdminId == c.AdminId).FirstName
        })
        .ToListAsync();
        return requestData;
    }

    public async Task<DownloadRWFResponse> DownloadRequestWiseFileDocuments(DownloadRWF downloadRWF)
    {
        DownloadRWFResponse downloadResponse = new DownloadRWFResponse();
        if (downloadRWF.isDownloadALl)
        {
            downloadResponse.RequestWiseFileList = await _context.RequestWiseFiles.Where(a => a.RequestId == downloadRWF.RequestId).Select(b => b.FileName).ToListAsync();
        }
        else
        {
            downloadResponse.RequestWiseFileList = await _context.RequestWiseFiles
            .Where(a => a.RequestId == downloadRWF.RequestId && downloadRWF.RequestWiseFileId.Contains(a.RequestWiseFileId))
            .Select(b => b.FileName).ToListAsync();
        }
        downloadResponse.RequestId = downloadRWF.RequestId;
        return downloadResponse;
    }
}

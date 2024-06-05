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
            IsPatient = a.RoleId == "3" ? true : false,
            Role = a.RoleId
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

    public async Task<Admin> GetAdminByEmail(string emailId){
        Admin admin = await _context.Admins.FirstOrDefaultAsync(a=>a.Email == emailId);
        return admin;
    }
    public async Task<Physician> GetPhysicianByEmail(string emailId){
        Physician physician = await _context.Physicians.FirstOrDefaultAsync(a=>a.Email == emailId);
        return physician;
    }

}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity.DataContext;
using Entity.DTO;
using Entity.DTO.Login;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;

namespace Repository.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthRepository(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<LoginUserStatus> IsUserExists(string Email)
    {
        // LoginUserDTO? user = _context.AspNetUserRoles.Include(a=>a.User).Where(a=>a.User.Email==loginDetails.Email
        // && BCrypt.Net.BCrypt.Verify(loginDetails.Password, a.User.PasswordHash))
        // .Select(a=> new LoginUserDTO(){
        //     Email = a.User.Email,
        //     Role = a.RoleId,
        // }).FirstOrDefault();
        // return user;

        LoginUserStatus? userStatus = await _context.AspNetUserRoles?.Include(a => a.User)
        .Where(a=>a.User.Email==Email)
        .Select(a=>new LoginUserStatus(){
            Email = a.User.Email,
            Password = a.User.PasswordHash,
            Role = a.RoleId.ToString(),
        }).FirstOrDefaultAsync();

        return userStatus;
    }
}

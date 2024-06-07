using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CloudinaryDotNet.Actions;
using Entity.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Services.IService;

namespace API.CustomAuthorizeMiddleware;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    public string Roles { get; set; }
    public CustomAuthorize(string Role)
    {
        this.Roles = Role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (token == null)
        {
            context.Result = new ObjectResult(new APIResponse
            {
                Result = null,
                IsSuccess = false,
                Error = "Unauthorized: No token provided",
                HttpStatusCode = HttpStatusCode.Forbidden
            });
            return;
        }

        if (!token.StartsWith("Bearer "))
        {
            context.Result = new ObjectResult(new APIResponse
            {
                Result = null,
                IsSuccess = false,
                Error = "Unauthorized: Invalid token format",
                HttpStatusCode = HttpStatusCode.Forbidden
            });
            return;
        }

        var tokenValue = token.Substring("Bearer ".Length);
        var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
        
        if (authService==null)
        {
            context.Result = new ObjectResult(new APIResponse
            {
                Result = null,
                IsSuccess = false,
                Error = "Internal Server Error",
                HttpStatusCode = HttpStatusCode.InternalServerError
            });
            return;
        }
            
        try
        {
            var principal = authService.ValidateToken(tokenValue,out JwtSecurityToken jwtToken);
            var roleClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (roleClaim != null && roleClaim == Roles)
            {
                return;
            }

            if (roleClaim != null && !(roleClaim == Roles))
            {
                context.Result = new ObjectResult(new APIResponse
                {
                    Result = roleClaim,
                    IsSuccess = false,
                    Error = $"Forbidden: Role '{roleClaim}' is not authorized",
                    HttpStatusCode = HttpStatusCode.Forbidden
                });
                return;
            }
        }
        catch (SecurityTokenException ex)
        {
            context.Result = new ObjectResult(new APIResponse
            {
                Result = null,
                IsSuccess = false,
                Error = "Unauthorized: Invalid token",
                HttpStatusCode = HttpStatusCode.Unauthorized
            });
            return;
        }

        context.Result = new ObjectResult(new APIResponse
        {
            Result = null,
            IsSuccess = false,
            Error = "Unauthorized: Unknown error",
            HttpStatusCode = HttpStatusCode.Forbidden
        });
    }
}


using System.ComponentModel.DataAnnotations;

namespace Entity.DTO.Login;

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

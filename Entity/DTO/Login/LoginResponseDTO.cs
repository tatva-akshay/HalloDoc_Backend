using Entity.Models;

namespace Entity.DTO.Login;

public class LoginResponseDTO
{
    public string token {get; set;}
    public string Email {get; set;}
    public string Role {get; set;}
}

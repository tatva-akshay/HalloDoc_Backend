namespace Entity.DTO.Login;

public class ResetPasswordVM
{
    public string Email { get; set; }
    public bool isExist { get; set; } = true;
    public bool isValidated { get; set; } = false;
    public int RoleId { get; set; }
    public int id { get; set; } 
    public string? token { get; set; }
}

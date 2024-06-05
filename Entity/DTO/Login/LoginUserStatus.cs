namespace Entity.DTO.Login;

public class LoginUserStatus
{
    public bool IsExists {get; set;} = false;
    public bool PasswordMatched {get; set;} = true;
    public bool IsUserCreated  {get; set;} = true;
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}

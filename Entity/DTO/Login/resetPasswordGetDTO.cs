namespace Entity.DTO.Login;

public class resetPasswordGetDTO
{
    public string? Email { get; set; }
    public bool isValidated { get; set; }=false;
}

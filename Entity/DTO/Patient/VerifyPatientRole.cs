using Entity.Models;

namespace Entity.DTO.Patient;

public class VerifyPatientRole
{
    public AspNetUser? aspNetUser  { get; set; }
    public bool IsPatient { get; set; } = false;
    public string? Role { get; set; }
}

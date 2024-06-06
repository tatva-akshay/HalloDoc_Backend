namespace Entity.DTO.Patient;

public class ReviewAgreement
{
    public int RequestId { get; set; }
    public string? PatientCancellationNotes { get; set; }
    public string? PatientName { get; set; }
    public string? Email { get; set; }
}

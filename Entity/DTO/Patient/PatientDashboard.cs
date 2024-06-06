namespace Entity.DTO.Patient;

public class PatientDashboard
{
    public List<PatientDashboardContent> dashboardContent = new List<PatientDashboardContent>();
}

public class PatientDashboardContent {
    public int RequestId { get; set; }  
    public int Status { get; set; }
    public string? StatusName { get; set; }
    public int DocumentCount { get; set; } = 0;
}

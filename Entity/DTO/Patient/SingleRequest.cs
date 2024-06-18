namespace Entity.DTO.Patient;

public class SingleRequest
{
   public List<DocumentList> DocumentList { get; set; }
   public int RequestId { get; set; }
   public string? ConfirmationNumber { get; set; }
   public string? PatientName { get; set; }
}

public class DocumentList
{
   public int RequestWiseFileId { get; set; }
   public int RequestId { get; set; }
   public string FileName { get; set; }
   public DateTime CreatedDate { get; set; }
   public string Uploader { get; set; }
   public int RoleId { get; set; }
}
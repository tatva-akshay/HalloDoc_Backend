using Microsoft.AspNetCore.Http;

namespace Entity.DTO.Patient;

public class UploadDocument
{
    public int RequestId { get; set; }
    public List<IFormFile> uploadedDocumentList { get; set; }
}

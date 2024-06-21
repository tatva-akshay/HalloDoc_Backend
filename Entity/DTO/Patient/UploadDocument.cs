using Microsoft.AspNetCore.Http;

namespace Entity.DTO.Patient;

public class UploadDocument
{
    public string RequestId { get; set; }
    public List<IFormFile> uploadedDocumentList { get; set; }
}

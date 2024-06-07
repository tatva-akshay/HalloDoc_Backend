namespace Entity.DTO.Patient;

public class DownloadRWFResponse
{
    public List<string> RequestWiseFileList  = new List<string>();
    public int RequestId { get; set; }
}

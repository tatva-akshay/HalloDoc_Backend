namespace Entity.DTO.Patient;

public class DownloadRWF
{
    public int RequestId { get; set; }
    public List<int> RequestWiseFileId = new List<int>();
    public bool isDownloadALl {get; set;} = false;
}

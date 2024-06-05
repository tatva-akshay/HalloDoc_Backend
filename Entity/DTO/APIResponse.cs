using System.Net;

namespace Entity.DTO;

public class APIResponse

{
    public string Error { get; set; }
    public bool IsSuccess { get; set; } = false;
    public HttpStatusCode HttpStatusCode { get; set; }
    public Object Result { get; set; }
}

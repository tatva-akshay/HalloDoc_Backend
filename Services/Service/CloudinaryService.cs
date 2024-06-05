using CloudinaryDotNet;
using Services.IService;

namespace Services.Service;

public class CloudinaryService : ICloudinaryService
{
    private readonly string _cloudName;
    private readonly string _apiKey;
    private readonly string _apiSecret;

    public CloudinaryService(string cloudName, string apiKey, string apiSecret)
    {
        _cloudName = cloudName;
        _apiKey = apiKey;
        _apiSecret = apiSecret;
    }

    public Cloudinary GetCloudinaryInstance()
    {
        var config = new Account
        {
            Cloud = _cloudName,
            ApiKey = _apiKey,
            ApiSecret = _apiSecret
        };
        return new Cloudinary(config);
    }
}

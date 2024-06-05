using CloudinaryDotNet;

namespace Services.IService;

public interface ICloudinaryService
{
    public Cloudinary GetCloudinaryInstance();
}

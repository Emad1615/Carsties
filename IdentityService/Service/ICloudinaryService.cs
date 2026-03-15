using CloudinaryDotNet.Actions;

namespace IdentityService.Service
{
    public interface ICloudinaryService
    {
        Task<PhotoUploadResult?> SavePhoto(IFormFile file);
        Task<DeletionResult> DeletePhoto(string id);
    }
}

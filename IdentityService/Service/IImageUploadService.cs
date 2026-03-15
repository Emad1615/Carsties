namespace IdentityService.Service
{
    public interface IImageUploadService
    {
        Task<string?> UploadPhoto(string ContainerName, IFormFile file);
        Task DeletePhoto(string fileUrl, string container);
        Task<string?> EditPhoto(string fileUrl, string container, IFormFile file);  
    }
}

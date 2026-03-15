using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace IdentityService.Service
{
    public class CloudinaryService: ICloudinaryService
    {
        private readonly ICloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            var account = new Account()
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret,
            };
            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoUploadResult?> SavePhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                var stream=file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(150).Height(150).Crop("fill"),
                    Folder = "CarstiesUsersAvatar"
                };
                var result= await _cloudinary.UploadAsync(uploadParams);

                if(result.Error is not null)
                    throw new Exception(result.Error.Message);
                return new PhotoUploadResult { PublicId = result.PublicId, Url = result.SecureUrl.AbsoluteUri };
            }
            return null;
        }
        public async Task<DeletionResult> DeletePhoto(string id)
        {
            var param= new DeletionParams(id);
            var result=await _cloudinary.DestroyAsync(param);
            if (result.Error is not null)
                throw new Exception(result.Error.Message);
            return result;
        }
    }
}

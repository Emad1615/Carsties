namespace IdentityService.Service
{
    public class ImageUploadService(IWebHostEnvironment webHost, IHttpContextAccessor httpContext) : IImageUploadService
    {
        public async Task<string?> UploadPhoto(string ContainerName, IFormFile file)
        {
            if (file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                string fileName = $"{Guid.NewGuid().ToString()}_{extension}";
                string folder = Path.Combine(webHost.WebRootPath, ContainerName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var route = Path.Combine(folder, fileName);
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    await File.WriteAllBytesAsync(route, content);
                }
                var url = $"{httpContext.HttpContext!.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
                return Path.Combine(url, ContainerName, fileName);
            }
            return null;
        }
        public Task DeletePhoto(string fileUrl, string container)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return Task.CompletedTask;
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(webHost.WebRootPath, container, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
        public async Task<string?> EditPhoto(string fileUrl, string container, IFormFile file)
        {
            await DeletePhoto(fileUrl, container);
            return await UploadPhoto(container, file);
        }
    }
}

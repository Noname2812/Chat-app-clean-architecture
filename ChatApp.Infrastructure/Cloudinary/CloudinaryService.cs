using ChatApp.Application.Abstractions.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace ChatApp.Infrastructure.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            var account = new Account();
            configuration.GetSection("CloudinaryOptions").Bind(account);
            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }
        public async Task<DeletionResult> DeleteImage(string id)
        {
            var deleteParams = new DeletionParams(id);
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
            return deleteResult;
        }

        public async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation(),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}

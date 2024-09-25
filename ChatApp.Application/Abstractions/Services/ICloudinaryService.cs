

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.Abstractions.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImage(IFormFile file);
        Task<DeletionResult> DeleteImage(string id);
        Task<VideoUploadResult> UploadAudio(IFormFile file);
    }
}

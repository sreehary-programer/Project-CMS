using BCA007.Shared.Service.Core;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace BCA007.Services.Core
{
    public class ServerFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public ServerFileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<bool> UploadTeamPhotoAsync(Stream stream, string fileName)
        {
            try
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, "team-photo.png");

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteTeamPhotoAsync()
        {
            try
            {
                var filePath = Path.Combine(_environment.WebRootPath, "images", "team-photo.png");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> CheckTeamPhotoExistsAsync()
        {
            var filePath = Path.Combine(_environment.WebRootPath, "images", "team-photo.png");
            return Task.FromResult(System.IO.File.Exists(filePath));
        }
    }
}

using System.IO;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Core
{
    public interface IFileUploadService
    {
        Task<bool> UploadTeamPhotoAsync(Stream stream, string fileName);
        Task<bool> DeleteTeamPhotoAsync();
        Task<bool> CheckTeamPhotoExistsAsync();
    }
}

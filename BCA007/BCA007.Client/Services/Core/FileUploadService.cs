using BCA007.Shared.Service.Core;
using System.IO;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Core
{
    public class FileUploadService : IFileUploadService
    {
        private readonly HttpClient _httpClient;

        public FileUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UploadTeamPhotoAsync(Stream stream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(stream);
            content.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync("api/upload/team-photo", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTeamPhotoAsync()
        {
            var response = await _httpClient.DeleteAsync("api/upload/team-photo");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckTeamPhotoExistsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<bool>("api/upload/team-photo/exists");
            }
            catch
            {
                return false;
            }
        }
    }
}

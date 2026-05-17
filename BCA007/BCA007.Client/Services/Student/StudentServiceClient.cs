using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BCA007.Client.Services.Student
{
    public class StudentServiceClient : IStudentService
    {
        private readonly HttpClient _http;

        public StudentServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/student/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task<List<StudentViewDto>> GetAllAsync() 
        {
            return await _http.GetFromJsonAsync<List<StudentViewDto>>("/api/student/getall") ?? new List<StudentViewDto>();
        }

        public async  Task<StudentDto> CreateAsync(StudentDto dto, Stream? fileStream, string? fileName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"), "dto");

            if (fileStream != null && fileName != null)
            {
                content.Add(new StreamContent(fileStream), "file", fileName);
            }

            var response = await _http.PostAsync("/api/student/create", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<StudentDto>()!;
        }

        public async Task<StudentDto> UpdateAsync(StudentDto dto, Stream? fileStream, string? fileName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(
                JsonSerializer.Serialize(dto)
                ,Encoding.UTF8
                , "application/json"), "dto");

            if (fileStream != null && fileName != null)
            {
                content.Add(new StreamContent(fileStream), "file", fileName);
            }

            var response = await _http.PutAsync("/api/student/edit", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<StudentDto>()!;
        }
    }
}
       //public async Task CreateAsync(StudentDto dto)
        //{
        //    var response = await _http.PostAsJsonAsync("/api/student/create", dto);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var msg = await response.Content.ReadAsStringAsync();
        //        throw new InvalidOperationException(msg);
        //    }
        //}
        //public async Task UpdateAsync(StudentDto dto)
        //{
        //    var response = await _http.PutAsJsonAsync($"api/Student/edit", dto);

        //    if (!response.IsSuccessStatusCode)
        //        throw new ApplicationException(await response.Content.ReadAsStringAsync());
        //}
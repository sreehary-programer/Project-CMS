using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Course
{
    public class CourseServiceClient : ICourseService
    {
        private readonly HttpClient _http;

        public CourseServiceClient(HttpClient _http)
        {
            this._http = _http;
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<CourseDto>>("/api/course/getall") ?? new List<CourseDto>();
        }
        public async Task<CourseDto> CreateAsync(CourseDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/course/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CourseDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<CourseDto> UpdateAsync(CourseDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/course/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CourseDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/course/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }
        public async Task<int> GetIntegerValue()
        {
            var tst= await _http.GetFromJsonAsync<int>("/api/course/test"); 
            
            return tst;
        }

        public async Task<string> GetStringValue()
        {
            string tst = await _http.GetStringAsync("/api/course/teststr")??"";
            return tst ;
        }
    }
}

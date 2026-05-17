using BCA007.Shared.Service.Core;
using BCA007.Shared.DTOs;
using System.Net.Http.Json;
using BCA007.Shared.DTOs.AMS;
namespace BCA007.Client.Services.Core
{
    public class LookupServiceClient : ILookupService
    {
        private readonly HttpClient _httpClient;
        public LookupServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<LookupItemDto>> GetClassesAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/lookup/classes") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetCoursesAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/lookup/courses") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetBatchesAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/lookup/batches") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetSemestersAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("api/lookup/semesters") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetDivisionsAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/lookup/divisions") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetGendersAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/lookup/genders") ?? new List<LookupItemDto>();

        //book lookups
        
        public async Task<List<LookupItemDto>> GetBookTypesAsync()
        => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/booktype") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetBookPublishersAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/bookpublisher") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetBookCategorysAsync() 
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/bookcategory") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetLanguageAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/languages") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetRoleAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/role") ?? new List<LookupItemDto>();


        public async Task<List<LookupItemDto>> GetParentAsync()
            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/parents") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetActivityAsync()
             => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/activity") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetTeacherAsync()
             => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/teachers") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetPeriodAsync()
             => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/period") ?? new List<LookupItemDto>();
        

        public async Task<List<LookupItemDto>> GetSubjetAsync()

            => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/student") ?? new List<LookupItemDto>();

       

        public async Task<List<LookupItemDto>> GetSessionAsync()
             => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/session") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetExamTypeAsync()
             => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/examtype") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetStudentAsync()
                    => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/student") ?? new List<LookupItemDto>();


        public async Task<List<LookupItemDto>> GetUsersAsync()
           => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/allusers") ?? new List<LookupItemDto>();
        public async Task<List<LookupItemDto>> GetTypeAsync()
       => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/type") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetHostalRoomAsync()
         => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/hostalRoom") ?? new List<LookupItemDto>();

        public async Task<List<LookupItemDto>> GetHostalStudentRoomAsync()
        => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/hostalStudentRoom") ?? new List<LookupItemDto>();


        public async Task<List<LookupItemDto>> GetAttendDefaultAsync()
           => await _httpClient.GetFromJsonAsync<List<LookupItemDto>>("/api/Lookup/attenddefault") ?? new List<LookupItemDto>();

    }
}

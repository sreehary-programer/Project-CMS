using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BCA007.Client.Services.Student
{
    public class StudentProfileServiceClient : IStudentProfileService
    {
        private readonly HttpClient _http;

        public StudentProfileServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<StudentViewProfileDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<StudentViewProfileDto>>("/api/studentprofile/getall") ?? new List<StudentViewProfileDto>();
        }


        async Task<List<HostelViewDto>> IStudentProfileService.GetAllHostelAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<HostelViewDto>>("/api/studentprofile/getall") ?? new List<HostelViewDto>();
        }
        public async Task<List<TimetablesViewDto>> GetAllTimetableAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<TimetablesViewDto>>("/api/studentprofile/getall") ?? new List<TimetablesViewDto>();

        }

        public async Task<List<BusViewDto>> GetAllBusAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<BusViewDto>>("/api/studentprofile/getall") ?? new List<BusViewDto>();
        }

        public async Task<List<BookHistoryViewDto>> GetAllBookHistoryAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<BookHistoryViewDto>>("/api/studentprofile/getall") ?? new List<BookHistoryViewDto>();
        }

        public async Task<List<AttendanceViewDto>> GetAllAttendanceAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<AttendanceViewDto>>("/api/studentprofile/getall") ?? new List<AttendanceViewDto>();

        }

        public async Task<List<AttendancePersentageViewDto>> GetAllPersentageAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<AttendancePersentageViewDto>>("/api/studentprofile/getall") ?? new List<AttendancePersentageViewDto>();

        }

        public async Task<List<ResultsViewDto>> GetAllResultAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<ResultsViewDto>>("/api/studentprofile/getall") ?? new List<ResultsViewDto>();

        }

        public async Task<List<FeeDto>> GetAllFeeAsync(int stdId)
        {
            return await _http.GetFromJsonAsync<List<FeeDto>>("/api/studentprofile/getall") ?? new List<FeeDto>();

        }
    }
}

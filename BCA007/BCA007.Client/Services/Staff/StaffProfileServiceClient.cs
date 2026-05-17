using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Staff;
using BCA007.Shared.Service.Student;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Staff
{
        public class StaffProfileServiceClient : IStaffProfileService
        {
            private readonly HttpClient _http;

            public StaffProfileServiceClient(HttpClient http)
            {
                _http = http;
            }
            async Task<List<StaffViewDto>> IStaffProfileService.GetAllAsync()
            {
                return await _http.GetFromJsonAsync<List<StaffViewDto>>("/api/stafftprofile/getall") ?? new List<StaffViewDto>();

            }
        }
    }

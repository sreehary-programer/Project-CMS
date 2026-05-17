using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.IO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BCA007.Client.Services.Library
{

    public class BookIssueServiceClient : IBookIssueViewService
    {
        private readonly HttpClient _http;

        public BookIssueServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BookIssueDto> CreateAsync(BookIssueDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BookIssue/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookIssueDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BookIssue/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BookIssueViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookIssueViewDto>>("/api/BookIssueView/getall") ?? new List<BookIssueViewDto>();
        }

        public Task<BookIssueViewDto> GetByIdAsync(int Bookid)
        {
            throw new NotImplementedException();
        }

        public async Task<BookIssueDto> UpdateAsync(BookIssueDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BookIssue/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookIssueDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        

       
    }
}
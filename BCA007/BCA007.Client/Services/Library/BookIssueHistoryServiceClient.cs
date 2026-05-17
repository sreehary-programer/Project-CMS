using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BCA007.Client.Services.Library
{
    public class BookIssueHistoryServiceClient : IBookIssueHistoryViewService
       
    {
        private readonly HttpClient _http;

        public BookIssueHistoryServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public Task<BookIssueHistoryViewDto> CreateAsync(BookIssueHistoryViewDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookIssueHistoryViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookIssueHistoryViewDto>>("/api/BookIssueHistoryView/getall") ?? new List<BookIssueHistoryViewDto>();
        }

        public Task<BookIssueHistoryViewDto> UpdateAsync(BookIssueHistoryViewDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;
namespace BCA007.Client.Services.Library
{
    public class BookRequestServiceClient : IBookRequestService
    {
        private readonly HttpClient _http;

        public BookRequestServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BookRequestDto> CreateAsync(BookRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BookRequest/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookRequestDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        { 
            throw new NotImplementedException();
        }
        public async Task<List<BookRequestDto>> GetAllAsync()
        { 
            throw new NotImplementedException();
        }

        public Task<BookRequestDto> GetByIdAsync(int Bookid)
        {
            throw new NotImplementedException();
        }

        public async Task<BookRequestDto> UpdateAsync(BookRequestDto dto)
        { 
            throw new NotImplementedException();
        }

    }
}

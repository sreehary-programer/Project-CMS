using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Library
{
    public class BookPublisherServiceClient:IBookPublisherService
    {
        private readonly HttpClient _http;

        public BookPublisherServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BookPublisherDto> CreateAsync(BookPublisherDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BookPublisher/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookPublisherDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BookPublisher/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BookPublisherDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookPublisherDto>>("/api/BookPublisher/getall") ?? new List<BookPublisherDto>();
        }

        public async Task<BookPublisherDto> UpdateAsync(BookPublisherDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BookPublisher/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookPublisherDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}

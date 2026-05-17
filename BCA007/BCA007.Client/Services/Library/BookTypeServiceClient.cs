using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Library
{
    public class BookTypeServiceClient:IBookTypeService
    {

        private readonly HttpClient _http;

        public BookTypeServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BookTypeDto> CreateAsync(BookTypeDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BookType/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookTypeDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BookType/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BookTypeDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookTypeDto>>("/api/BookType/getall") ?? new List<BookTypeDto>();
        }

        public async Task<BookTypeDto> UpdateAsync(BookTypeDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BookType/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookTypeDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
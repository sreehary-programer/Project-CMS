using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Library
{
    public class BookCategoryServiceClient:IBookCategoryService
    {
        private readonly HttpClient _http;

        public BookCategoryServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BookCategoryDto> CreateAsync(BookCategoryDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BookCategory/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookCategoryDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BookCategory/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BookCategoryDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookCategoryDto>>("/api/BookCategory/getall") ?? new List<BookCategoryDto>();
        }

        public async Task<BookCategoryDto> UpdateAsync(BookCategoryDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BookCategory/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookCategoryDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
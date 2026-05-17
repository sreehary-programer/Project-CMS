using BCA007.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Library
{
    public interface  IBookService
    {
        Task<List<BookViewDto>> GetAllAsync();
        Task<BookDto> CreateAsync(BookDto dto, Stream? fileStream, string? fileName);
        Task<BookDto> UpdateAsync(BookDto dto, Stream? fileStream, string? fileName);
        Task<BookDto> UpdateIssueAsync(BookDto dto);
        Task<BookDto> UpdateReturnAsync(BookDto dto);

        Task DeleteAsync(int id);
    }
    public interface IBookCategoryService
    {
        Task<List<BookCategoryDto>> GetAllAsync();
        Task<BookCategoryDto> CreateAsync(BookCategoryDto dto);
        Task<BookCategoryDto> UpdateAsync(BookCategoryDto dto);
        Task DeleteAsync(int id);
    }
    public interface IBookPublisherService
    {
        Task<List<BookPublisherDto>> GetAllAsync();
        Task<BookPublisherDto> CreateAsync(BookPublisherDto dto);
        Task<BookPublisherDto> UpdateAsync(BookPublisherDto dto);
        Task DeleteAsync(int id);
    }
    public interface IBookTypeService
    {
        Task<List<BookTypeDto>> GetAllAsync();
        Task<BookTypeDto> CreateAsync(BookTypeDto dto);
        Task<BookTypeDto> UpdateAsync(BookTypeDto dto);
        Task DeleteAsync(int id);
    }
    //public interface IBookIssueService
    //{
    //    Task<List<BookIssueDto>> GetAllAsync();
    //    Task<BookIssueDto> CreateAsync(BookIssueDto dto);
    //    Task<BookIssueDto> UpdateAsync(BookIssueDto dto);
    //    Task DeleteAsync(int id);
    //}
    public interface IBookIssueViewService
    {
        Task<List<BookIssueViewDto>> GetAllAsync();
        Task<BookIssueViewDto> GetByIdAsync(int Bookid);
        Task<BookIssueDto> CreateAsync(BookIssueDto dto);
        Task<BookIssueDto> UpdateAsync(BookIssueDto dto);
        Task DeleteAsync(int id);
    }

        public interface IBookIssueHistoryViewService
        {
            Task<List<BookIssueHistoryViewDto>> GetAllAsync();
            Task<BookIssueHistoryViewDto> CreateAsync(BookIssueHistoryViewDto dto);
            Task<BookIssueHistoryViewDto> UpdateAsync(BookIssueHistoryViewDto dto);
            Task DeleteAsync(int id);
        }
        public interface IBookRequestService
        {
            Task<List<BookRequestDto>> GetAllAsync();
            Task<BookRequestDto> GetByIdAsync(int Bookid);
            Task<BookRequestDto> CreateAsync(BookRequestDto dto);
            Task<BookRequestDto> UpdateAsync(BookRequestDto dto);
            Task DeleteAsync(int id);
        }
}
using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Library
{
    public class BookIssueHistoryServiceServer : IBookIssueHistoryViewService
    {
        private readonly ApplicationDbContext _db;

        public BookIssueHistoryServiceServer(ApplicationDbContext db)
        {
            _db = db;
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
            var x=_db.BookIssueHIstorys.Count();
            var _List = await _db.BookIssueHIstorys.Select(c => new BookIssueHistoryViewDto
            {
                Id = c.Id,
                Book_Id = c.Book_Id,
                Issued_To = c.Issued_To,
                Issue_Date = c.Issue_Date,
                Due_Date = c.Due_Date,
                Return_Date = c.Return_Date,
                Fine = c.Fine,
                UserName = c.UserName,
                FullName = c.FullName,
                Title = c.Title,
                Author = c.Author,
            }).ToListAsync() ?? [];
            return _List;
        }

        public Task<BookIssueHistoryViewDto> UpdateAsync(BookIssueHistoryViewDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

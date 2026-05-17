using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BCA007.Services.Library
{
    public class BookIssueServiceServer : IBookIssueViewService
    {
        private readonly ApplicationDbContext _db;

        public BookIssueServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BookIssueViewDto>> GetAllAsync()
        {
            return await _db.BookIssues.Select(c => new BookIssueViewDto
            {
                Id = c.Id,
                Book_Id = c.Book_Id,
                Issued_Id = c.Issued_Id,
                Issue_Date = c.Issue_Date,
                Due_Date = c.Due_Date,
                Return_Date = c.Return_Date,
                FullName=c.UserName + " - " + c.FullName
               
            })
                .ToListAsync() ?? [];
        }
        public async Task<BookIssueViewDto> GetByIdAsync(int Bookid)
        {
            return await _db.BookIssues.Select(c => new BookIssueViewDto
            {
                Id = c.Id,
                Book_Id = c.Book_Id,
                Issued_Id = c.Issued_Id,
                Issue_Date = c.Issue_Date,
                Due_Date = c.Due_Date,
                Return_Date = c.Return_Date,
                FullName = c.UserName + " - " + c.FullName

            }).Where(c => c.Book_Id == Bookid).FirstOrDefaultAsync()?? new BookIssueViewDto();
              //  .ToListAsync() ?? [];
        }
        public async Task<BookIssueDto> CreateAsync(BookIssueDto dto)
        {
            if (await _db.BookIssue.AnyAsync(x => x.Id == dto.Id))
                throw new InvalidOperationException("BookIssue Publisher already exists");

            var entity = new BookIssueDto
            {
                Book_Id = dto.Book_Id,
                Issued_Id = dto.Issued_Id,
                Issue_Date = dto.Issue_Date,
                Due_Date = dto.Due_Date,
                Return_Date = dto.Return_Date
            };

            _db.BookIssue.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BookIssueDto> UpdateAsync(BookIssueDto dto)
        {
            var entity = await _db.BookIssue.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("BookIssue Publisher not found");

            if (entity.Book_Id != dto.Book_Id)
            {
                if (await _db.BookIssues.AnyAsync(x => x.Id == dto.Id))
                    throw new InvalidOperationException("BookIssue Publisher name already exists");
            }

            entity.Book_Id = dto.Book_Id;
            entity.Issued_Id = dto.Issued_Id;
            entity.Book_Id = dto.Book_Id;
            entity.Issued_Id = dto.Issued_Id;
            entity.Issue_Date = dto.Issue_Date;
            entity.Due_Date = dto.Due_Date;
            entity.Return_Date = dto.Return_Date;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BookIssue.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("BookIssue Publisher not found");

            _db.BookIssue.Remove(entity);
            await _db.SaveChangesAsync();
        }

        Task<List<BookIssueViewDto>> IBookIssueViewService.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

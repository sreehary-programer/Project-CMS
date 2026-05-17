using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Library
{
    public class BookPublisherServiceServer:IBookPublisherService
    {
        private readonly ApplicationDbContext _db;

        public BookPublisherServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BookPublisherDto>> GetAllAsync()
        {
            var _List = await _db.BookPublishers.Select(c => new BookPublisherDto
            {
                Id = c.Id,
                Publisher_Name = c.Publisher_Name,
                Publisher_Address = c.Publisher_Address
            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<BookPublisherDto> CreateAsync(BookPublisherDto dto)
        {
            if (await _db.BookPublishers.AnyAsync(x => x.Publisher_Name == dto.Publisher_Name))
                throw new InvalidOperationException("Book Publisher already exists");

            var entity = new BookPublisherDto
            {
                Publisher_Name = dto.Publisher_Name,
                Publisher_Address = dto.Publisher_Address
            };

            _db.BookPublishers.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BookPublisherDto> UpdateAsync(BookPublisherDto dto)
        {
            var entity = await _db.BookPublishers.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book Publisher not found");

            if (entity.Publisher_Name != dto.Publisher_Name)
            {
                if (await _db.BookPublishers.AnyAsync(x => x.Publisher_Name == dto.Publisher_Name))
                    throw new InvalidOperationException("Book Publisher name already exists");
            }

            entity.Publisher_Name = dto.Publisher_Name;
            entity.Publisher_Address = dto.Publisher_Address;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BookPublishers.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Publisher not found");

            _db.BookPublishers.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

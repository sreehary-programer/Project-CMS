using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Library
{
    public class BookTypeServiceServer:IBookTypeService
    {
        private readonly ApplicationDbContext _db;

        public BookTypeServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BookTypeDto>> GetAllAsync()
        {
            var _List = await _db.BookTypes.Select(c => new BookTypeDto
            {
                Id = c.Id,
                Type_Name = c.Type_Name
            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<BookTypeDto> CreateAsync(BookTypeDto dto)
        {
            if (await _db.BookTypes.AnyAsync(x => x.Type_Name == dto.Type_Name))
                throw new InvalidOperationException("Book Type already exists");

            var entity = new BookTypeDto
            {
                Type_Name = dto.Type_Name
            };

            _db.BookTypes.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BookTypeDto> UpdateAsync(BookTypeDto dto)
        {
            var entity = await _db.BookTypes.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book Type not found");

            if (entity.Type_Name != dto.Type_Name)
            {
                if (await _db.BookTypes.AnyAsync(x => x.Type_Name == dto.Type_Name))
                    throw new InvalidOperationException("Book Type name already exists");
            }

            entity.Type_Name = dto.Type_Name;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BookTypes.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Type not found");

            _db.BookTypes.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

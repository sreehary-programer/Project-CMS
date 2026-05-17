using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Library
{
    public class BookCategoryServiceServer : IBookCategoryService
    {
        private readonly ApplicationDbContext _db;

        public BookCategoryServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BookCategoryDto>> GetAllAsync()
        {
            var _List = await _db.BookCategories.Select(c => new BookCategoryDto
            {
                Id = c.Id,
                Category_Name = c.Category_Name
            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<BookCategoryDto> CreateAsync(BookCategoryDto dto)
        {
            if (await _db.BookCategories.AnyAsync(x => x.Category_Name == dto.Category_Name))
                throw new InvalidOperationException("Book Category already exists");

            var entity = new BookCategoryDto
            {
                Category_Name = dto.Category_Name
            };

            _db.BookCategories.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BookCategoryDto> UpdateAsync(BookCategoryDto dto)
        {
            var entity = await _db.BookCategories.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book Category not found");

            if (entity.Category_Name != dto.Category_Name)
            {
                if (await _db.BookCategories.AnyAsync(x => x.Category_Name == dto.Category_Name))
                    throw new InvalidOperationException("Book Category name already exists");
            }

            entity.Category_Name = dto.Category_Name;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BookCategories.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Category not found");

            _db.BookCategories.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

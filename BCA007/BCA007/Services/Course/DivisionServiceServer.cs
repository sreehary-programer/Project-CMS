using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Course
{
    public class DivisionServiceServer : IDivisionService
    {
        private readonly ApplicationDbContext _db;

        public DivisionServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<DivisionDto>> GetAllAsync()
        {
            var _List = await _db.Divisions.Select(c => new DivisionDto
            {
                Id = c.Id,
                Division_Name = c.Division_Name
            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<DivisionDto> CreateAsync(DivisionDto dto)
        {
            if (await _db.Divisions.AnyAsync(x => x.Division_Name == dto.Division_Name))
                throw new InvalidOperationException("Division already exists");

            var entity = new DivisionDto
            {
                Division_Name = dto.Division_Name
            };

            _db.Divisions.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<DivisionDto> UpdateAsync(DivisionDto dto)
        {
            var entity = await _db.Divisions.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Division not found");

            if (entity.Division_Name != dto.Division_Name)
            {
                if (await _db.Divisions.AnyAsync(x => x.Division_Name == dto.Division_Name))
                    throw new InvalidOperationException("Division name already exists");
            }

            entity.Division_Name = dto.Division_Name;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Divisions.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Division not found");

            _db.Divisions.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

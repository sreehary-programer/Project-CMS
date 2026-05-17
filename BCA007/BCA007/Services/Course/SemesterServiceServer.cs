using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Course
{
    public class SemesterServiceServer : ISemesterService
    {
        private readonly ApplicationDbContext _db;

        public SemesterServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<SemesterDto>> GetAllAsync()
        {
            var _List = await _db.Semesters.Select(c => new SemesterDto
            {
                Id = c.Id,
                Semester_Name = c.Semester_Name
            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<SemesterDto> CreateAsync(SemesterDto dto)
        {
            if (await _db.Semesters.AnyAsync(x => x.Semester_Name == dto.Semester_Name))
                throw new InvalidOperationException("Semester already exists");

            var entity = new SemesterDto
            {
                Semester_Name = dto.Semester_Name
            };

            _db.Semesters.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<SemesterDto> UpdateAsync(SemesterDto dto)
        {
            var entity = await _db.Semesters.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Semester not found");

            if (entity.Semester_Name != dto.Semester_Name)
            {
                if (await _db.Semesters.AnyAsync(x => x.Semester_Name == dto.Semester_Name))
                    throw new InvalidOperationException("Semester name already exists");
            }

            entity.Semester_Name = dto.Semester_Name;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Semesters.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Semester not found");

            _db.Semesters.Remove(entity);
            await _db.SaveChangesAsync();
        }

    }
}

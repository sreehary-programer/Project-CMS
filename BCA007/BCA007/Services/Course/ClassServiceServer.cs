using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BCA007.Services.Course
{
    public class ClassServiceServer : IClassService
    {
        private readonly ApplicationDbContext _db;
        public ClassServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<ClassViewDto>> GetAllAsync()
        {
            return await _db.ClasseView
                .AsNoTracking()
                .Select(c => new ClassViewDto
                {
                    Id = c.Id,
                    Class_Name = c.Class_Name,

                    Course_Id = c.Course_Id,
                    Batch_Id = c.Batch_Id,
                    Semester_Id = c.Semester_Id,
                    Division_Id = c.Division_Id,

                    Course_Name = c.Course_Name,
                    Batch_Name = c.Batch_Name,
                    Semester_Name = c.Semester_Name,
                    Division_Name = c.Division_Name,

                    Course_Code = c.Course_Code,
                    Duration = c.Duration

                }).ToListAsync() ?? [];
        }
        public async Task<ClassDto> CreateAsync(ClassDto dto)
        {
            if (dto.Course_Id is null ||
                dto.Batch_Id is null ||
                dto.Semester_Id is null ||
                dto.Division_Id is null)
            {
                throw new ValidationException("All fields are required.");
            }

            if (await _db.Classes.AnyAsync(x =>
               x.Course_Id == dto.Course_Id &&
               x.Batch_Id == dto.Batch_Id &&
               x.Semester_Id == dto.Semester_Id &&
               x.Division_Id == dto.Division_Id))
            {
                throw new InvalidOperationException("Class already exists.");
            }

            var entity = new ClassDto
            {
                Course_Id = dto.Course_Id,
                Batch_Id = dto.Batch_Id,
                Semester_Id = dto.Semester_Id,
                Division_Id = dto.Division_Id
            };

            try
            {
                _db.Classes.Add(entity);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Class already exists.");
            }

            dto.Id = entity.Id;
            return dto;
        }
        public async Task<ClassDto> UpdateAsync(ClassDto dto)
        {
            var entity = await _db.Classes.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Class not found");

            if (await _db.Classes.AnyAsync(x =>
                x.Course_Id == dto.Course_Id &&
                x.Batch_Id == dto.Batch_Id &&
                x.Semester_Id == dto.Semester_Id &&
                x.Division_Id == dto.Division_Id &&
                x.Id != dto.Id
                ))
            {
                throw new InvalidOperationException("Course code already exists");
            }

            entity.Course_Id = dto.Course_Id;
            entity.Batch_Id = dto.Batch_Id;
            entity.Semester_Id = dto.Semester_Id;
            entity.Division_Id = dto.Division_Id;

            _db.Classes.Update(entity);
            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Classes.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Class not found");

            _db.Classes.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

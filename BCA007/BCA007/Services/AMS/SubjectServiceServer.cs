using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.AMS;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.AMS
{
    public class SubjectServiceServer : ISubjectService
    {
        private readonly ApplicationDbContext _db;

        public SubjectServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SubjectDto> CreateAsync(SubjectDto dto)
        {
            if (await _db.Subject.AnyAsync(x => x.Subject_Name == dto.Subject_Name))
                throw new Exception("Course Name already Exists.");
            if (await _db.Subject.AnyAsync(x => x.Subject_Code == dto.Subject_Code))
                throw new Exception("Course Code already Exists.");


            var entity = new SubjectDto
            {
                Subject_Name = dto.Subject_Name,
                Subject_Code = dto.Subject_Code
            };
            _db.Subject.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Subject.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Subject not Found");

            _db.Subject.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<SubjectDto>> GetAllAsync()
        {
            var subjectlist = await _db.Subject.Select(c => new SubjectDto
            {
                Id = c.Id,
                Subject_Code = c.Subject_Code,
                Subject_Name = c.Subject_Name


            }).ToListAsync() ?? [];
            return subjectlist;
        }

        public async Task<SubjectDto> UpdateAsync(SubjectDto dto)
        {

            var entity = await _db.Subject.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Subject not Found");

            entity.Subject_Code = dto.Subject_Code;
            entity.Subject_Name = dto.Subject_Name;
            _db.Subject.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

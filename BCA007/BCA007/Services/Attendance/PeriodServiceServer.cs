using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.Attendance;
using BCA007.Shared.Service.Course;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Course
{
    public class PeriodServiceServer : IPeriodService
    {
        private readonly ApplicationDbContext _db;

        public PeriodServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<PeriodDto>> GetAllAsync()
        {
            var periodList = await _db.Period.Select(c => new PeriodDto
            {
                Id = c.Id,
                Period_Name = c.Period_Name,
            }).ToListAsync() ?? [];
            return periodList;
        }

        public async Task<PeriodDto> CreateAsync(PeriodDto dto)
        {
            if (await _db.Period.AnyAsync(x => x.Period_Name == dto.Period_Name))
                throw new InvalidOperationException("Period already exists");

            var entity = new PeriodDto
            {
                Period_Name = dto.Period_Name
            };

            _db.Period.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PeriodDto> UpdateAsync(PeriodDto dto)
        {
            var entity = await _db.Period.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Period not found");

            if (await _db.Period.AnyAsync(x => 
                x.Id != dto.Id
                ))
                    throw new InvalidOperationException("Time Duration already exists");
            

            entity.Period_Name = dto.Period_Name;

            await _db.SaveChangesAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Period.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Period not found");

            _db.Period.Remove(entity);
            await _db.SaveChangesAsync();
        }

        
    }
}

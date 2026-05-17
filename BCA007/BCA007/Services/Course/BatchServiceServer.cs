using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Course
{
    public class BatchServiceServer : IBatchService
    {
        private readonly ApplicationDbContext _db;

        public BatchServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BatchDto>> GetAllAsync()
        {
            var _List = await _db.Batchs.Select(c => new BatchDto
            {
                Id = c.Id,
                Batch_Name = c.Batch_Name,
                Duration = c.Duration
            }).ToListAsync() ?? [];
            return _List;
        }

        public async Task<BatchDto> CreateAsync(BatchDto dto)
        {
            if (await _db.Batchs.AnyAsync(x => x.Batch_Name == dto.Batch_Name))
                throw new InvalidOperationException("Batch already exists");

            var entity = new BatchDto
            {
                Duration = dto.Duration,
                Batch_Name = dto.Batch_Name
            };

            _db.Batchs.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BatchDto> UpdateAsync(BatchDto dto)
        {
            var entity = await _db.Batchs.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Batch not found");

            if (entity.Batch_Name != dto.Batch_Name)
            {
                if (await _db.Batchs.AnyAsync(x => x.Batch_Name == dto.Batch_Name))
                    throw new InvalidOperationException("Batch name already exists");
            }

            entity.Duration = dto.Duration;
            entity.Batch_Name = dto.Batch_Name;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Batchs.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Batch not found");

            _db.Batchs.Remove(entity);
            await _db.SaveChangesAsync();
        }

    }
}

using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.AMS
{
    public class ActivitiesServiceServer : IActivitiesService
    {
        private readonly ApplicationDbContext _db;

        public ActivitiesServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ActivitiesDto> CreateAsync(ActivitiesDto dto)
        {
            

            var entity = new ActivitiesDto
            {
                ActivityName = dto.ActivityName,
                Description = dto.Description
            };

            _db.Activities.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Activities.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Activity not found");
            var activity = await _db.Activity.AnyAsync(x => x.ActivityId == id);


            if (activity)
                throw new InvalidOperationException("Activity currently in use and cannot be deleted.");

            _db.Activities.Remove(entity);
            await _db.SaveChangesAsync();
        }
       
        public async Task<List<ActivitiesDto>> GetAllAsync()
        {
            var activityList = await _db.Activities.Select(c => new ActivitiesDto
            {
                Id = c.Id,
                ActivityName = c.ActivityName,
                Description = c.Description
            }).ToListAsync() ?? [];
            return activityList;
        }

        public async Task<ActivitiesDto> UpdateAsync(ActivitiesDto dto)
        {
            var entity = await _db.Activities.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Activity not found");

            if (await _db.Activities.AnyAsync(x =>
                x.ActivityName == dto.ActivityName &&
                x.Id != dto.Id
                ))
                throw new InvalidOperationException("Activity already exists");


            entity.ActivityName = dto.ActivityName;
            entity.Description = dto.Description;

            await _db.SaveChangesAsync();
            return dto;
        }
    }
}

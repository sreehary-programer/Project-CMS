using BCA007.Data;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.AMS
{
    public class ActivityServiceServer : IActivityService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ActivityServiceServer(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<ActivityDto> CreateAsync(ActivityDto dto)
        {
            if (await _db.Activity.AnyAsync(x => x.ActivityId == dto.ActivityId &&x.ClassId == dto.ClassId && x.PlannedDate == dto.PlannedDate))
                throw new InvalidOperationException("Activity already exists");

            var entity = new ActivityDto
            {
                ActivityId = dto.ActivityId,
                ClassId = dto.ClassId,
                PlannedDate = dto.PlannedDate,
                ConductedDate=dto.ConductedDate,

                PlannedTeacherId = dto.PlannedTeacherId,
                ConductedTeacherId = dto.ConductedTeacherId,

                PeriodId=dto.PeriodId,

                Feedback = dto.Feedback
            };

            _db.Activity.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Activity.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Type not found");

            _db.Activity.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ActivityViewDto>> GetAllAsync()
        {
            return await _db.ActivityView
               .Select(x => new ActivityViewDto
               {
                   Id = x.Id,
                   ActivityName = x.ActivityName,
                   Description = x.Description,
                   PlannedDate = x.PlannedDate,
                   ConductedDate = x.ConductedDate,
                   ActivityId = x.ActivityId,

                   IsConducted = x.IsConducted,
                   Feedback = x.Feedback,

                   ClassId = x.ClassId,
                   Class_Name = x.Class_Name,

                   PlannedTeacherId = x.PlannedTeacherId,
                   PlannedTeacherName = x.PlannedTeacherName,
                   PlannedTeacherEmail = x.PlannedTeacherEmail,
                   IsPlannedTeacherLocked = x.IsPlannedTeacherLocked,
                  
                   PeriodId = x.PeriodId,
                   Period_Name = x.Period_Name,

                   ConductedTeacherId = x.ConductedTeacherId,
                   ConductedTeacherName = x.ConductedTeacherName
               })
                   .ToListAsync();
        }

        public async Task<ActivityDto> UpdateAsync(ActivityDto dto)
        {
            var entity = await _db.Activity.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Activity record not found");

            entity.ActivityId = dto.ActivityId;
            entity.ClassId = dto.ClassId;
            entity.PlannedDate = dto.PlannedDate;
            entity.ConductedDate = dto.ConductedDate;
            entity.PlannedTeacherId = dto.PlannedTeacherId;
            entity.ConductedTeacherId = dto.ConductedTeacherId;
            entity.PeriodId = dto.PeriodId;
            entity.Feedback = dto.Feedback;

            await _db.SaveChangesAsync();
            return dto;

        }
    }
}

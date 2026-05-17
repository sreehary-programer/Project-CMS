using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.AMS;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.AMS
{
    public class TimeTableServiceServer : ITimeTableService
    {
        private readonly ApplicationDbContext _db;

        public TimeTableServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async  Task<TimetableDto> CreateAsync(TimetableDto dto)
        {
            //if (await _db.Timetables.AnyAsync(x => x.Course_Code == dto.Course_Code))
            //    throw new InvalidOperationException("Course code already exists");

            var entity = new TimetableDto
            {
                Id = dto.Id,
                ClassID = dto.ClassID,
                PeriodID = dto.PeriodID,
                SubjectID = dto.SubjectID,
                FacultyID = dto.FacultyID,
                Date = dto.Date

            };

            _db.Timetables.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;

        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Timetables.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Timetable not found");

            _db.Timetables.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TimetableViewDto>> GetAllAsync()
        {
            var timetableList = await _db.Timetable.Select(c => new TimetableViewDto
            {
                Id = c.Id,

                ClassID = c.ClassID,
                Class_Name = c.Class_Name,
                Date = c.Date,


                PeriodID = c.PeriodID,
                Period_Name = c.Period_Name,

                SubjectID = c.SubjectID,
                Subject_Name = c.Subject_Name,
                Subject_Code = c.Subject_Code,

                FacultyID = c.FacultyID,
                UserName = c.UserName,
                FullName = c.FullName
            }).ToListAsync() ?? [];
            return timetableList;
        }

        public async Task<TimetableDto> UpdateAsync(TimetableDto dto)
        {
            var entity = await _db.Timetables.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Timetable not found");


            entity.Id = dto.Id;

            entity.ClassID = dto.ClassID;
            entity.PeriodID = dto.PeriodID;
            entity.SubjectID = dto.SubjectID;
            entity.FacultyID = dto.FacultyID;

            entity.Date = dto.Date;


            await _db.SaveChangesAsync();
            return dto;
        }
    }
}

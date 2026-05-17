using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Attendance;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BCA007.Services.Attendance
{
    public class AttendEntryServiceServer : IAttendEntryService
    {
        private readonly ApplicationDbContext _db;
        public AttendEntryServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<AttendEntryViewDto>> GetAllAsync()
        {
            return await _db.AttendanceEntryView
                .AsNoTracking()
                .Select(c => new AttendEntryViewDto
                {
                    Id = c.Id,
                    Att_Form_Id = c.Att_Form_Id,
                    Date = c.Date,
                    UserName = c.UserName,
                    FullName = c.FullName,
                    Class_Name = c.Class_Name,
                    Att_1 = c.Att_1,
                    Att_2 = c.Att_2,
                    Att_3 = c.Att_3,
                    Att_4 = c.Att_4,
                    Att_5 = c.Att_5,
                    Student_Id = c.Student_Id,

                    Period1_Att_Id = c.Period1_Att_Id,
                    Period2_Att_Id = c.Period2_Att_Id,
                    Period3_Att_Id = c.Period3_Att_Id,
                    Period4_Att_Id = c.Period4_Att_Id,
                    Period5_Att_Id = c.Period5_Att_Id,
                }).ToListAsync() ?? [];
        }
        public async Task<AttendEntryDto> CreateAsync(AttendEntryDto dto)
        {
            if (
               dto.Att_Form_Id is null ||
                dto.Period1_Att_Id is null ||
                dto.Period2_Att_Id is null ||
                dto.Period3_Att_Id is null ||
                dto.Period4_Att_Id is null ||
                dto.Period5_Att_Id is null )
            {
                throw new ValidationException("All fields are required.");
            }

            //if (await _db.AttendanceForm.AnyAsync(x =>
            //   x.Class_Id == dto.Class_Id &&
            //   x.Period1_DefId == dto.Period1_DefId &&
            //   x.Period1_StaffId == dto.Period1_StaffId &&
            //   x.Period2_DefId == dto.Period2_DefId &&
            //   x.Period2_StaffId == dto.Period2_StaffId &&
            //   x.Period3_DefId == dto.Period3_DefId &&
            //   x.Period3_StaffId == dto.Period3_StaffId &&
            //   x.Period4_DefId == dto.Period4_DefId &&
            //   x.Period4_StaffId == dto.Period4_StaffId &&
            //   x.Period5_DefId == dto.Period5_DefId &&
            //   x.Period5_StaffId == dto.Period5_StaffId ))
            //{
            //    throw new InvalidOperationException("Attendance already exists.");
            //}

            var entity = new AttendEntryDto
            {
                Att_Form_Id = dto.Att_Form_Id,
                //Date = dto.Date,
                Period1_Att_Id = dto.Period1_Att_Id,
                Period2_Att_Id = dto.Period2_Att_Id,
                Period3_Att_Id = dto.Period3_Att_Id,
                Period4_Att_Id = dto.Period4_Att_Id,
                Period5_Att_Id = dto.Period5_Att_Id,
                Student_Id = dto.Student_Id
            };

           
          
                _db.AttendanceEntry.Add(entity);
                await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }
       
        public async Task<AttendEntryDto> UpdateAsync(AttendEntryDto dto)
        {
            var entity = await _db.AttendanceEntry.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Entry not found");

            if (await _db.AttendanceEntry.AnyAsync(x =>
                x.Att_Form_Id == dto.Att_Form_Id &&
                //x.Date == dto.Date &&
                x.Period1_Att_Id == dto.Period1_Att_Id &&
                x.Period2_Att_Id == dto.Period2_Att_Id &&
                x.Period3_Att_Id == dto.Period3_Att_Id &&
                x.Period4_Att_Id == dto.Period4_Att_Id &&
                x.Period5_Att_Id == dto.Period5_Att_Id && 
                x.Student_Id == dto.Student_Id

                ))
            {
                throw new InvalidOperationException("Attendance Id already exists");
            }

            entity.Att_Form_Id = dto.Att_Form_Id;
            //entity.Date = dto.Date;
            entity.Period1_Att_Id = dto.Period1_Att_Id;
            entity.Period2_Att_Id = dto.Period2_Att_Id;
            entity.Period3_Att_Id = dto.Period3_Att_Id;
            entity.Period4_Att_Id = dto.Period4_Att_Id;
            entity.Period5_Att_Id = dto.Period5_Att_Id;
            entity.Student_Id = dto.Student_Id;

            _db.AttendanceEntry.Update(entity);
            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.AttendanceEntry.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Attendance Entry not found");

            _db.AttendanceEntry.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<AttendEntryViewDto>> GetAttendanceById(int? FormId)
        {
            return await _db.AttendanceEntryView
                .AsNoTracking()
                .Where(r => r.Att_Form_Id == FormId)
                .Select(c => new AttendEntryViewDto
                {
                    Id = c.Id,
                    Att_Form_Id = c.Att_Form_Id,
                    Date = c.Date,
                    UserName = c.UserName,
                    FullName = c.FullName,
                    Class_Name = c.Class_Name,
                    Att_1 = c.Att_1,
                    Att_2 = c.Att_2,
                    Att_3 = c.Att_3,
                    Att_4 = c.Att_4,
                    Att_5 = c.Att_5,
                    Student_Id = c.Student_Id,
                    Period1_Att_Id = c.Period1_Att_Id,
                    Period2_Att_Id = c.Period2_Att_Id,
                    Period3_Att_Id = c.Period3_Att_Id,
                    Period4_Att_Id = c.Period4_Att_Id,
                    Period5_Att_Id = c.Period5_Att_Id
                })
                .ToListAsync();
        }

    }
}

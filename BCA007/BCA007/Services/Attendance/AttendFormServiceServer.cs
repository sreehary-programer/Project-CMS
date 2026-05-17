using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Attendance;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BCA007.Services.Attendance
{
    public class AttendFormServiceServer : IAttendFormService
    {
        private readonly ApplicationDbContext _db;
        public AttendFormServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<AttendFormViewDto>> GetAllAsync()
        {
            return await _db.AttendanceFormView
                .AsNoTracking()
                .Select(c => new AttendFormViewDto
                {
                    Id = c.Id,
                    Class_Id = c.Class_Id,
                    Class_Name = c.Class_Name,
                    Date = c.Date,
                    Period1_DefId = c.Period1_DefId,
                    Period1_StaffId = c.Period1_StaffId,
                    Teacher_Name1 = c.Teacher_Name1,
                    Att_1 = c.Att_1,
                    Period2_DefId = c.Period2_DefId,
                    Period2_StaffId = c.Period2_StaffId,
                    Teacher_Name2 = c.Teacher_Name2,
                    Att_2 = c.Att_2,
                    Period3_DefId = c.Period3_DefId,
                    Period3_StaffId = c.Period3_StaffId,
                    Teacher_Name3 = c.Teacher_Name3,
                    Att_3 = c.Att_3,
                    Period4_DefId = c.Period4_DefId,
                    Period4_StaffId = c.Period4_StaffId,
                    Teacher_Name4 = c.Teacher_Name4,
                    Att_4 = c.Att_4,
                    Period5_DefId = c.Period5_DefId,
                    Period5_StaffId = c.Period5_StaffId,
                    Teacher_Name5 = c.Teacher_Name5,
                    Att_5 = c.Att_5,

                }).ToListAsync() ?? [];
        }
       
        public async Task<AttendFormDto> UpdateAsync(AttendFormDto dto)
        {
            if (
                dto.Class_Id is null ||
                dto.Period1_DefId is null || dto.Period1_StaffId is null ||
                dto.Period2_DefId is null || dto.Period2_StaffId is null ||
                dto.Period3_DefId is null || dto.Period3_StaffId is null ||
                dto.Period4_DefId is null || dto.Period4_StaffId is null ||
                dto.Period5_DefId is null || dto.Period5_StaffId is null
            )
            {
                throw new ValidationException("All fields are required.");
            }
            if (await _db.AttendanceForm.AnyAsync(x =>
                x.Class_Id == dto.Class_Id &&
                x.Date == dto.Date &&
                x.Id != dto.Id))
            {
                throw new InvalidOperationException("Attendance already exists.");
            }
            var existing = await _db.AttendanceForm
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            bool classChanged = existing != null && existing.Class_Id != dto.Class_Id;
            var prmId = new SqlParameter("@prmId", SqlDbType.Int)
            {
                Direction = ParameterDirection.InputOutput,
                Value = dto.Id > 0 ? dto.Id : -1
            };

            var prmMessage = new SqlParameter("@prmMessage", SqlDbType.NVarChar, 250)
            {
                Direction = ParameterDirection.Output
            };

            await _db.Database.ExecuteSqlRawAsync(
                @"EXEC dbo.UpdateAttendanceFormTable
            @prmDate,
            @prmClass_Id,

            @prmPeriod1_DefId,
            @prmPeriod2_DefId,
            @prmPeriod3_DefId,
            @prmPeriod4_DefId,
            @prmPeriod5_DefId,

            @prmPeriod1_StaffId,
            @prmPeriod2_StaffId,
            @prmPeriod3_StaffId,
            @prmPeriod4_StaffId,
            @prmPeriod5_StaffId,

            @prmId OUTPUT,
            @prmMessage OUTPUT",

                new SqlParameter("@prmDate", dto.Date ?? (object)DBNull.Value),
                new SqlParameter("@prmClass_Id", dto.Class_Id),

                new SqlParameter("@prmPeriod1_DefId", dto.Period1_DefId),
                new SqlParameter("@prmPeriod2_DefId", dto.Period2_DefId),
                new SqlParameter("@prmPeriod3_DefId", dto.Period3_DefId),
                new SqlParameter("@prmPeriod4_DefId", dto.Period4_DefId),
                new SqlParameter("@prmPeriod5_DefId", dto.Period5_DefId),

                new SqlParameter("@prmPeriod1_StaffId", dto.Period1_StaffId),
                new SqlParameter("@prmPeriod2_StaffId", dto.Period2_StaffId),
                new SqlParameter("@prmPeriod3_StaffId", dto.Period3_StaffId),
                new SqlParameter("@prmPeriod4_StaffId", dto.Period4_StaffId),
                new SqlParameter("@prmPeriod5_StaffId", dto.Period5_StaffId),

                prmId,
                prmMessage
            );

            dto.Id = (int)prmId.Value;

            var message = prmMessage.Value?.ToString();
            if (!string.IsNullOrEmpty(message))
                throw new InvalidOperationException(message);

            return dto;
        }

       
        public async Task DeleteAsync(int id)
        {

            var entity = await _db.AttendanceForm.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Attendance Form not found");
            var attend = await _db.AttendanceEntry.AnyAsync(x => x.Att_Form_Id == id);
            if (attend)
                throw new InvalidOperationException("Attendance currently in use and cannot be deleted.");
            _db.AttendanceForm.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

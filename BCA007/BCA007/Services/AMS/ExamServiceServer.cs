using System.Data;
using BCA007.Data;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static MudBlazor.CategoryTypes;

namespace BCA007.Services.AMS
{
    public class ExamServiceServer : IExamService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ExamServiceServer(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<ExamTimeTableDto> UpdateAsync(ExamTimeTableDto dto)
        {
            //if (await _db.ExamTimeTable.AnyAsync(x => x.Date == dto.Date && x.Class_Id == dto.Class_Id  && x.Subject_Id == dto.Subject_Id))
            //    throw new InvalidOperationException("Exam already exists");

            try
            {
                var dateparam = new SqlParameter("@prmDate", dto.Date);
                var classparam = new SqlParameter("@prmClass_Id", dto.Class_Id);
                var subjectparam = new SqlParameter("@prmSubject_Id", dto.Subject_Id);
                var timeparam = new SqlParameter("@prmTime", dto.Time ?? (object)DBNull.Value)
                {
                    SqlDbType = SqlDbType.Time
                };
                var sessionparam = new SqlParameter("@prmSession_Id", dto.Session_Id);
                var examparam = new SqlParameter("@prmExamType_Id", dto.ExamType_Id);
                var markparam = new SqlParameter("@prmMax_Mark", dto.Max_Mark);
                var idParam = new SqlParameter("@prmId", dto.Id)
                {
                    Direction = ParameterDirection.InputOutput
                };
                var messageParam = new SqlParameter("@prmMessage", "")
                {
                    Direction = ParameterDirection.Output
                };
                _db.Database.ExecuteSqlRaw("EXEC UpdateExamTimeTable @prmDate, @prmClass_Id,@prmSubject_Id,@prmTime,@prmSession_Id,@prmExamType_Id,@prmMax_Mark, @prmId OUTPUT, @prmMessage OUTPUT", dateparam, classparam, subjectparam, timeparam, sessionparam, examparam, markparam, idParam, messageParam);
                await _db.SaveChangesAsync();
                var message = (string?)messageParam.Value;

                if (!string.IsNullOrEmpty(message))
                    throw new InvalidOperationException(message);

                dto.Id = (int)idParam.Value;
                return dto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }

        }
        //public async Task<ExamTimeTableDto> CreateAsync(ExamTimeTableDto dto)
        //{
        //    if (await _db.ExamTimeTable.AnyAsync(x => x.Date == dto.Date && x.Class_Id == dto.Class_Id && x.Time == dto.Time && x.Subject_Id == dto.Subject_Id))
        //        throw new InvalidOperationException("Exam already exists");

        //    var entity = new ExamTimeTableDto
        //    {
        //        Date = dto.Date,
        //        Class_Id = dto.Class_Id,
        //        Time = dto.Time,
        //        Subject_Id = dto.Subject_Id,
        //        ExamType_Id=dto.ExamType_Id,
        //        Session_Id = dto.Session_Id
        //    };

        //    _db.ExamTimeTable.Add(entity);
        //    await _db.SaveChangesAsync();

        //    dto.Id = entity.Id;
        //    return dto;
        //}

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.ExamTimeTable.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Exam not found");
            var exam = await _db.Result.AnyAsync(x => x.Exam_Id == id);


            if (exam)
                throw new InvalidOperationException("Exam currently in use and cannot be deleted.");
            _db.ExamTimeTable.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ExamTimeTableViewDto>> GetAllAsync()
        {
            return await _db.ExamTimeTableView
               .Select(x => new ExamTimeTableViewDto
               {
                   Id = x.Id,
                   Date = x.Date,
                   Time = x.Time,
                   SessionName = x.SessionName,
                   Subject_Name = x.Subject_Name,
                   Class_Name = x.Class_Name,
                   Class_Id = x.Class_Id,
                   Subject_Id = x.Subject_Id,
                   Exam_Type=x.Exam_Type,
                   Session_Id = x.Session_Id,
                   ExamType_Id=x.ExamType_Id,
                   Max_Mark=x.Max_Mark
               })
                   .ToListAsync();
        }

        //public async Task<ExamTimeTableDto> xUpdateAsync(ExamTimeTableDto dto)
        //{
        //    var entity = await _db.ExamTimeTable.FindAsync(dto.Id);
        //    if (entity == null)
        //        throw new KeyNotFoundException("Exam not found");

        //    entity.Date = dto.Date;
        //    entity.Class_Id = dto.Class_Id;
        //    entity.Time = dto.Time;
        //    entity.Subject_Id = dto.Subject_Id;
        //    entity.Session_Id = dto.Session_Id;
            

        //    await _db.SaveChangesAsync();
        //    return dto;
        //}

        
    }
}

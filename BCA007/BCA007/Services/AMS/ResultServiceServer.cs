using BCA007.Data;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.AMS
{
    public class ResultServiceServer : IResultService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ResultServiceServer(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task UpdateResults(List<ResultViewDto> dtoList)
        {
            foreach (var item in dtoList)
            {
                var entity = await _db.Result.FindAsync(item.Id);

                if (entity != null)
                {
                    entity.Marks = item.Marks;

                    // DO NOT update Exam_Id
                    // DO NOT update Student_Id
                }
            }

            await _db.SaveChangesAsync();
        }



        public async Task<List<ResultViewDto>> GetAllAsync()
        {
            return await _db.ResultView
                          .Select(x => new ResultViewDto
                          {
                              Id = x.Id,
                              Exam_Id=x.Exam_Id,
                              Date = x.Date,
                              Student_Id = x.Student_Id,
                              Marks = x.Marks,
                              Class_Name = x.Class_Name,
                              UserName = x.UserName,
                              Exam_Type = x.Exam_Type,
                              FullName = x.FullName,
                              Subject_Name=x.Subject_Name,
                              Subject_Code=x.Subject_Code,
                              Max_Mark=x.Max_Mark

                          }).ToListAsync();
        }

        public async Task<List<ResultViewDto>> GetResultsByExamId(int? examId)
        {
            var query = _db.ResultView
                .AsNoTracking()
                .AsQueryable();

            if (examId > 0)
            {
                query = query.Where(r => r.Exam_Id == examId);
            }

            return await query
                .Select(r => new ResultViewDto
                {
                    Id = r.Id,
                    Exam_Id = r.Exam_Id,
                    Class_Name = r.Class_Name,
                    FullName = r.FullName,
                    Date = r.Date,
                    Exam_Type = r.Exam_Type,
                    Subject_Name = r.Subject_Name,
                    Subject_Code = r.Subject_Code,
                    Marks = r.Marks,
                    Max_Mark = r.Max_Mark
                })
                .ToListAsync();

            //return await _db.ResultView
            //    .AsNoTracking()
            //    .Where(r => r.Exam_Id == examId)
            //    .Select(r => new ResultViewDto
            //    {
            //        Id = r.Id,
            //        Class_Name = r.Class_Name,
            //        FullName = r.FullName,
            //        Date = r.Date,
            //        Exam_Type = r.Exam_Type,
            //        Subject_Name = r.Subject_Name,
            //        Subject_Code = r.Subject_Code,
            //        Marks = r.Marks,
            //        Max_Mark = r.Max_Mark
            //    })
            //    .ToListAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Result.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Result not Found");

            _db.Result.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

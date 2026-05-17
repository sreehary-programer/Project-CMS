using System.Data;
using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Course
{
    public class CourseServiceServer : ICourseService
    {
        private readonly ApplicationDbContext _db;

        public CourseServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courseList = await _db.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Course_Name = c.Course_Name,
                Course_Code = c.Course_Code
            }).ToListAsync() ?? []; 
            return courseList;
        }

        public async Task<CourseDto> CreateAsync(CourseDto dto)
        {
            if (await _db.Courses.AnyAsync(x => x.Course_Code == dto.Course_Code))
                throw new InvalidOperationException("Course code already exists");

            //var entity = new CourseDto
            //{
            //    Course_Code = dto.Course_Code,
            //    Course_Name = dto.Course_Name
            //};

            //_db.Courses.Add(entity);
            var codeparam = new SqlParameter("@ParamCourse_Code", dto.Course_Code);
            var courseparam = new SqlParameter("@ParamCourse_Name", dto.Course_Name);
            var idParam = new SqlParameter("@Param_Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var messageParam = new SqlParameter("@param_Message", SqlDbType.NVarChar, 250)
            {
                Direction = ParameterDirection.Output
            };
            _db.Database.ExecuteSqlRaw("EXEC SaveCourse @ParamCourse_Code, @ParamCourse_Name, @Param_Id OUTPUT, @param_Message OUTPUT", codeparam, courseparam, idParam, messageParam);
            //_db.Courses.Add(entity);
            await _db.SaveChangesAsync();
            var message = (string)messageParam.Value;

            if (message == null && message.Length < 0)
                throw new InvalidOperationException(message);

            dto.Id = (int)idParam.Value;
            return dto;
        }

        public async Task<CourseDto> UpdateAsync(CourseDto dto)
        {
            var entity = await _db.Courses.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Course not found");

            if (await _db.Courses.AnyAsync(x => 
                x.Course_Code == dto.Course_Code &&
                x.Id != dto.Id
                ))
                    throw new InvalidOperationException("Course code already exists");
            

            entity.Course_Code = dto.Course_Code;
            entity.Course_Name = dto.Course_Name;

            await _db.SaveChangesAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Courses.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Course not found");

            _db.Courses.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetIntegerValue()
        {
            return await Task.FromResult(await _db.Courses.CountAsync());
        }

        public async Task<string> GetStringValue()
        {
            return "I am from Server Service";
        }
    }
}

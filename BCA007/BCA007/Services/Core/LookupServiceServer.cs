using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Core
{
    public class LookupServiceServer : ILookupService
    {
        private readonly ApplicationDbContext _db;

        public LookupServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<LookupItemDto>> GetClassesAsync()
            => await _db.ClasseView
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Class_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
        public async Task<List<LookupItemDto>> GetGendersAsync()
            => await _db.Genderss
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Gender_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
        public async Task<List<LookupItemDto>> GetCoursesAsync()
            => await _db.Courses
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Course_Code + " - " + x.Course_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetBatchesAsync()
            => await _db.Batchs
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Batch_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetSemestersAsync()
            => await _db.Semesters
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Semester_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetDivisionsAsync()
            => await _db.Divisions
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Division_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        //book lookups
        public async Task<List<LookupItemDto>> GetBookTypesAsync()
            => await _db.BookTypes
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Type_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
        public async Task<List<LookupItemDto>> GetBookPublishersAsync()
            => await _db.BookPublishers
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Publisher_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
        public async Task<List<LookupItemDto>> GetBookCategorysAsync()
            => await _db.BookCategories
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Category_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
        public async Task<List<LookupItemDto>> GetLanguageAsync()
            => await _db.Languagess
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Language_Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetRoleAsync()
         => await _db.Roles
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.Name
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        public async Task<List<LookupItemDto>> GetParentAsync()
         => await _db.ParentView
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.UserName +"-"+ x.FullName
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        public async Task<List<LookupItemDto>> GetActivityAsync()
         => await _db.Activities
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.ActivityName
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        public async Task<List<LookupItemDto>> GetTeacherAsync()
         => await _db.Teacher
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.UserName+"-"+x.FullName
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        public async Task<List<LookupItemDto>> GetPeriodAsync()
         => await _db.Period
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.Period_Name
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();
       

        

        public async Task<List<LookupItemDto>> GetSubjetAsync()

            => await _db.Subject
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Subject_Name + "-" + x.Subject_Code
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

       
        public async Task<List<LookupItemDto>> GetSessionAsync()
         => await _db.Session
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.SessionName
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetExamTypeAsync()
        => await _db.ExamType
                .AsNoTracking()
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = x.Exam_Type
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

        public async Task<List<LookupItemDto>> GetStudentAsync()
        => await _db.StudentsView
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.UserName + "-" + x.FullName
                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        public async Task<List<LookupItemDto>> GetUsersAsync()
        => await _db.Users
                 .AsNoTracking()
                 .Select(x => new LookupItemDto
                 {
                     Id = x.Id,
                     Text = x.UserName + " - " + x.FullName

                 })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

        //hostal
        public async Task<List<LookupItemDto>> GetTypeAsync()
        => await _db.Type
               .AsNoTracking()
               .Select(x => new LookupItemDto
               {
                   Id = x.Id,
                   Text = x.Type
               })
               .OrderBy(x => x.Text)
               .ToListAsync();
        public async Task<List<LookupItemDto>> GetHostalRoomAsync()
        => await _db.Hostal
            .AsNoTracking()
            .Select(x => new LookupItemDto
            {
                Id = x.Id,
                Text = x.Hostel_Name
            })
            .OrderBy(x => x.Text)
            .ToListAsync();

        public async Task<List<LookupItemDto>> GetHostalStudentRoomAsync()
        => await _db.HostalView
            .AsNoTracking()
            .Select(x => new LookupItemDto
            {
                Id = x.Id,
                Text = x.Hostel_FullName
            })
            .OrderBy(x => x.Text)
            .ToListAsync();
        public async Task<List<LookupItemDto>> GetAttendDefaultAsync()
                 => await _db.AttendanceDefault
                        .AsNoTracking()
                        .Select(x => new LookupItemDto
                        {
                            Id = x.Id,
                            Text = x.Attendance_Name
                        })
                        .OrderBy(x => x.Text)
                        .ToListAsync();

    }
}

using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Student
{
    public class StudentProfileServer : IStudentProfileService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _context;
        private readonly NavigationManager _navMgr;

        public StudentProfileServer(ApplicationDbContext db
                , IHttpContextAccessor context, NavigationManager navMgr)
        {
            _db = db;
            _context = context;
            _navMgr = navMgr;
        }

        public async Task<List<StudentViewProfileDto>> GetAllAsync()
        {
            try
            {
                var user = _context.HttpContext?.User;
                if (user == null || !user.Identity.IsAuthenticated)
                    _navMgr.NavigateTo("/Account/Login", true);
                //throw new InvalidOperationException("User is not authenticated");
                string? usname = user.Identity.Name;

                if (usname == null) throw new InvalidOperationException("Invalid login");
                List<StudentViewProfileDto> lst = new List<StudentViewProfileDto>();
                lst = await _db.StudentViewProfile
                    .Select(x => new StudentViewProfileDto
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        FullName = x.FullName,
                        PhoneNumber = x.PhoneNumber,
                        Class_Id = x.Class_Id,
                        Gender_Id = x.Gender_Id,
                        ProfileURL = x.ProfileURL,
                        Class_Name = x.Class_Name,
                        Gender_Name = x.Gender_Name,
                        DateOfBirth = x.DateOfBirth,
                        IsLocked = x.IsLocked,
                        Parent_Name = x.Parent_Name,
                        Division_Name=x.Division_Name,
                        Semester_Name=x.Semester_Name,
                        Course_Code=x.Course_Code,
                        Course_Name=x.Course_Name,
                        Batch_Name=x.Batch_Name,
                        Duration=x.Duration
                    })
                    .Where(x => x.UserName == usname || x.Parent_Name == usname)
                    .ToListAsync();

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public async Task<List<HostelViewDto>> GetAllHostelAsync(int stdId)
        {
            //var user = _context.HttpContext?.User;
            //string? usname = user.Identity.Name;
            //if (usname == null) throw new InvalidOperationException("Invalid login");

            var resp = await _db.HostelView
                .Select(x => new HostelViewDto
                {
                    Student_Id = x.Student_Id,
                   
                    Hostel_FullName = x.Hostel_FullName,
                    UserName=x.UserName,
                    Warden_Name=x.Warden_Name,
                    Room_Number=x.Room_Number,
                    Hostel_Name = x.Hostel_Name,
                })
                .Where(x => x.Student_Id == stdId)
                .ToListAsync();
            return resp;
        }

        public async Task<List<TimetablesViewDto>> GetAllTimetableAsync(int stdId)
        { 

            var resp = await _db.TimetableView
                .Select(x => new TimetablesViewDto
                {
                    Id = x.Id,

                    Class_Name = x.Class_Name,
                    Subject_Name = x.Subject_Name,
                    Period_Name = x.Period_Name,
                    Staff_Name = x.Staff_Name,
                    Date = x.Date,
                    Student_UserName = x.Student_UserName,
                    Parent_Name = x.Parent_Name,
                    Student_Id = x.Student_Id

                })
                .Where(x => x.Student_Id == stdId && x.Date == DateTime.Now.Date)
                .ToListAsync();
            return resp;

        }
        public async Task<List<BusViewDto>> GetAllBusAsync(int stdId)
        {
            var resp = await _db.BusView
                .Select(x => new BusViewDto
                {
                    Id = x.Id,

                    Bus_Name = x.Bus_Name,
                    Route_Name = x.Route_Name,
                    UserName=x.UserName,
                    Route_Price = x.Route_Price,
                    Parent_Name = x.Parent_Name
                })
                .Where(x => x.Id == stdId)
                .ToListAsync();
            return resp;

        }
        public async Task<List<BookHistoryViewDto>> GetAllBookHistoryAsync(int stdId)
        {
            var resp = await _db.BookHistoryView
                .Select(x => new BookHistoryViewDto
                {
                    Id = x.Id,

                    Title = x.Title,
                    UserName = x.UserName,
                    Return_Date=x.Return_Date,
                    Issued_To=x.Issued_To,
                })
                .Where(x => x.Issued_To == stdId)
                .ToListAsync();
            return resp;

        }

        public async Task<List<AttendanceViewDto>> GetAllAttendanceAsync(int stdId)
        {
            var resp = await _db.AttendanceView
                .Select(x => new AttendanceViewDto
                {
                    Id = x.Id,

                    UserName= x.UserName,
                    Parent_Name = x.Parent_Name,
                    Date = x.Date,
                    Att_1 = x.Att_1,
                    Att_2 = x.Att_2,
                    Att_3 = x.Att_3,
                    Att_4 = x.Att_4,
                    Att_5 = x.Att_5,
                    Student_Id = x.Student_Id,
                    
                })
                .Where(x => x.Student_Id == stdId)
          
                .ToListAsync();
            return resp;
        }

        public async Task<List<AttendancePersentageViewDto>> GetAllPersentageAsync(int stdId)
        {
            var resp = await _db.AttendancePersentageView
                .Select(x => new AttendancePersentageViewDto
                {
                    Id = x.Id,

                    UserName = x.UserName,
                    Parent_Name = x.Parent_Name,
                    TotalPresent = x.TotalPresent,
                    TotalAbsent = x.TotalAbsent,
                    AttendancePercentage = x.TotalPresent*100/(x.TotalPresent+x.TotalAbsent) 
                })
                .Where(x => x.Id == stdId)

                .ToListAsync();
            return resp;
        }

        public async Task<List<ResultsViewDto>> GetAllResultAsync(int stdId)
        {
            var resp = await _db.ResultViews
                .Select(x => new ResultsViewDto
                {
                    Id = x.Id,

                    UserName = x.UserName,
                    Parent_Name = x.Parent_Name,
                    Date = x.Date,
                    Subject_Name = x.Subject_Name,
                    Exam_Type = x.Exam_Type,
                    Obtained_Mark = x.Obtained_Mark,
                    Max_Mark = x.Max_Mark,
                    Student_Id=x.Student_Id
                })
                .Where(x => x.Student_Id == stdId)

                .ToListAsync();
            return resp;
        }

        public async Task<List<FeeDto>> GetAllFeeAsync(int stdId)
        {
            try
            {
                var resp = await _db.Fee
                    .Select(x => new FeeDto
                    {
                        Id = x.Id,
                        TotalAmount = x.TotalAmount,
                        Status = x.Status,
                        Fee_Type_Name = x.Fee_Type_Name,
                        Student_Id = x.Student_Id,
                    })
                    .Where(x => x.Student_Id == stdId)

                    .ToListAsync();
                return resp;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

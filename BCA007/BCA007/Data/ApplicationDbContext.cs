using System.Diagnostics;
using System.Reflection.Emit;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.DTOs.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser
        ,ApplicationRole, int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)
    {
        internal IEnumerable<object> BookIssueHistory;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseDto>(entity =>
            {
                entity.ToTable("T_Course").HasKey("Id"); // legacy table name
            });
            builder.Entity<BatchDto>(entity =>
            {
                entity.ToTable("T_Batch").HasKey("Id"); // legacy table name
            });
            builder.Entity<SemesterDto>(entity =>
            {
                entity.ToTable("T_Semester").HasKey("Id"); // legacy table name
            });
            builder.Entity<DivisionDto>(entity =>
            {
                entity.ToTable("T_Division").HasKey("Id"); // legacy table name
            });
            builder.Entity<ClassDto>(entity =>
            {
                entity.ToTable("T_Class").HasKey("Id"); // legacy table name
            });
            builder.Entity<ClassViewDto>(entity =>
            {
                entity.ToView("V_Class").HasKey("Id"); // legacy view name
            });
            builder.Entity<GenderDto>(entity =>
            {
                entity.ToTable("T_Gender").HasKey("Id"); // legacy view name
            });

            builder.Entity<StudentViewDto>(entity =>
            {
                entity.ToView("V_Studentses").HasKey("Id"); // legacy view name
            });
            builder.Entity<ParentViewDto>(entity =>
            {
                entity.ToView("V_Parent").HasKey("Id"); // legacy view name
            });
            builder.Entity<AdminViewDto>(entity =>
            {
                entity.ToView("V_Admin").HasKey("Id"); // legacy view name
            });
            builder.Entity<StaffViewDto>(entity =>
            {
                entity.ToView("V_Staff").HasKey("Id"); // legacy view name
            });
            //lookup

            builder.Entity<RoleDto>(entity =>
            {
                entity.ToView("V_Roles").HasKey("Id"); // legacy view name
            });

            //library datasets
            builder.Entity<BookViewDto>(entity =>
            {
                entity.ToView("V_Books").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookDto>(entity =>
            {
                entity.ToTable("T_Book").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookCategoryDto>(entity =>
            {
                entity.ToTable("T_BookCategory").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookPublisherDto>(entity =>
            {
                entity.ToTable("T_BookPublisher").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookTypeDto>(entity =>
            {
                entity.ToTable("T_BookType").HasKey("Id"); // legacy view name
            });
            builder.Entity<LanguageDto>(entity =>
            {
                entity.ToTable("T_Language").HasKey("Id"); // legacy view name
            });

            builder.Entity<BookIssueViewDto>(entity =>
            {
                entity.ToView("V_BookIssue").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookIssueDto>(entity =>
            {
                entity.ToTable("T_BookIssue").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookIssueHistoryViewDto>(entity =>
            {
                entity.ToView("V_BookIssue_Historys").HasKey("Id"); // legacy view name
            });
            builder.Entity<BookRequestDto>(entity =>
            {
                entity.ToTable("T_Book_Request").HasKey("Id"); // legacy view name
            });

            //SMS
            builder.Entity<StudentViewProfileDto>(entity =>
            {
                entity.ToView("V_P_Students").HasKey("Id"); // legacy view name
            });
            //builder.Entity<StaffViewDto>(entity =>
            //{
            //    entity.ToView("V_Staff").HasKey("Id"); // legacy view name
            //});
            //builder.Entity<StaffViewProfileDto>(entity =>
            //{
            //    entity.ToView("V_P_Staff").HasKey("Id"); // legacy view name
            //});

            //hostel view
            builder.Entity<HostelViewDto>(entity =>
            {
                entity.ToView("V_Hostel").HasKey("Student_Id"); // legacy view name
            });
            builder.Entity<TimetablesViewDto>(entity =>
            {
                entity.ToView("V_Timetables").HasKey("Id"); // legacy view name
            });


            //bus route
            builder.Entity<BusViewDto>(entity =>
            {
                entity.ToView("V_BusRoute_Students").HasKey("Id"); // legacy view name
            });

            //Book
            builder.Entity<BookHistoryViewDto>(entity =>
            {
                entity.ToView("V_BookIssue_History").HasKey("Id"); // legacy view name
            });

            builder.Entity<AttendanceViewDto>(entity =>
            {
                entity.ToView("V_AttendanceEntryForm").HasKey("Id"); // legacy view name
            });

            builder.Entity<AttendancePersentageViewDto>(entity =>
            {
                entity.ToView("V_Attendance_Caluculation").HasKey("Id"); // legacy view name
            });

            builder.Entity<ResultsViewDto>(entity =>
            {
                entity.ToView("V_Results").HasKey("Id"); // legacy view name
            });

            //AMS
            builder.Entity<TimetableViewDto>(entity =>
            {
                entity.ToView("V_Timetable").HasKey("Id"); // legacy view name
            });
            builder.Entity<TimetableDto>(entity =>
            {
                entity.ToTable("T_Timetable").HasKey("Id"); // legacy view name
            });
            builder.Entity<ClassroomDto>(entity =>
            {
                entity.ToTable("T_Classroom").HasKey("RoomID"); // legacy table name
            });
            builder.Entity<ExamTypeDto>(entity =>
            {
                entity.ToTable("T_ExamType").HasKey("Id"); // legacy table name
            });
            builder.Entity<SessionDto>(entity =>
            {
                entity.ToTable("T_Session").HasKey("Id"); // legacy table name
            });
            builder.Entity<SubjectDto>(entity =>
            {
                entity.ToTable("T_Subject").HasKey("Id"); // legacy table name
            });

            builder.Entity<ResultViewDto>(entity =>
            {
                entity.ToView("V_Result").HasKey("Id"); // legacy view name
            });
            builder.Entity<ResultDto>(entity =>
            {
                entity.ToTable("T_Exam_Result").HasKey("Id"); // legacy table name
            });

            builder.Entity<ActivityViewDto>(entity =>
            {
                entity.ToView("V_Activity").HasKey("Id"); // legacy view name
            });
            builder.Entity<ActivityDto>(entity =>
            {
                entity.ToTable("T_Activities").HasKey("Id"); // legacy view name
            });
            builder.Entity<ActivitiesDto>(entity =>
            {
                entity.ToTable("T_Activity").HasKey("Id"); // legacy view name
            });
            builder.Entity<TeacherDto>(entity =>
            {
                entity.ToView("V_Teacher").HasKey("Id"); // legacy view name
            });
            builder.Entity<PeriodDto>(entity =>
            {
                entity.ToTable("T_Period").HasKey("Id"); // legacy view name
            });
            builder.Entity<ExamTimeTableViewDto>(entity =>
            {
                entity.ToView("V_ExamTimeTable").HasKey("Id"); // legacy view name
            });
            builder.Entity<ExamTimeTableDto>(entity =>
            {
                entity.ToTable("T_ExamTimeTable").HasKey("Id"); // legacy view name
            });



          
            builder.Entity<AttendFormDto>(entity =>
            {
                entity.ToTable("T_AttendanceForm").HasKey("Id"); // legacy view name
            });
            builder.Entity<AttendFormViewDto>(entity =>
            {
                entity.ToView("V_AttendanceForms").HasKey("Id"); // legacy view name
            });
            builder.Entity<AttendDefaultDto>(entity =>
            {
                entity.ToTable("T_AttendanceDefault").HasKey("Id"); // legacy view name
            });
            builder.Entity<AttendEntryDto>(entity =>
            {
                entity.ToTable("T_AttendanceEntryForm").HasKey("Id"); // legacy view name
            });
            builder.Entity<AttendEntryViewDto>(entity =>
            {
                entity.ToView("V_AttendanceEntryForms").HasKey("Id"); // legacy view name
            });


            //aams
            //bus datasets
            builder.Entity<BusDto>(entity =>
            {
                entity.ToTable("T_Buses").HasKey("Id"); // legacy view name
            });

            builder.Entity<BusAssignmentDto>(entity =>
            {
                entity.ToTable("T_Bus_Assignments").HasKey("Id"); // legacy view name
            });

            builder.Entity<BusRouteDto>(entity =>
            {
                entity.ToTable("T_Bus_Routes").HasKey("Id"); // legacy view name
                entity.Property(e => e.Route_Price).HasColumnType("decimal(18,2)");
            });

            builder.Entity<BusViewsDto>(entity =>
            {
                entity.ToView("V_Bus").HasKey("Id"); // legacy view name
            });


            // Staff Attendance & Salary Mappings
            builder.Entity<StaffAttendanceDto>(entity =>
            {
                entity.ToTable("T_Staff_Attendance").HasKey(e => e.Id);
            });

            builder.Entity<StaffAttendanceViewDto>(entity =>
            {
                entity.ToView("V_Staff_Attendance").HasNoKey();
            });



            builder.Entity<StatusDto>(entity =>
            {
                entity.ToTable("T_Status").HasKey(e => e.Id);
            });

            //Hostel managment
            builder.Entity<HostalDto>(entity =>
            {
                entity.ToTable("T_Hostel").HasKey("Id"); // legacy view name
            });
            builder.Entity<HostelTypeDto>(entity =>
            {
                entity.ToTable("T_Hostel_Type").HasKey("Id"); // legacy view name
            });

            builder.Entity<HostalRoomDto>(entity =>
            {
                entity.ToTable("T_Hostel_Rooms").HasKey("Id"); // legacy view name
            });
            builder.Entity<HostalViewDto>(entity =>
            {
                entity.ToView("V_Hostels").HasKey("Id"); // legacy view name
            });

            builder.Entity<HostalOccupiedViewDto>(entity =>
            {
                entity.ToView("V_Hostel_Occupieds").HasKey("Id"); // legacy view name
            });

            // Staff Payment Mappings
            builder.Entity<StaffPaymentDto>(entity =>
            {
                entity.ToTable("T_Staff_Payment").HasKey(e => e.Id);
            });
            builder.Entity<StaffPaymentViewDto>(entity =>
            {
                entity.ToView("V_Staff_Payment").HasKey(e => e.Id);
            });

            // Student Fee & Payment Mappings
            builder.Entity<StudentFeeDto>(entity =>
            {
                entity.ToTable("T_Student_Fee").HasKey(e => e.Id);
            });
            builder.Entity<StudentFeeViewDto>(entity =>
            {
                entity.ToView("V_Student_Fee").HasKey(e => e.Id);
            });
            builder.Entity<StudentPaymentDto>(entity =>
            {
                entity.ToTable("T_Payment").HasKey(e => e.Id);
            });
            builder.Entity<StudentPaymentViewDto>(entity =>
            {
                entity.ToView("V_Student_Payment").HasKey(e => e.Id);
            });
            builder.Entity<FeeTypeDto>(entity =>
            {
                entity.ToTable("T_Fee_Type").HasKey(e => e.Id);
            });

            //student fee
            builder.Entity<FeeDto>(entity =>
            {
                entity.ToTable("V_Fee_Total").HasKey(e => e.Id);
            });

 
        }
        public DbSet<CourseDto> Courses => Set<CourseDto>();
        public DbSet<BatchDto> Batchs => Set<BatchDto>();
        public DbSet<SemesterDto> Semesters => Set<SemesterDto>();
        public DbSet<DivisionDto> Divisions => Set<DivisionDto>();
        public DbSet<ClassDto> Classes => Set<ClassDto>();
        public DbSet<GenderDto> Genderss => Set<GenderDto>();
        public DbSet<ClassViewDto> ClasseView => Set<ClassViewDto>();
        public DbSet<StudentViewDto> StudentsView => Set<StudentViewDto>();
        public DbSet<ParentViewDto> ParentView => Set<ParentViewDto>();

        public DbSet<StaffViewDto> StaffView => Set<StaffViewDto>();
        public DbSet<AdminViewDto> AdminView => Set<AdminViewDto>();

        //lookup
        public DbSet<RoleDto> Roles => Set<RoleDto>();


        //Book services
        public DbSet<BookDto> Books => Set<BookDto>();
        public DbSet<BookViewDto> BooksView => Set<BookViewDto>();
        public DbSet<BookCategoryDto> BookCategories => Set<BookCategoryDto>();
        public DbSet<BookPublisherDto> BookPublishers => Set<BookPublisherDto>();
        public DbSet<BookTypeDto> BookTypes => Set<BookTypeDto>();
        public DbSet<LanguageDto> Languagess => Set<LanguageDto>();
        public DbSet<BookIssueDto> BookIssue => Set<BookIssueDto>();
        public DbSet<BookIssueViewDto> BookIssues => Set<BookIssueViewDto>();
        public DbSet<BookIssueHistoryViewDto> BookIssueHIstorys => Set<BookIssueHistoryViewDto>();
        public DbSet<BookRequestDto> BookRequest => Set<BookRequestDto>();



        //AMS
        public DbSet<TimetableViewDto> Timetable => Set<TimetableViewDto>();
        public DbSet<TimetableDto> Timetables => Set<TimetableDto>();
        public DbSet<ClassroomDto> Classroom => Set<ClassroomDto>();

        public DbSet<ExamTimeTableViewDto> ExamTimeTableView => Set<ExamTimeTableViewDto>();
        public DbSet<ExamTimeTableDto> ExamTimeTable => Set<ExamTimeTableDto>();

        public DbSet<SubjectDto> Subject => Set<SubjectDto>();

        public DbSet<ActivityViewDto> ActivityView => Set<ActivityViewDto>();
        public DbSet<ActivityDto> Activity => Set<ActivityDto>();
        public DbSet<ActivitiesDto> Activities => Set<ActivitiesDto>();
        public DbSet<TeacherDto> Teacher => Set<TeacherDto>();
        public DbSet<PeriodDto> Period => Set<PeriodDto>();
        public DbSet<SessionDto> Session => Set<SessionDto>();
        public DbSet<ExamTypeDto> ExamType => Set<ExamTypeDto>();
        public DbSet<ResultViewDto> ResultView => Set<ResultViewDto>();
        public DbSet<ResultDto> Result => Set<ResultDto>();



        public DbSet<AttendFormDto> AttendanceForm => Set<AttendFormDto>();
        public DbSet<AttendFormViewDto> AttendanceFormView => Set<AttendFormViewDto>();
        public DbSet<AttendDefaultDto> AttendanceDefault => Set<AttendDefaultDto>();
        public DbSet<AttendEntryDto> AttendanceEntry => Set<AttendEntryDto>();
        public DbSet<AttendEntryViewDto> AttendanceEntryView => Set<AttendEntryViewDto>();
        //SMS
        public DbSet<StudentViewProfileDto> StudentViewProfile => Set<StudentViewProfileDto>();
        //public DbSet<StaffViewDto> StaffViewProfile => Set<StaffViewDto>();

        public DbSet<TimetablesViewDto> TimetableView => Set<TimetablesViewDto>();
        public DbSet<BusViewDto> BusView => Set<BusViewDto>();
        public DbSet<BookHistoryViewDto> BookHistoryView => Set<BookHistoryViewDto>();
        public DbSet<AttendanceViewDto> AttendanceView => Set<AttendanceViewDto>();
        public DbSet<AttendancePersentageViewDto> AttendancePersentageView => Set<AttendancePersentageViewDto>();
        public DbSet<ResultsViewDto> ResultViews => Set<ResultsViewDto>();
        public DbSet<HostelViewDto> HostelView => Set<HostelViewDto>();
        //aams
        //bus  services
        public DbSet<BusDto> Bus => Set<BusDto>();
        public DbSet<BusViewsDto> BusViews => Set<BusViewsDto>();
        public DbSet<BusAssignmentDto> BusAssignment => Set<BusAssignmentDto>();

        public DbSet<BusRouteDto> BusRoute => Set<BusRouteDto>();

        // Staff Attendance & Salary
        public DbSet<StaffAttendanceDto> StaffAttendance => Set<StaffAttendanceDto>();
        public DbSet<StaffAttendanceViewDto> StaffAttendanceView => Set<StaffAttendanceViewDto>();

        public DbSet<StatusDto> Statuses => Set<StatusDto>();

        //Hostel
        public DbSet<HostalDto> Hostal => Set<HostalDto>();
        public DbSet<HostelTypeDto> Type => Set<HostelTypeDto>();
        public DbSet<HostalRoomDto> HostalRoom => Set<HostalRoomDto>();

        public DbSet<HostalOccupiedViewDto> HostalOccView => Set<HostalOccupiedViewDto>();

        public DbSet<HostalViewDto> HostalView => Set<HostalViewDto>();

        // Staff Payments
        public DbSet<StaffPaymentDto> StaffPayments => Set<StaffPaymentDto>();
        public DbSet<StaffPaymentViewDto> StaffPaymentView => Set<StaffPaymentViewDto>();

        // Student Payments
        public DbSet<StudentFeeDto> StudentFees => Set<StudentFeeDto>();
        public DbSet<StudentFeeViewDto> StudentFeeView => Set<StudentFeeViewDto>();
        public DbSet<StudentPaymentDto> StudentPayments => Set<StudentPaymentDto>();
        public DbSet<StudentPaymentViewDto> StudentPaymentView => Set<StudentPaymentViewDto>();
        public DbSet<FeeTypeDto> FeeTypes => Set<FeeTypeDto>();

        public DbSet<FeeDto> Fee => Set<FeeDto>();

    }
}

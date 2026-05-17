using BCA007.Client.Services.AMS;
using BCA007.Client.Services.Attendance;
using BCA007.Client.Services.Bus;
using BCA007.Client.Services.Core;
using BCA007.Client.Services.Course;
using BCA007.Client.Services.Hostal;
using BCA007.Client.Services.Library;
using BCA007.Client.Services.Period;
using BCA007.Client.Services.Staff;
using BCA007.Client.Services.Student;
using BCA007.Client.Services.Users;
using BCA007.Shared.Service.AMS;
using BCA007.Shared.Service.Attendance;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Core;
using BCA007.Shared.Service.Course;
using BCA007.Shared.Service.Hostal;
using BCA007.Shared.Service.Library;
using BCA007.Shared.Service.Staff;
using BCA007.Shared.Service.Student;
using BCA007.Shared.Service.Users;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped<ICourseService, CourseServiceClient>();
builder.Services.AddScoped<IBatchService, BatchServiceClient>();
builder.Services.AddScoped<ISemesterService, SemesterServiceClient>();
builder.Services.AddScoped<IClassService, ClassServiceClient>();
builder.Services.AddScoped<IDivisionService, DivisionServiceClient>();
builder.Services.AddScoped<ILookupService, LookupServiceClient>();

builder.Services.AddScoped<IStudentService, StudentServiceClient>();
builder.Services.AddScoped<IStaffService, StaffServiceClient>();
builder.Services.AddScoped<IParentService, ParentServiceClient>();
builder.Services.AddScoped<IAdminService, AdminServiceClient>();

builder.Services.AddScoped<IStaffProfileService, StaffProfileServiceClient>();
builder.Services.AddScoped<IStudentProfileService, StudentProfileServiceClient>();

builder.Services.AddScoped<IActivityService, ActivityServiceClient>();
builder.Services.AddScoped<IActivitiesService, ActivitiesServiceClient>();
builder.Services.AddScoped<ITimeTableService, TimeTableServiceClient>();

builder.Services.AddScoped<ISubjectService, SubjectServiceClient>();
builder.Services.AddScoped<IExamService, ExamServiceClient>();
builder.Services.AddScoped<IResultService,ResultServiceClient>();
builder.Services.AddScoped<IPeriodService, PeriodServiceClient>();
builder.Services.AddScoped<IAttendFormService, AttendFormServiceClient>();
builder.Services.AddScoped<IAttendEntryService, AttendEntryServiceClient>();
builder.Services.AddScoped<IBusService, BusServiceClient>();
builder.Services.AddScoped<IBusRouteService, BusRouteServiceClient>();
builder.Services.AddScoped<IBusAssignmentService, BusAssignmentServiceClient>();


builder.Services.AddScoped<IHostalServices, HostelServiceClient>();
builder.Services.AddScoped<IHostalRoomService, HostalRoomServiceClient>();
builder.Services.AddScoped<IHostaltService, HostaltServiceClient>();

builder.Services.AddScoped<IStaffPaymentService, StaffPaymentServiceClient>();
builder.Services.AddScoped<IStudentPaymentService, StudentPaymentServiceClient>();

builder.Services.AddScoped<IFileUploadService, FileUploadService>();


builder.Services.AddScoped<IBookService, BookServiceClient>();
builder.Services.AddScoped<IBookTypeService, BookTypeServiceClient>();
builder.Services.AddScoped<IBookPublisherService, BookPublisherServiceClient>();
builder.Services.AddScoped<IBookCategoryService, BookCategoryServiceClient>();
builder.Services.AddScoped<IBookIssueViewService, BookIssueServiceClient>();
builder.Services.AddScoped<IBookIssueHistoryViewService, BookIssueHistoryServiceClient>();
builder.Services.AddScoped<IBookRequestService, BookRequestServiceClient>();
await builder.Build().RunAsync();

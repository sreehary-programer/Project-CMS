using BCA007.Client.Services.AMS;
using BCA007.Components;
using BCA007.Components.Account;
using BCA007.Data;
using BCA007.Services.AMS;
using BCA007.Services.Attendance;
using BCA007.Services.Bus;
using BCA007.Services.Core;
using BCA007.Services.Course;
using BCA007.Services.Hostal;
using BCA007.Services.HostalRoom;
using BCA007.Services.Library;
using BCA007.Services.Staff;
using BCA007.Services.Student;
using BCA007.Services.Users;
using BCA007.Shared.DTOs;
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

builder.Services.AddControllers();
// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents()
//    .AddInteractiveWebAssemblyComponents()
//    .AddAuthenticationStateSerialization();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultScheme = IdentityConstants.ApplicationScheme;
//        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//    })
//    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("AppData") ?? throw new InvalidOperationException("Connection string 'AppData' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddRoles<ApplicationRole>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<ICourseService, CourseServiceServer> ();
builder.Services.AddScoped<IBatchService, BatchServiceServer>();
builder.Services.AddScoped<ISemesterService, SemesterServiceServer>();
builder.Services.AddScoped<IClassService, ClassServiceServer>();
builder.Services.AddScoped<IDivisionService, DivisionServiceServer>();
builder.Services.AddScoped<ILookupService, LookupServiceServer>();

builder.Services.AddScoped<IStudentService, StudentServiceServer>();
builder.Services.AddScoped<IStaffService, StaffServiceServer>();
builder.Services.AddScoped<IParentService, ParentServiceServer>();
builder.Services.AddScoped<IAdminService, AdminServiceServer>();
builder.Services.AddScoped<IActivityService, ActivityServiceServer>();
builder.Services.AddScoped<IActivitiesService, ActivitiesServiceServer>();
builder.Services.AddScoped<ITimeTableService, TimeTableServiceServer>();
builder.Services.AddScoped<ISubjectService, SubjectServiceServer>();
builder.Services.AddScoped<IExamService, ExamServiceServer>();
builder.Services.AddScoped<IResultService, ResultServiceServer>();
builder.Services.AddScoped<IAttendFormService, AttendFormServiceServer>();
builder.Services.AddScoped<IAttendEntryService, AttendEntryServiceServer>();
builder.Services.AddScoped<IPeriodService, PeriodServiceServer>();

builder.Services.AddScoped<IStudentProfileService, StudentProfileServer>();
builder.Services.AddScoped<IStaffProfileService, StaffProfileServer>();
builder.Services.AddScoped<IFileUploadService, ServerFileUploadService>();


builder.Services.AddScoped<IBusService, BusServiceServer>();
builder.Services.AddScoped<IBusAssignmentService, BusAssignmentServiceServer>();
builder.Services.AddScoped<IBusRouteService, BusRouteServiceServer>();


builder.Services.AddScoped<IHostalServices, HostalServiceServer>();
builder.Services.AddScoped<IHostalRoomService, HostalRoomServiceServer>();
builder.Services.AddScoped<IHostaltService, HostaltServiceServer>();

builder.Services.AddScoped<IStaffPaymentService, StaffPaymentServiceServer>();
builder.Services.AddScoped<IStudentPaymentService, StudentPaymentServiceServer>();


builder.Services.AddScoped<IBookService, BookServiceServer>();
builder.Services.AddScoped<IBookTypeService, BookTypeServiceServer>();
builder.Services.AddScoped<IBookPublisherService, BookPublisherServiceServer>();
builder.Services.AddScoped<IBookCategoryService, BookCategoryServiceServer>();
builder.Services.AddScoped<IBookIssueViewService, BookIssueServiceServer>();
builder.Services.AddScoped<IBookIssueHistoryViewService, BookIssueHistoryServiceServer>();
builder.Services.AddScoped<IBookRequestService, BookRequestServiceServer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedRolesAsync(scope.ServiceProvider);
    await SeedStatusesAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode()
//    .AddInteractiveWebAssemblyRenderMode()
//    .AddAdditionalAssemblies(typeof(BCA007.Client._Imports).Assembly);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(BCA007.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();

app.MapControllers();

app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(IdentityConstants.ApplicationScheme);

    return Results.Redirect("/");
});

app.Run();

static async Task SeedRolesAsync(IServiceProvider sp)
{
    var roleManager = sp.GetRequiredService<RoleManager<ApplicationRole>>();

    string[] roles = ["Admin", "Teacher", "Student", "Parent", "Librarian", "Accountant", "Principal"];

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = role });
        }
    }
}
static async Task SeedStatusesAsync(IServiceProvider sp)
{
    using var scope = sp.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!await context.Statuses.AnyAsync())
    {
        var statuses = new List<StatusDto>
        {
            new StatusDto { Status = "Present" },
            new StatusDto { Status = "Absent" },
            new StatusDto { Status = "Leave" },
            new StatusDto { Status = "Late" },
            new StatusDto { Status = "Paid" },
            new StatusDto { Status = "Pending" },
            new StatusDto { Status = "Processing" },
            new StatusDto { Status = "Unpaid" },
            new StatusDto { Status = "Partial" }
        };

        context.Statuses.AddRange(statuses);
        await context.SaveChangesAsync();
    }
    else
    {
        // Check for missing statuses and add them
        var existingStatuses = await context.Statuses.Select(s => s.Status).ToListAsync();
        var requiredStatuses = new[] { "Unpaid", "Partial" };
        var newStatuses = requiredStatuses.Where(s => !existingStatuses.Contains(s))
                                          .Select(s => new StatusDto { Status = s })
                                          .ToList();

        if (newStatuses.Any())
        {
            context.Statuses.AddRange(newStatuses);
            await context.SaveChangesAsync();
        }
    }
}

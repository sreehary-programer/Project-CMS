using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Staff;
using BCA007.Shared.Service.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Staff
{
    public class StaffProfileServer:IStaffProfileService
    {
            private readonly ApplicationDbContext _db;
            private readonly IHttpContextAccessor _context;
        private readonly NavigationManager _navMgr;

        public StaffProfileServer(ApplicationDbContext db
                , IHttpContextAccessor context, NavigationManager navMgr)
            {
                _db = db;
                _context = context;
            _navMgr = navMgr;
        }

        public async Task<List<StaffViewDto>> GetAllAsync()
        {
            var user = _context.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                _navMgr.NavigateToLogin(loginPath: "/Account/Login");
                
            //throw new InvalidOperationException("User is not authenticated");

            return await _db.StaffView
                .Select(x => new StaffViewDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Gender_Id = x.Gender_Id,
                    ProfileURL = x.ProfileURL,
                    Gender_Name = x.Gender_Name,
                    DateOfBirth = x.DateOfBirth,
                    IsLocked = x.IsLocked
                })
                .Where(x => x.UserName == user.Identity.Name)
                .ToListAsync();
        }
    }
    
}

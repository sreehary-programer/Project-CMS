using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace BCA007.Services.Student
{
    public class StudentServiceServer: IStudentService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _context;

        public StudentServiceServer(
            UserManager<ApplicationUser> userManager
            , ApplicationDbContext db
            , IWebHostEnvironment env, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _db = db;
            _env = env;
            _context = context;
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Student not found");

            // Optional: remove roles first (clean)
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
                await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<List<StudentViewDto>> GetAllAsync()
        {
            
            var user =  _context.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not authenticated");

            return await _db.StudentsView
                .Select(x => new StudentViewDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Class_Id = x.Class_Id,
                    Gender_Id = x.Gender_Id,
                    ProfileURL = x.ProfileURL,
                    Class_Name=x.Class_Name,
                    Gender_Name = x.Gender_Name,
                    DateOfBirth = x.DateOfBirth,
                    IsLocked = x.IsLocked,
                    Parent_Id=x.Parent_Id,
                    Route_Name = x.Route_Name,
                    BusRoute_Id = x.BusRoute_Id,
                    HostelRoom_Id = x.HostelRoom_Id,
                    Hostal_FullName = x.Hostal_FullName

                })
                //.Where(x => x.UserName == user.Identity.Name)
                .ToListAsync();
        }

        public async Task<StudentDto> CreateAsync(StudentDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("Email is required");

            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
                throw new InvalidOperationException("Student email already exists");

            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName))
                throw new InvalidOperationException("Student User Name already exists");

            var user = new ApplicationUser
            {
                UserName = await GenerateNextUserNameAsync(),//dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Class_Id = dto.Class_Id,
                Gender_Id = dto.Gender_Id,
                Parent_Id=dto.Parent_Id,
                DateOfBirth = dto.DateOfBirth,
                ProfileURL = dto.ProfileURL,
                EmailConfirmed = true,
                BusRoute_Id = dto.BusRoute_Id,
                HostelRoom_Id = dto.HostelRoom_Id
            };

            if (dto.HostelRoom_Id.HasValue)
            {
                var room = await _db.HostalRoom.FindAsync(dto.HostelRoom_Id.Value);
                if (room == null)
                    throw new InvalidOperationException("Hostel Room not found");

                var count = await _userManager.Users.CountAsync(u => u.HostelRoom_Id == dto.HostelRoom_Id.Value);
                if (room.Capacity.HasValue && count >= room.Capacity.Value)
                    throw new InvalidOperationException($"Hostel Room is full (Capacity: {room.Capacity})");
            }


            try
            {
                var result = await _userManager.CreateAsync(user, "Test@123");

                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                var roleResult = await _userManager.AddToRoleAsync(user, "Student");

                if (!roleResult.Succeeded)
                   throw new InvalidOperationException(
                        string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
                {
                    var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.UserName);
                    user.ProfileURL = profileUrl;
                    await _userManager.UpdateAsync(user);
                }
                dto.Id = user.Id;
                dto.ProfileURL = user.ProfileURL;
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        private async Task<string> GenerateNextUserNameAsync()
        {
            var year = DateTime.Now.Year % 100; // 26
            var prefix = $"AJ{year}";

            var lastUser = await _userManager.Users
                .Where(u => u.UserName.StartsWith(prefix))
                .OrderByDescending(u => u.UserName)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            int next = 1;

            if (!string.IsNullOrEmpty(lastUser))
            {
                var numberPart = lastUser.Substring(prefix.Length);

                if (int.TryParse(numberPart, out int n))
                    next = n + 1;
            }

            return $"{prefix}{next:D4}";
        }

        private async Task<string> SaveProfileImageAsync(Stream fileStream, string fileName, string username)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "profiles");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            var ext = Path.GetExtension(fileName);
            var uniqueFileName = $"{username}_000{ext}";
            var path = Path.Combine(uploadsFolder, uniqueFileName);
            int count = 0;
            while (File.Exists(path))
            {
                count++;
                uniqueFileName = $"{username}_{count:D3}{ext}";
                path = Path.Combine(uploadsFolder, uniqueFileName);
            }

            using (var fileStreamOutput = new FileStream(path, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }
            return $"/uploads/profiles/{uniqueFileName}";
        }

        public async Task<StudentDto> UpdateAsync(StudentDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Student not found");

            if (dto.Email != null && user.Email != dto.Email)
            {
                if (await _userManager.FindByEmailAsync(dto.Email) != null)
                    throw new InvalidOperationException("Email already exists");
            }

            if (dto.UserName != null && user.UserName != dto.UserName)
            {
                if (await _userManager.FindByNameAsync(dto.UserName) != null)
                    throw new InvalidOperationException("Username already exists");
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.DateOfBirth = dto.DateOfBirth;
            user.Class_Id = dto.Class_Id;
            user.Parent_Id = dto.Parent_Id;
            user.Gender_Id = dto.Gender_Id;
            user.ProfileURL = dto.ProfileURL;
            user.BusRoute_Id = dto.BusRoute_Id;
            if (dto.HostelRoom_Id.HasValue && dto.HostelRoom_Id != user.HostelRoom_Id)
            {
                var room = await _db.HostalRoom.FindAsync(dto.HostelRoom_Id.Value);
                if (room == null)
                    throw new InvalidOperationException("Hostel Room not found");

                var count = await _userManager.Users.CountAsync(u => u.HostelRoom_Id == dto.HostelRoom_Id.Value);
                if (room.Capacity.HasValue && count >= room.Capacity.Value)
                    throw new InvalidOperationException($"Hostel Room is full (Capacity: {room.Capacity})");
            }
            user.HostelRoom_Id = dto.HostelRoom_Id;


            if (dto.IsLocked != null && dto.IsLocked== true)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;
            }
            else
            {
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
            }

            if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
            {
                var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.UserName);
                user.ProfileURL = profileUrl;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            dto.ProfileURL = user.ProfileURL;
            return dto;
        }
    }
}
        
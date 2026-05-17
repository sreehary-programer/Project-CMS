using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Core;
using BCA007.Shared.Service.Student;
using BCA007.Shared.Service.Users;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
namespace BCA007.Services.Student
{
    public class StaffServiceServer : IStaffService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly ILookupService _lookup;

        public StaffServiceServer(
            UserManager<ApplicationUser> userManager
            , ApplicationDbContext db
            , IWebHostEnvironment env,ILookupService lookup)
        {
            _userManager = userManager;
            _db = db;
            _env = env;
            _lookup = lookup;
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Staff not found");

            // Optional: remove roles first (clean)
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
                await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<List<StaffViewDto>> GetAllAsync()
        {
            //return await _db.StaffView
            //    .Select(x => new StaffViewDto
            //    {
            //        Id = x.Id,
            //        UserName = x.UserName,
            //        Email = x.Email,
            //        FullName = x.FullName,
            //        PhoneNumber = x.PhoneNumber,
            //        Gender_Id=x.Gender_Id,
            //        ProfileURL = x.ProfileURL,
            //        Gender_Name = x.Gender_Name,
            //        DateOfBirth = x.DateOfBirth,
            //        IsLocked = x.IsLocked,
            //        RoleId=x.RoleId
            //    })
            //    .ToListAsync();
            var query = from s in _db.StaffView
                        join ur in _db.UserRoles on s.Id equals ur.UserId into urGroup
                        from ur in urGroup.DefaultIfEmpty()
                        join r in _db.Roles on ur.RoleId equals r.Id into rGroup
                        from r in rGroup.DefaultIfEmpty()
                        join u in _db.Users on s.Id equals u.Id // Join with AspNetUsers to get persistent fields
                        select new StaffViewDto
                        {
                            Id = s.Id,
                            UserName = s.UserName,
                            Email = s.Email,
                            FullName = s.FullName,
                            PhoneNumber = s.PhoneNumber,
                            Gender_Id = s.Gender_Id,
                            ProfileURL = s.ProfileURL,
                            Gender_Name = s.Gender_Name,
                            DateOfBirth = s.DateOfBirth,
                            IsLocked = s.IsLocked,
                            RoleId = r != null ? r.Id : (int?)null,
                            RoleName = r != null ? r.Name : null,
                            NextPaymentDueDate = u.NextPaymentDueDate,
                            CurrentPaymentStatus = u.CurrentPaymentStatus
                        };

            return await query.ToListAsync();
        }

        public async Task<StaffDto> CreateAsync(StaffDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("Email is required");

            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
                throw new InvalidOperationException("Staff email already exists");

            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName))
                throw new InvalidOperationException("Staff User Name already exists");

            var user = new ApplicationUser
            {
                UserName = await GenerateNextUserNameAsync(),//dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Gender_Id = dto.Gender_Id,
                DateOfBirth = dto.DateOfBirth,
                ProfileURL = dto.ProfileURL,
                EmailConfirmed = true,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, "Test@123");

                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        string.Join(", ", result.Errors.Select(e => e.Description)));



                /*  var roleResult = await _userManager.AddToRoleAsync(user, "Staff");

                  if (!roleResult.Succeeded)
                     throw new InvalidOperationException(
                          string.Join(", ", roleResult.Errors.Select(e => e.Description)));*/

                var roles = await _lookup.GetRoleAsync();

                var selectedRole = roles.FirstOrDefault(r => r.Id == dto.RoleId);

                if (selectedRole == null)
                    throw new Exception("Invalid role selected");

                await _userManager.AddToRoleAsync(user, selectedRole.Text);



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

        public async Task<StaffDto> UpdateAsync(StaffDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Staff not found");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("Email is required");

            // Email uniqueness check (excluding current user)
            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email && x.Id != user.Id))
                throw new InvalidOperationException("Staff email already exists");

            // Username uniqueness check (excluding current user)
            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName && x.Id != user.Id))
                    throw new InvalidOperationException("Staff User Name already exists");

                user.UserName = dto.UserName;
            }

            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Gender_Id = dto.Gender_Id;
            user.DateOfBirth = dto.DateOfBirth;

            // Lock handling
            if (dto.IsLocked == true)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;
            }
            else
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
                user.AccessFailedCount = 0;
            }

            // Role Update (remove old + add new like create flow)
            var roles = await _lookup.GetRoleAsync();
            var selectedRole = roles.FirstOrDefault(r => r.Id == dto.RoleId);

            if (selectedRole == null)
                throw new Exception("Invalid role selected");

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, selectedRole.Text);

            // Profile image update
            if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
            {
                var profileUrl = await SaveProfileImageAsync(fileStream, fileName, user.UserName);
                user.ProfileURL = profileUrl;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            dto.ProfileURL = user.ProfileURL;
            return dto;
        }

        public async Task UpdatePaymentStatusAsync(int staffId, string? status, DateTime? dueDate)
        {
            var user = await _userManager.FindByIdAsync(staffId.ToString());
            if (user == null) throw new KeyNotFoundException("Staff not found");

            user.CurrentPaymentStatus = status;
            user.NextPaymentDueDate = dueDate;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }


    }
}
        
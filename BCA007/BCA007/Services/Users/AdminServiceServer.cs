using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Users
{
    public class AdminServiceServer : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AdminServiceServer(
            UserManager<ApplicationUser> userManager
            , ApplicationDbContext db
            , IWebHostEnvironment env)
        {
            _userManager = userManager;
            _db = db;
            _env = env;
        }
        public async Task<AdminDto> CreateAsync(AdminDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new InvalidOperationException("Email is required");

            if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
                throw new InvalidOperationException("Admin email already exists");

            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName))
                throw new InvalidOperationException("Admin User Name already exists");

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

                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

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

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Admin not found");

            // Optional: remove roles first (clean)
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
                await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<List<AdminViewDto>> GetAllAsync()
        {
            return await _db.AdminView
                            .Select(x => new AdminViewDto
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
                            .ToListAsync();
        }

        public async Task<AdminDto> UpdateAsync(AdminDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                throw new KeyNotFoundException("Admin not found");

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
            user.Gender_Id = dto.Gender_Id;
            user.ProfileURL = dto.ProfileURL;
            if (dto.IsLocked != null && dto.IsLocked == true)
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
    }
}

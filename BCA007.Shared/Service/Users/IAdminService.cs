using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs;

namespace BCA007.Shared.Service.Users
{
    public interface IAdminService
    {
        Task<List<AdminViewDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<AdminDto> CreateAsync(AdminDto dto, Stream? fileStream, string? fileName);
        Task<AdminDto> UpdateAsync(AdminDto dto, Stream? fileStream, string? fileName);
        Task DeleteAsync(int id);
    }
}

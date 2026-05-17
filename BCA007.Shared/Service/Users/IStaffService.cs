using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCA007.Shared.DTOs;

namespace BCA007.Shared.Service.Users
{
    public interface IStaffService
    {
        Task<List<StaffViewDto>> GetAllAsync();
        //Task CreateAsync(StudentDto dto);
        Task<StaffDto> CreateAsync(StaffDto dto, Stream? fileStream, string? fileName);
        Task<StaffDto> UpdateAsync(StaffDto dto, Stream? fileStream, string? fileName);
        Task UpdatePaymentStatusAsync(int staffId, string? status, DateTime? dueDate);
        Task DeleteAsync(int id);

    }
}

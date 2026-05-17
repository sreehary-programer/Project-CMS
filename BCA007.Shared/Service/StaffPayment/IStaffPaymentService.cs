using BCA007.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Users
{
    public interface IStaffPaymentService
    {
        Task<IEnumerable<StaffPaymentViewDto>> GetAllStaffPaymentsAsync();
        Task<StaffPaymentDto> GetStaffPaymentByIdAsync(int id);
        Task<StaffPaymentDto> AddStaffPaymentAsync(StaffPaymentDto payment);
        Task<StaffPaymentDto> UpdateStaffPaymentAsync(StaffPaymentDto payment);
        Task<bool> DeleteStaffPaymentAsync(int id);
    }
}

using BCA007.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCA007.Shared.Service.Student
{
    public interface IStudentPaymentService
    {
        // Fee Management
        Task<IEnumerable<StudentFeeViewDto>> GetAllStudentFeesAsync();
        Task<StudentFeeDto> GetStudentFeeByIdAsync(int id);
        Task<StudentFeeDto> AddStudentFeeAsync(StudentFeeDto fee);
        Task<StudentFeeDto> UpdateStudentFeeAsync(StudentFeeDto fee);
        Task<bool> DeleteStudentFeeAsync(int id);

        // Payment Management
        Task<IEnumerable<StudentPaymentViewDto>> GetAllStudentPaymentsAsync();
        Task<StudentPaymentDto> AddStudentPaymentAsync(StudentPaymentDto payment);
        Task<StudentPaymentDto> UpdateStudentPaymentAsync(StudentPaymentDto payment);
        Task<bool> DeleteStudentPaymentAsync(int id);
        Task<StudentPaymentDto> GetStudentPaymentByIdAsync(int id);

        // Fee Types & Statuses
        Task<IEnumerable<FeeTypeDto>> GetAllFeeTypesAsync();
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();

        // Specific Updates
        Task<bool> UpdateStudentFeeStatusAsync(int id, int statusId);
    }
}

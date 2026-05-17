using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Hostal
{
    public class StaffPaymentServiceServer : IStaffPaymentService
    {
        private readonly ApplicationDbContext _context;

        public StaffPaymentServiceServer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StaffPaymentDto> AddStaffPaymentAsync(StaffPaymentDto payment)
        {
            try
            {
                 // Bypass EF Core View mapping issue by using Raw SQL
                string sql = "INSERT INTO T_Staff_Payment (Staff_Id, Amount, Payment_Date, Payment_Mode_Id, Due_Date, Status) VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
                await _context.Database.ExecuteSqlRawAsync(sql, payment.Staff_Id, payment.Amount, payment.Payment_Date, payment.Payment_Mode_Id, payment.Due_Date, payment.Status);
                return payment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding staff payment: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    throw new Exception($"Error adding payment: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task<bool> DeleteStaffPaymentAsync(int id)
        {
            var payment = await _context.StaffPayments.FindAsync(id);
            if (payment == null)
            {
                return false;
            }

            _context.StaffPayments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StaffPaymentViewDto>> GetAllStaffPaymentsAsync()
        {
            // Use LINQ Join instead of View to include Due_Date and Status from Table
            var query = from p in _context.StaffPayments
                        join s in _context.StaffView on p.Staff_Id equals s.Id
                        orderby p.Payment_Date descending
                        select new StaffPaymentViewDto
                        {
                            Id = p.Id,
                            Staff_Id = p.Staff_Id,
                            FullName = s.FullName,
                            UserName = s.UserName,
                            SalaryType = "Salary", // Placeholder or derived logic
                            Amount = p.Amount,
                            Payment_Date = p.Payment_Date,
                            Payment_Mode_Id = p.Payment_Mode_Id,
                            Due_Date = p.Due_Date,
                            Status = p.Status
                        };

            return await query.ToListAsync();
        }

        public async Task<StaffPaymentDto> GetStaffPaymentByIdAsync(int id)
        {
            return await _context.StaffPayments.FindAsync(id);
        }

        public async Task<StaffPaymentDto> UpdateStaffPaymentAsync(StaffPaymentDto payment)
        {
            try
            {
                 // Bypass EF Core View mapping issue by using Raw SQL
                string sql = "UPDATE T_Staff_Payment SET Staff_Id={0}, Amount={1}, Payment_Date={2}, Payment_Mode_Id={3}, Due_Date={4}, Status={5} WHERE Id={6}";
                var rows = await _context.Database.ExecuteSqlRawAsync(sql, payment.Staff_Id, payment.Amount, payment.Payment_Date, payment.Payment_Mode_Id, payment.Due_Date, payment.Status, payment.Id);
                return rows > 0 ? payment : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating staff payment: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    throw new Exception($"Error updating payment: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}

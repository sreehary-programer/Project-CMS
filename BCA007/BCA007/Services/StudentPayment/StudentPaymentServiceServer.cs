using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Student
{
    public class StudentPaymentServiceServer : IStudentPaymentService
    {
        private readonly ApplicationDbContext _context;

        public StudentPaymentServiceServer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentFeeDto> AddStudentFeeAsync(StudentFeeDto fee)
        {
            try
            {
                // Bypass EF Core View mapping issue by using Raw SQL
                string sql = "INSERT INTO T_Student_Fee (Student_Id, Fee_Type_Id, Amount, Due_Date, Status_Id, Description) VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
                await _context.Database.ExecuteSqlRawAsync(sql, fee.Student_Id, fee.Fee_Type_Id, fee.Amount, fee.Due_Date, fee.Status_Id, fee.Description);
                
                // We are not retrieving the generated ID here as the UI currently doesn't strictly need it for the immediate redirect.
                // If needed, we would run a SELECT SCOPE_IDENTITY() query.
                return fee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student fee: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    throw new Exception($"Error saving fee: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task<StudentPaymentDto> AddStudentPaymentAsync(StudentPaymentDto payment)
        {
            try
            {
                 // Bypass EF Core View mapping issue by using Raw SQL
                 // Note: Payment_Id in DTO seems to correspond to Payment Mode
                string sql = "INSERT INTO T_Payment (Student_Id, Student_Fee_Id, Amount_Paid, Paid_Date, Payment_Id, Receipt_Number) VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
                await _context.Database.ExecuteSqlRawAsync(sql, payment.Student_id, payment.Student_Fee_Id, payment.Amount_Paid, payment.Paid_Date, payment.Payment_Id, payment.Receipt_Number);
                
                // Logic to update fee status is handled in the controller/UI for now by calling specific update method
                
                return payment;
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error adding student payment: {ex.Message}");
                 if (ex.InnerException != null)
                 {
                     Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                     throw new Exception($"Error adding payment: {ex.InnerException.Message}");
                 }
                 throw;
            }
        }
        
        public async Task<bool> DeleteStudentPaymentAsync(int id)
        {
            var rows = await _context.Database.ExecuteSqlRawAsync("DELETE FROM T_Payment WHERE Id = {0}", id);
            return rows > 0;
        }

        public async Task<StudentPaymentDto> GetStudentPaymentByIdAsync(int id)
        {
            return await _context.StudentPayments.FindAsync(id);
        }

        public async Task<StudentPaymentDto> UpdateStudentPaymentAsync(StudentPaymentDto payment)
        {
            try
            {
                 // Bypass EF Core View mapping issue by using Raw SQL
                string sql = "UPDATE T_Payment SET Student_Id={0}, Student_Fee_Id={1}, Amount_Paid={2}, Paid_Date={3}, Payment_Id={4}, Receipt_Number={5} WHERE Id={6}";
                var rows = await _context.Database.ExecuteSqlRawAsync(sql, payment.Student_id, payment.Student_Fee_Id, payment.Amount_Paid, payment.Paid_Date, payment.Payment_Id, payment.Receipt_Number, payment.Id);
                
                return rows > 0 ? payment : null;
            }
             catch (Exception ex)
            {
                 Console.WriteLine($"Error updating student payment: {ex.Message}");
                 if (ex.InnerException != null)
                 {
                     Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                     throw new Exception($"Error updating payment: {ex.InnerException.Message}");
                 }
                 throw;
            }
        }

        public async Task<bool> DeleteStudentFeeAsync(int id)
        {
            var fee = await _context.StudentFees.FindAsync(id);
            if (fee == null) return false;

            _context.StudentFees.Remove(fee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FeeTypeDto>> GetAllFeeTypesAsync()
        {
            var types = await _context.FeeTypes.ToListAsync();
            if (!types.Any())
            {
                var defaultTypes = new List<FeeTypeDto>
                {
                    new FeeTypeDto { Fee_Type_Name = "Tuition Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Exam Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Library Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Transportation Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Hostel Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Laboratory Fee" },
                    new FeeTypeDto { Fee_Type_Name = "Miscellaneous Fee" }
                };
                _context.FeeTypes.AddRange(defaultTypes);
                await _context.SaveChangesAsync();
                types = await _context.FeeTypes.ToListAsync();
            }
            return types;
        }

        public async Task<IEnumerable<StudentFeeViewDto>> GetAllStudentFeesAsync()
        {
            return await _context.StudentFeeView.ToListAsync();
        }

        public async Task<IEnumerable<StudentPaymentViewDto>> GetAllStudentPaymentsAsync()
        {
            return await _context.StudentPaymentView.ToListAsync();
        }

        public async Task<StudentFeeDto> GetStudentFeeByIdAsync(int id)
        {
            return await _context.StudentFees.FindAsync(id);
        }

        public async Task<StudentFeeDto> UpdateStudentFeeAsync(StudentFeeDto fee)
        {
            try
            {
                // Bypass EF Core View mapping issue by using Raw SQL
                string sql = "UPDATE T_Student_Fee SET Student_Id={0}, Fee_Type_Id={1}, Amount={2}, Due_Date={3}, Status_Id={4}, Description={5} WHERE Id={6}";
                var rows = await _context.Database.ExecuteSqlRawAsync(sql, fee.Student_Id, fee.Fee_Type_Id, fee.Amount, fee.Due_Date, fee.Status_Id, fee.Description, fee.Id);
                
                return rows > 0 ? fee : null;
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error updating student fee: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    throw new Exception($"Error updating fee: {ex.InnerException.Message}");
                }
                throw;
            }
        }
        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<bool> UpdateStudentFeeStatusAsync(int id, int statusId)
        {
            try
            {
                // Using ExecuteSqlRawAsync to bypass EF Core mapping issues potentially causing it to target the View
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync("UPDATE T_Student_Fee SET Status_Id = {0} WHERE Id = {1}", statusId, id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating fee status: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    throw new Exception($"Error saving changes: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}

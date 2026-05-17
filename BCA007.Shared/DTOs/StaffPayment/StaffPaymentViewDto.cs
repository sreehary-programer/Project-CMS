using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("V_Staff_Payment")]
    public class StaffPaymentViewDto
    {
        [Key]
        public int Id { get; set; }

        public int Staff_Id { get; set; }

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        [Column("Salary Type")]
        public string? SalaryType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Payment_Date { get; set; }



        public int Payment_Mode_Id { get; set; }

        public DateTime? Due_Date { get; set; }

        public string? Status { get; set; }
    }
}

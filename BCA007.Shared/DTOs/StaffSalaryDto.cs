using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Staff_Salary")]
    public class StaffSalaryDto
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasicSalary { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Allowances { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Deductions { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetSalary { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int StatusId { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}

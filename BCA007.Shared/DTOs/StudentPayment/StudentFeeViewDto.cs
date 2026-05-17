using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("V_Student_Fee")]
    public class StudentFeeViewDto
    {
        [Key]
        public int Id { get; set; }

        public int Student_Id { get; set; }

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public int Fee_Type_Id { get; set; }

        public string? Fee_Type_Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Due_Date { get; set; }

        public int Status_Id { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }
    }
}

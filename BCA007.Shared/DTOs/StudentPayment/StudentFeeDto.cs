using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Student_Fee")]
    public class StudentFeeDto
    {
        [Key]
        public int Id { get; set; }

        public int Student_Id { get; set; }

        public int Fee_Type_Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Due_Date { get; set; }

        public int Status_Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
    }
}

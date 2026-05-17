using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("V_Student_Payment")]
    public class StudentPaymentViewDto
    {
        [Key]
        public int Id { get; set; }

        public int Student_id { get; set; }
        
        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public int Student_Fee_Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } // Fee Amount

        public string? Description { get; set; }

        public DateTime Due_Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount_Paid { get; set; }

        public DateTime Paid_Date { get; set; }

        public int Payment_Id { get; set; }

        [Column("PymntMthd")]
        public string? PaymentMethod { get; set; }

        public string? Receipt_Number { get; set; }
        
        public string? Status { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Staff_Payment")]
    public class StaffPaymentDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Staff_Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Payment_Date { get; set; }

        public int Payment_Mode_Id { get; set; }

        public DateTime? Due_Date { get; set; }

        public string? Status { get; set; }
    }
}

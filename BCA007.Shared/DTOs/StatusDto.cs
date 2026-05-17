using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Status")]
    public class StatusDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        public string Status { get; set; } = null!;
    }
}

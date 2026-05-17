using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCA007.Shared.DTOs
{
    [Table("T_Fee_Type")]
    public class FeeTypeDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Fee_Type_Name { get; set; }
    }
}

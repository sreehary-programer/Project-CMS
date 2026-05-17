using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class ParentDto
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "User name is required")]
        //[StringLength(10, ErrorMessage = "User name max 10 characters")]
        public string? UserName { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Enter a valic Email")]
        public string? Email { get; set; } = default!;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(150, ErrorMessage = "Full name max 150 characters")]
        public string? FullName { get; set; } = default!;

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(50, ErrorMessage = "Phone Number max 50 characters")]
        public string? PhoneNumber { get; set; } = default!;

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(50, ErrorMessage = "Phone Number max 50 characters")]
        //public string Password { get; set; } = default!;

        //[Required, Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; } = default!;

        [Required]
        public DateTime? DateOfBirth { get; set; }
        //[Required]
        //public int? Class_Id { get; set; }
        [Required]
        public int? Gender_Id { get; set; }
        public int? Parent_Id { get; set; }

        public string? ProfileURL { get; set; }=string.Empty;
        public bool? IsLocked { get; set; }=false;
        public DateTime? LockoutEnd { get; set; }
        public string? Parent_Name { get; set; } = string.Empty;

    }
    public class ParentViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        //public int? Class_Id { get; set; }
        //public string Class_Name { get; set; } = default!;
        public int? Gender_Id { get; set; }
        public string Gender_Name { get; set; } = default!;
        public string? ProfileURL { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = false;
        public int? Parent_Id { get; set; }
        public string? Parent_Name { get; set; } = string.Empty;


    }
  
}

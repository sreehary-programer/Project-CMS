using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class StudentDto
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
        [Required]
        public int? Class_Id { get; set; }
        [Required]
        public int? Gender_Id { get; set; }
        public int? Parent_Id { get; set; }
        public string? ProfileURL { get; set; }=string.Empty;
        public bool? IsLocked { get; set; }=false;
        public DateTime? LockoutEnd { get; set; }
        public int? BusRoute_Id { get; set; }
        [Required]
        public int? HostelRoom_Id { get; set; }
    }
    public class StudentViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public int? Class_Id { get; set; }
        public string Class_Name { get; set; } = default!;
        public int? Gender_Id { get; set; }
        public string Gender_Name { get; set; } = default!;
        public string? ProfileURL { get; set; } = string.Empty;
        public int? Parent_Id { get; set; }
        public string Parent_Name { get; set; } = default!;

        public bool IsLocked { get; set; } = false;
        public int? BusRoute_Id { get; set; }
        public string? Route_Name { get; set; } = default!;
        public int? HostelRoom_Id { get; set; }
        public string? Hostal_FullName { get; set; } = default!;



    }
    public class StudentViewProfileDto
    {
        //personal info
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public int? Class_Id { get; set; }
        public int? Gender_Id { get; set; }
        public string Gender_Name { get; set; } = default!;
        public string? ProfileURL { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = false;
        public string? Parent_Name { get; set; } = default!;

        //academic
        public string Class_Name { get; set; } = default!;
        public string Division_Name { get; set; } = default!;//
        public string Semester_Name { get; set; } = default!;//
        public string Course_Code { get; set; } = default!;
        public string Course_Name { get; set; } = default!;//
        public string Batch_Name { get; set; } = default!;//
        public int Duration { get; set; }//

    }
    public class HostelViewDto
    {
        public int Student_Id { get; set; }
        public string? UserName { get; set; }
        public string? Hostel_FullName { get; set; } = default!;
        public string? Warden_Name { get; set; } = default!;
        //public string? Parent_Name { get; set; } = default!;
        public string? Room_Number { get; set; } = default!;
        public string? Hostel_Name { get; set; } = default!;
        //public int Student_Id { get; set; }

    }
    public class TimetablesViewDto
    {
        public int Id { get; set; }
        public int Student_Id { get; set; }

        public string? Class_Name { get; set; }
        public string? Subject_Name { get; set; } = default!;
        public string? Period_Name { get; set; } = default!;
        public string? Staff_Name { get; set; } = default!;
        public DateTime? Date { get; set; }
        public string? DayOfWeek{ get; set; } = default!;
        public string? Student_UserName { get; set; } = default!;
        public string? Parent_Name { get; set; } = default!;
    }
    public class BusViewDto
    {
        public int Id { get; set; }
        public string? Bus_Name { get; set; }
        public string? Route_Name { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public decimal Route_Price { get; set; }
        public string? Parent_Name { get; set; } = default!;

    }
    public class BookHistoryViewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? Parent_Name { get; set; } = default!;
        public DateTime? Return_Date { get; set; }
        public DateTime? Issue_Date { get; set; }
        public int Issued_To { get; set; }
        public decimal Fine { get; set; }

    }
    public class AttendanceViewDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Parent_Name { get; set; } = default!;

        public string? Att_1 { get; set; } = default!;
        public string? Att_2 { get; set; } = default!;
        public string? Att_3 { get; set; } = default!;
        public string? Att_4 { get; set; } = default!;
        public string? Att_5 { get; set; } = default!;
        public int Student_Id { get; set; }

    }
    public class AttendancePersentageViewDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Parent_Name { get; set; } = default!;
        public double AttendancePercentage { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }

    }

    public class ResultsViewDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Parent_Name { get; set; } = default!;

        public DateTime? Date { get; set; }
        public string? Subject_Name { get; set; } = default!;
        public string? Exam_Type { get; set; } = default!;
        public decimal Obtained_Mark { get; set; }
        public decimal Max_Mark { get; set; }
        public int Student_Id { get; set; }

    }

    public class FeeDto
    {
        public int Id { get; set; }
        //public string? UserName { get; set; } = default!;
        //public string? Parent_Name { get; set; } = default!;

        //public DateTime Due_Date { get; set; }

        //public string Description { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } = default!;
        public string? Fee_Type_Name { get; set; } = default!;
        public int Student_Id { get; set; }
    }

  


}

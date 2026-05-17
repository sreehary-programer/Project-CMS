using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [StringLength(5,
        MinimumLength = 3,
        ErrorMessage = "Course code must be between 3 and 5 characters")]
        public string Course_Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, ErrorMessage = "Course name max 50 characters")]
        public string Course_Name { get; set; } = string.Empty;
    }
    public class BatchDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Batch name is required")]
        [StringLength(50, ErrorMessage = "Batch name max 50 characters")]
        public string Batch_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Value is required")]
        [Range(6, 8, ErrorMessage = "Value must be between 4 and 8")]
        public int Duration { get; set; } = 6;
    }
    public class SemesterDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Semester name is required")]
        [StringLength(50, ErrorMessage = "Semester name max 50 characters")]
        public string Semester_Name { get; set; } = string.Empty;
    }
    public class DivisionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Division name is required")]
        [StringLength(50, ErrorMessage = "Division name max 50 characters")]
        public string Division_Name { get; set; } = string.Empty;
    }
    public class ClassDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int? Course_Id { get; set; }

        [Required(ErrorMessage = "Batch is required")]
        public int? Batch_Id { get; set; }

        [Required(ErrorMessage = "Semester is required")]
        public int? Semester_Id { get; set; }

        [Required(ErrorMessage = "Division is required")]
        public int? Division_Id { get; set; }
    }
    public class ClassViewDto
    {
        public int Id { get; set; }
        public string Class_Name { get; set; } = string.Empty;
        public int Course_Id { get; set; } = -1;
        public string Course_Code { get; set; } = string.Empty;
        public string Course_Name { get; set; } = string.Empty;
        public int Batch_Id { get; set; } = -1;
        public string Batch_Name { get; set; } = string.Empty;
        public int Duration { get; set; } = 6;
        public int Semester_Id { get; set; } = -1;
        public string Semester_Name { get; set; } = string.Empty;
        public int Division_Id { get; set; } = -1;
        public string Division_Name { get; set; } = string.Empty;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class AttendFormViewDto
    {
        public int Id { get; set; }
        public int? Class_Id { get; set; } = default!;
        public string? Class_Name { get; set; } = default!;
        public DateTime? Date { get; set; }
        public int? Period1_DefId { get; set; } = default!;
        public int? Period1_StaffId { get; set; } = default!;
        public string? Teacher_Name1 { get; set; } = default!;
        public string? Att_1 { get; set; } = default!;
        public int? Period2_DefId { get; set; } = default!;
        public int? Period2_StaffId { get; set; } = default!;
        public string? Teacher_Name2 { get; set; } = default!;
        public string? Att_2 { get; set; } = default!;
        public int? Period3_DefId { get; set; } = default!;
        public int? Period3_StaffId { get; set; } = default!;
        public string? Teacher_Name3 { get; set; } = default!;
        public string? Att_3 { get; set; } = default!;
        public int? Period4_DefId { get; set; } = default!;
        public int? Period4_StaffId { get; set; } = default!;
        public string? Teacher_Name4 { get; set; } = default!;
        public string? Att_4 { get; set; } = default!;
        public int? Period5_DefId { get; set; } = default!;
        public int? Period5_StaffId { get; set; } = default!;
        public string? Teacher_Name5 { get; set; } = default!;
        public string? Att_5 { get; set; } = default!;

    }
    public class AttendFormDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Class_Id { get; set; } = default!;
        public int? Period1_DefId { get; set; } = default!;
        public int? Period2_DefId { get; set; } = default!;
        public int? Period3_DefId { get; set; } = default!;
        public int? Period4_DefId { get; set; } = default!;
        public int? Period5_DefId { get; set; } = default!;
        public int? Period1_StaffId { get; set; } = default!;
        public int? Period2_StaffId { get; set; } = default!;
        public int? Period3_StaffId { get; set; } = default!;
        public int? Period4_StaffId { get; set; } = default!;
        public int? Period5_StaffId { get; set; } = default!;

    }
    public class AttendDefaultDto
    {
        public int? Id { get; set; }
        public string? Attendance_Name { get; set; } = default!;
    }

    public class AttendEntryDto
    {
         public int Id { get; set; }
        public int? Att_Form_Id { get; set; }
        //public DateTime? Date { get; set; }

        public int? Period1_Att_Id { get; set; }
        public int? Period2_Att_Id { get; set; }
        public int? Period3_Att_Id { get; set; }
        public int? Period4_Att_Id { get; set; }
        public int? Period5_Att_Id { get; set; }
        public int? Student_Id { get; set; }

   


    }
    public class AttendEntryViewDto
    {
          public int Id { get; set; }
        public int? Att_Form_Id { get; set; }
        public DateTime? Date { get; set; }
        public string? UserName { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? Class_Name { get; set; } = default!;
        public string? Att_1 { get; set; } = default!;
        public string? Att_2 { get; set; } = default!;
        public string? Att_3 { get; set; } = default!;
        public string? Att_4 { get; set; } = default!;
        public string? Att_5 { get; set; } = default!;
        //public int? Form_1 { get; set; }
        //public int? Form_2 { get; set; }
        //public int? Form_3 { get; set; }
        //public int? Form_4 { get; set; }
        //public int? Form_5 { get; set; }
        public int? Period1_Att_Id { get; set; }
        public int? Period2_Att_Id { get; set; }
        public int? Period3_Att_Id { get; set; }
        public int? Period4_Att_Id { get; set; }
        public int? Period5_Att_Id { get; set; }
        public int? Student_Id { get; set; }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs.AMS
{
    public class ExamTimeTableViewDto
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; } = default!;

        public string? SessionName { get; set; } = default!;

        public string? Subject_Name { get; set; } = default!;

        public string? Class_Name { get; set; } = default!;
        public int? Class_Id { get; set; }


        public int? Subject_Id { get; set; }
        public int? ExamType_Id { get; set; }

        public string? Exam_Type { get; set; } = default!;

        public int? Session_Id { get; set; }
        public decimal? Max_Mark { get; set; }

    }
    public class ExamTimeTableDto
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; } = default!;
        public int? Class_Id { get; set; }

        public int? ExamType_Id { get; set; }

        public int? Subject_Id { get; set; }


        public int? Session_Id { get; set; }
        public decimal? Max_Mark { get; set; }

    }
    public class SessionDto
    {
        public int? Id { get; set; }
        public string? SessionName { get; set; } = default!;


    }
    public class ExamTypeDto
    {
        public int? Id { get; set; }
        public string? Exam_Type { get; set; } = default!;

    }

    public class ResultViewDto
    {
        public int Id { get; set; }
        public int Exam_Id { get; set; }


        public int? Class_Id { get; set; }

        public int? Student_Id { get; set; }

        public DateTime? Date { get; set; }

        public int? ExamType_Id { get; set; }
        public int? Subject_Id { get; set; }
        public string? Subject_Name { get; set; }
        public string? Subject_Code { get; set; }
        public decimal? Marks { get; set; }
        public decimal? Max_Mark { get; set; }


        public string? Class_Name { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Exam_Type { get; set; }
    }
    public class ResultDto
    {
        public int Id { get; set; }
        public int Exam_Id { get; set; }


        public int? Student_Id { get; set; }

    

        public decimal? Marks { get; set; }


    }




}

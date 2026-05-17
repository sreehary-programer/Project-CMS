using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class TimetableViewDto 
    {
        public int Id { get; set; }

        // Class info
        public int? ClassID { get; set; }
        public string Class_Name { get; set; }

        // Classroom info
        public string ClassNumber { get; set; }

        // Period info
        public int? PeriodID { get; set; }
        public string Period_Name { get; set; }

        // Subject info
        public int? SubjectID { get; set; }
        public string Subject_Name { get; set; }
        public string Subject_Code { get; set; }

        // Faculty info
        public int? FacultyID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        // Timetable info


        public DateTime? Date { get; set; }

}
    public class TimetableDto
    {
        public int Id { get; set; }

        // Class info
        public int? ClassID { get; set; }

        // Classroom info

        // Period info
        public int? PeriodID { get; set; }

        // Subject info
        public int? SubjectID { get; set; }

        // Faculty info
        public int? FacultyID { get; set; }

        // Timetable info


        public DateTime? Date { get; set; }


    }

    public class ClassroomDto
    {
        public int? RoomID { get; set; }

        public string ClassNumber { get; set; } 
    }

   

    public class SubjectDto
    {
        public int Id { get; set; }

        public string? Subject_Name { get; set; }

        public string? Subject_Code { get; set; }



    }
    }

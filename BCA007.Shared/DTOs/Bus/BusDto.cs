using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs.Library
{
    public class BusDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bus_Name is required")]
        [StringLength(150, ErrorMessage = "Bus_Name max 150 characters")]
        public string? Bus_Name { get; set; } = default!;

        [Required(ErrorMessage = "Capacity is required")]
        public int? Capacity { get; set; } = default!;

        [NotMapped]
        public string? Route_Id { get; set; }

        [NotMapped]
        public DateTime? Start_Date { get; set; }

        [NotMapped]
        public DateTime? End_Date { get; set; }

        [NotMapped]
        public bool CreateNewRoute { get; set; }

        [NotMapped]
        public string? New_Route_Name { get; set; }

        [NotMapped]
        public string? New_Start_Point { get; set; }

        [NotMapped]
        public string? New_End_Point { get; set; }

        public int? Driver_Id { get; set; }
        public string? Emergency_Number { get; set; }
        public DateTime? Insurance_Expiry_Date { get; set; }
        public DateTime? Road_Permit_Expiry_Date { get; set; }
        public DateTime? Next_Service_Date { get; set; }
    }
    public class BusViewsDto
    {
        public int Id { get; set; }
        public int? Bus_id { get; set; } = default!;
        public int? Route_Id { get; set; } = default!;
        public int? Student_Id { get; set; } = default!;
        public DateTime? Start_Date { get; set; } = default!;
        public DateTime? End_date { get; set; } = default!;
        public string? Start_Point { get; set; } = default!;
        public string? End_Point { get; set; } = default!;
        public string? Route_Name { get; set; } = default!;
        public string? Bus_Name { get; set; } = default!;
        public int? Capacity { get; set; } = default!;

        public int? Driver_Id { get; set; }
        public string? Driver_Name { get; set; }
        public string? Emergency_Number { get; set; }
        public DateTime? Insurance_Expiry_Date { get; set; }
        public DateTime? Road_Permit_Expiry_Date { get; set; }
        public DateTime? Next_Service_Date { get; set; }
    }

    public class BusRouteDto
    {
       
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name max 100 characters")]
        public string Route_Name { get; set; } = string.Empty;

        public int Id { get; set; }

        public decimal? Route_Price { get; set; }
        public int? Bus_Id { get; set; }

        [NotMapped]
        public string? Bus_Name { get; set; }
    }

    public class BusAssignmentDto
    {
        public int Id { get; set; }
        
        public int Route_Id { get; set; }


       
        
        public DateTime? Start_Date { get; set; } 

       
        
        public DateTime? End_Date { get; set; } 

       
        
        public int Bus_Id { get; set; }

       
        public int? Student_Id { get; set; } 

       
    }

    public class DriverViewDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }

}

    


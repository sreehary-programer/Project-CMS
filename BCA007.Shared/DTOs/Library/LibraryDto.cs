using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCA007.Shared.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(250, ErrorMessage = "Title max 250 characters")]
        public string? Title { get; set; } = default!;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(150, ErrorMessage = "Author max 150 characters")]
        public string? Author { get; set; } = default!;

        [Required(ErrorMessage = "Publisher is required")]
        public int? Publisher_Id { get; set; } = default!;

        [Required(ErrorMessage = "Category is required")]
        public int? Category_Id { get; set; } = default!;

        [Required(ErrorMessage = "Type is required")]
        public int? Type_Id { get; set; } = default!;

        [Required(ErrorMessage = "Language is required")]
        public int? Language_Id { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public string? Edition { get; set; } = default!;
        public string? Volume { get; set; } = default!;
        public int? Pages { get; set; } = default!;
        public string? ThumbURL { get; set; } = default!;
        public int? Issued_To { get; set; } = default!;
        public DateTime? Issued_Date { get; set; }
        public DateTime? Return_Date { get; set; }
        //public DateTime? Due_Date { get; set; }
    }
    public class BookViewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = default!;
        public string? Author { get; set; } = default!;
        public int? Publisher_Id { get; set; } = default!;
        public int? Category_Id { get; set; } = default!;
        public int? Type_Id { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public decimal? Fine { get; set; } = default!;
        public string? Edition { get; set; } = default!;
        public string? Volume { get; set; } = default!;
        public int? Pages { get; set; } = default!;
        public string? Category_Name { get; set; } = default!;
        public string? Publisher_Address { get; set; } = default!;
        public string? Publisher_Name { get; set; } = default!;
        public string? Type_Name { get; set; } = default!;
        public string? Language_Name { get; set; } = default!;
        public int? Language_Id { get; set; } = default!;
        public string? ThumbURL { get; set; } = default!;
        public int? Issued_To { get; set; } = default!;
        public DateTime? Return_Date { get; set; } 
        public DateTime? Issued_Date { get; set; } 
        public DateTime? Due_Date { get; set; } 
        public string? UserName { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? BorrowedBy { get; set; } = default!;
    }
    public class BookPublisherDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Publisher name is required")]
        [StringLength(100, ErrorMessage = "Publisher name max 100 characters")]
        public string Publisher_Name { get; set; } = string.Empty;
        [StringLength(250, ErrorMessage = "Publisher address max 250 characters")]
        public string? Publisher_Address { get; set; } = string.Empty;
    }
    public class BookCategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name max 100 characters")]
        public string Category_Name { get; set; } = string.Empty;
    }

    public class BookTypeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Type name is required")]
        [StringLength(100, ErrorMessage = "Type name max 100 characters")]
        public string Type_Name { get; set; } = string.Empty;
    }
    public class LanguageDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Language name is required")]
        [StringLength(50, ErrorMessage = "Language name max 50 characters")]
        public string Language_Name { get; set; } = string.Empty;
    }
    public class BookIssueDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Book is required")]
        public int? Book_Id { get; set; } = default!;
        [Required(ErrorMessage = "Member is required")]
        public int? Issued_Id { get; set; } = default!;
        [Required(ErrorMessage = "Issue date is required")]
        public DateTime Issue_Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Due date is required")]
        public DateTime Due_Date { get; set; } = DateTime.Now.AddDays(14);
        public DateTime? Return_Date { get; set; } = default!;

    }

    public class BookIssueViewDto
    {
        public int Id { get; set; }
        public int? Book_Id { get; set; } = default!;
        public int? Issued_Id { get; set; } = default!;
        public DateTime Issue_Date { get; set; } = DateTime.Now;
        public DateTime Due_Date { get; set; } = DateTime.Now.AddDays(14);
        public DateTime Return_Date { get; set; } = default!;
        public decimal? FineAmount { get; set; } = default!;
        public string? Book_Title { get; set; } = default!;
        public string? Issued_Name { get; set; } = default!;

        public string? UserName { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? Author { get; set; } = default!;



    }
    public class BookIssueHistoryViewDto
    {
        public int? Id { get; set; }
        public int? Book_Id { get; set; }
        public int? Issued_To { get; set; }
        public DateTime? Issue_Date { get; set; } = DateTime.Now;

        public DateTime? Due_Date { get; set; } = DateTime.Now;
        public DateTime? Return_Date { get; set; } = DateTime.Now;
        public Decimal? Fine { get; set; }
        public string? FullName { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? Title { get; set; } = default!;
        public string? Author { get; set; } = default!;




        //[Required(ErrorMessage = "Category name is required")]
        //[StringLength(100, ErrorMessage = "Category name max 100 characters")]
        //public string Category_Name { get; set; } = string.Empty;
    }
    public class BookRequestDto
    {
        public int Id { get; set; }
        public int? Requested_By { get; set; }
        public int? No_Of_Copies { get; set; }
        public string? Title { get; set; } = default!;
        public DateTime? Date_Requested { get; set; } = DateTime.Now;
        public string? Author { get; set; } = default!;
        public int? Category_Id { get; set; } = default!;
        public int? Language_Id { get; set; } = default!;
        public string? Edition { get; set; } = default!;
        public string? Volume { get; set; } = default!;
        public decimal? Approximate_Price { get; set; } = default!;

    }
}

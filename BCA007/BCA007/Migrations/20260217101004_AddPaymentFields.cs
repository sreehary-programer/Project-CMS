using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCA007.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Course_Name",
                table: "T_Course",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Course_Code",
                table: "T_Course",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BusRoute_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Class_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentPaymentStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HostelRoom_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextPaymentDueDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parent_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "T_Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    PlannedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConductedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodId = table.Column<int>(type: "int", nullable: true),
                    PlannedTeacherId = table.Column<int>(type: "int", nullable: true),
                    ConductedTeacherId = table.Column<int>(type: "int", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Batch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Publisher_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Type_Id = table.Column<int>(type: "int", nullable: false),
                    Language_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    ThumbURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issued_To = table.Column<int>(type: "int", nullable: true),
                    Issued_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Return_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BookCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BookCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BookIssue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    Issued_Id = table.Column<int>(type: "int", nullable: false),
                    Issue_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BookIssue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BookPublisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Publisher_Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BookPublisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BookType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BookType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Bus_Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Route_Id = table.Column<int>(type: "int", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bus_Id = table.Column<int>(type: "int", nullable: false),
                    Student_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Bus_Assignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Bus_Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Route_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Route_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bus_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Bus_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Buses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bus_Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Driver_Id = table.Column<int>(type: "int", nullable: true),
                    Emergency_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Insurance_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Road_Permit_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Next_Service_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course_Id = table.Column<int>(type: "int", nullable: false),
                    Batch_Id = table.Column<int>(type: "int", nullable: false),
                    Semester_Id = table.Column<int>(type: "int", nullable: false),
                    Division_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Classroom",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Classroom", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "T_Division",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Division_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Division", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Exam_Result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Id = table.Column<int>(type: "int", nullable: false),
                    Student_Id = table.Column<int>(type: "int", nullable: true),
                    Marks = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Exam_Result", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ExamTimeTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Class_Id = table.Column<int>(type: "int", nullable: true),
                    ExamType_Id = table.Column<int>(type: "int", nullable: true),
                    Subject_Id = table.Column<int>(type: "int", nullable: true),
                    Session_Id = table.Column<int>(type: "int", nullable: true),
                    Max_Mark = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ExamTimeTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ExamType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ExamType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Fee_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fee_Type_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Fee_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hostal_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type_Id = table.Column<int>(type: "int", nullable: false),
                    Warden_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hostal_Id = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_id = table.Column<int>(type: "int", nullable: false),
                    Student_Fee_Id = table.Column<int>(type: "int", nullable: false),
                    Amount_Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Id = table.Column<int>(type: "int", nullable: false),
                    Receipt_Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Period",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Period", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Staff_Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Staff_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Staff_Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Mode_Id = table.Column<int>(type: "int", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Staff_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Student_Fee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    Fee_Type_Id = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Student_Fee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Timetable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<int>(type: "int", nullable: true),
                    PeriodID = table.Column<int>(type: "int", nullable: true),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    FacultyID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Timetable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "V_Staff_Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaryType = table.Column<string>(name: "Salary Type", type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Mode_Id = table.Column<int>(type: "int", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Staff_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "V_Student_Fee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee_Type_Id = table.Column<int>(type: "int", nullable: false),
                    Fee_Type_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Student_Fee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "V_Student_Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_Fee_Id = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount_Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paid_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Id = table.Column<int>(type: "int", nullable: false),
                    PymntMthd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receipt_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Student_Payment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Activities");

            migrationBuilder.DropTable(
                name: "T_Activity");

            migrationBuilder.DropTable(
                name: "T_Batch");

            migrationBuilder.DropTable(
                name: "T_Book");

            migrationBuilder.DropTable(
                name: "T_BookCategory");

            migrationBuilder.DropTable(
                name: "T_BookIssue");

            migrationBuilder.DropTable(
                name: "T_BookPublisher");

            migrationBuilder.DropTable(
                name: "T_BookType");

            migrationBuilder.DropTable(
                name: "T_Bus_Assignments");

            migrationBuilder.DropTable(
                name: "T_Bus_Routes");

            migrationBuilder.DropTable(
                name: "T_Buses");

            migrationBuilder.DropTable(
                name: "T_Class");

            migrationBuilder.DropTable(
                name: "T_Classroom");

            migrationBuilder.DropTable(
                name: "T_Division");

            migrationBuilder.DropTable(
                name: "T_Exam_Result");

            migrationBuilder.DropTable(
                name: "T_ExamTimeTable");

            migrationBuilder.DropTable(
                name: "T_ExamType");

            migrationBuilder.DropTable(
                name: "T_Fee_Type");

            migrationBuilder.DropTable(
                name: "T_Gender");

            migrationBuilder.DropTable(
                name: "T_Hostel");

            migrationBuilder.DropTable(
                name: "T_Hostel_Rooms");

            migrationBuilder.DropTable(
                name: "T_Hostel_Type");

            migrationBuilder.DropTable(
                name: "T_Language");

            migrationBuilder.DropTable(
                name: "T_Payment");

            migrationBuilder.DropTable(
                name: "T_Period");

            migrationBuilder.DropTable(
                name: "T_Semester");

            migrationBuilder.DropTable(
                name: "T_Session");

            migrationBuilder.DropTable(
                name: "T_Staff_Attendance");

            migrationBuilder.DropTable(
                name: "T_Staff_Payment");

            migrationBuilder.DropTable(
                name: "T_Status");

            migrationBuilder.DropTable(
                name: "T_Student_Fee");

            migrationBuilder.DropTable(
                name: "T_Subject");

            migrationBuilder.DropTable(
                name: "T_Timetable");

            migrationBuilder.DropTable(
                name: "V_Staff_Payment");

            migrationBuilder.DropTable(
                name: "V_Student_Fee");

            migrationBuilder.DropTable(
                name: "V_Student_Payment");

            migrationBuilder.DropColumn(
                name: "BusRoute_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Class_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentPaymentStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HostelRoom_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NextPaymentDueDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileURL",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Course_Name",
                table: "T_Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Course_Code",
                table: "T_Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);
        }
    }
}

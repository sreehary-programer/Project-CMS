using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BCA007.Services.Library
{
    [Authorize]
    public class BookServiceServer : IBookService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BookServiceServer(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<List<BookViewDto>> GetAllAsync()
        {
            return await _db.BooksView
                .Select(x => new BookViewDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Publisher_Id = x.Publisher_Id,
                    Publisher_Address =  x.Publisher_Address,
                    Publisher_Name =  x.Publisher_Name,
                    Category_Id = x.Category_Id,
                    Category_Name =  x.Category_Name ,
                    Type_Id = x.Type_Id,
                    Type_Name =  x.Type_Name ,
                    Language_Id = x.Language_Id,
                    Language_Name =  x.Language_Name ,
                    Price = x.Price,
                    Edition = x.Edition,
                    Volume = x.Volume,
                    Pages = x.Pages,
                    ThumbURL = x.ThumbURL,
                    Issued_To = x.Issued_To,
                    Issued_Date = x.Issued_Date,
                    Due_Date = x.Due_Date,
                    Return_Date = x.Return_Date,
                    BorrowedBy = x.BorrowedBy,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Fine=x.Fine
                })
                .ToListAsync();
        }
        public async Task<BookDto> CreateAsync(BookDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = new BookDto
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                Publisher_Id = dto.Publisher_Id,
                Category_Id = dto.Category_Id,
                Type_Id = dto.Type_Id,
                Language_Id = dto.Language_Id,
                Price = dto.Price,
                Edition = dto.Edition,
                Volume = dto.Volume,
                Pages = dto.Pages,
                ThumbURL = dto.ThumbURL
            };

            try
            {
                _db.Books.Add(entity);
                await _db.SaveChangesAsync();

                dto.Id = entity.Id;
                
                if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
                {
                    var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.Id);
                    entity.ThumbURL = profileUrl;
                    await _db.SaveChangesAsync();
                }
                dto.ThumbURL= entity.ThumbURL;
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        private async Task<string> SaveProfileImageAsync(Stream fileStream, string fileName, int bookid)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "Books");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var ext = Path.GetExtension(fileName);
            var uniqueFileName = $"{bookid:D7}_000{ext}";
            var path = Path.Combine(uploadsFolder, uniqueFileName);
            int count = 0;
            while (File.Exists(path))
            {
                count++;
                uniqueFileName = $"{bookid:D7}_{count:D3}{ext}";
                path = Path.Combine(uploadsFolder, uniqueFileName);
            }

            using (var fileStreamOutput = new FileStream(path, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }
            return $"/uploads/books/{uniqueFileName}";
        }
        public async Task<BookDto> UpdateAsync(BookDto dto, Stream? fileStream, string? fileName)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _db.Books.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book not found");


            entity.Id = dto.Id;
            entity.Title = dto.Title;
            entity.Author = dto.Author;
            entity.Publisher_Id = dto.Publisher_Id;
            entity.Category_Id = dto.Category_Id;
            entity.Type_Id = dto.Type_Id;
            entity.Language_Id = dto.Language_Id;
            entity.Price = dto.Price;
            entity.Edition = dto.Edition;
            entity.Volume = dto.Volume;
            entity.Pages = dto.Pages;
            entity.ThumbURL = dto.ThumbURL;

            if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
            {
                var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.Id);
                entity.ThumbURL = profileUrl;
            }

            await _db.SaveChangesAsync();
            dto.ThumbURL = entity.ThumbURL;
            return dto;
        }
        //public async Task DeleteAsync(int id)
        //{
        //    var entity = await _db.Books.FindAsync(id);
        //    if (entity == null)
        //        throw new KeyNotFoundException("Book not found");

        //    _db.Books.Remove(entity);
        //    await _db.SaveChangesAsync();
        //}
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Books.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book not found");
            var isIssued = await _db.BookIssue.AnyAsync(x => x.Book_Id == id);


            if (isIssued)
                throw new InvalidOperationException("Book is currently issued and cannot be deleted.");


            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
        }
        public async Task<BookDto> UpdateIssueAsync(BookDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto));

                var entity = await _db.Books.FindAsync(dto.Id);
                if (entity == null)
                    throw new KeyNotFoundException("Book not found");


                var prmBook_Id = new SqlParameter("@prmBook_Id", dto.Id);
                var prmIssued_To = new SqlParameter("@prmIssued_To", dto.Issued_To);
                var prmId = new SqlParameter("@prmId", -1)
                {
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
                var prmMessage = new SqlParameter("@prmMessage", "")
                {
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 250
                };
                _db.Database.ExecuteSqlRaw("EXEC BookIssueUpdate @prmBook_Id, @prmIssued_To, @prmId OUTPUT, @prmMessage OUTPUT", prmBook_Id, prmIssued_To, prmId, prmMessage);
                //_db.Courses.Add(entity);

                var msg = (string)prmMessage.Value??string.Empty;
                if (! string.IsNullOrEmpty( msg ))
                {
                    throw new KeyNotFoundException(msg);
                }

                dto.Id = (int)prmId.Value;

                await _db.SaveChangesAsync();

                //entity.Id = dto.Id;
                //entity.Issued_To = dto.Issued_To;
                //entity.Issued_Date = DateTime.Now;

                //_db.Update(entity);
                //await _db.SaveChangesAsync();

                return dto;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<BookDto> UpdateReturnAsync(BookDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _db.Books.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book not found");


            entity.Id = dto.Id;
            entity.Issued_To = null;
            entity.Issued_Date = null;

            _db.Update(entity);
            await _db.SaveChangesAsync();

            return dto;
        }
        }
}

using BCA007.Components.Account;
using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using Humanizer;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BCA007.Services.Library
{
   

    public class BookRequestServiceServer : IBookRequestService
    {
        private readonly ApplicationDbContext _db;
        private object entity;

        public BookRequestServiceServer(ApplicationDbContext db)
        {
            _db = db;

        }
        

        public async Task<BookRequestDto> CreateAsync(BookRequestDto dto)
        {

            //if (await _db.BookRequest.AnyAsync(x => x.Category_Name == dto.Category_Name))
            //    throw new InvalidOperationException("Book Category already exists");

            var entity = new BookRequestDto
            {
                Title = dto.Title,
                Requested_By = dto.Requested_By,
                Author = dto.Author,
                Date_Requested = dto.Date_Requested,
                No_Of_Copies = dto.No_Of_Copies,
                Approximate_Price = dto.Approximate_Price,
                Category_Id = dto.Category_Id,
                Language_Id = dto.Language_Id,
                Edition = dto.Edition,
                Volume = dto.Volume,
            };

            _db.BookRequest.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BookRequest.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Request not found");

            _db.BookRequest.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<BookRequestDto>> GetAllAsync()
        {
            var _List = await _db.BookRequest.Select(c => new BookRequestDto
            {
                Id = c.Id,
                Title = c.Title,
                Requested_By = c.Requested_By,
                Author = c.Author,
                Date_Requested = c.Date_Requested,
                No_Of_Copies = c.No_Of_Copies,
                Approximate_Price = c.Approximate_Price,
                Category_Id = c.Category_Id,
                Language_Id = c.Language_Id,
                Edition = c.Edition,
                Volume = c.Volume,
            }).ToListAsync() ?? [];
            return _List;
        }

        public Task<BookRequestDto> GetByIdAsync(int Bookid)
        {
            throw new NotImplementedException();
        }

        public async Task<BookRequestDto> UpdateAsync(BookRequestDto dto)
        {
            var entity = await _db.BookRequest.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Book Request not found");

            if (entity.Title != dto.Title)
            {
                if (await _db.BookRequest.AnyAsync(x => x.Title == dto.Title))
                    throw new InvalidOperationException("Book Title already exists");
            }

            entity.Title = dto.Title;
            entity.Requested_By = dto.Requested_By;
            entity.Author = dto.Author;
            entity.Date_Requested = dto.Date_Requested;
            entity.No_Of_Copies = dto.No_Of_Copies;
            entity.Approximate_Price = dto.Approximate_Price;
            entity.Category_Id = dto.Category_Id;
            entity.Language_Id = dto.Language_Id;
            entity.Edition = dto.Edition;
            entity.Volume = dto.Volume;

            await _db.SaveChangesAsync();
            return dto;
        }
    }


}

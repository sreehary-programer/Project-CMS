using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BCA007.Services.Hostal
{
    public class HostaltServiceServer : IHostaltService
    {
        private readonly ApplicationDbContext _db;
        public HostaltServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<HostalViewDto>> GetAllAsync()
        {
            return await _db.HostalView
                .AsNoTracking()
                .Select(c => new HostalViewDto
                {
                    Id = c.Id,
                    Capacity = c.Capacity,
                    Room_Number = c.Room_Number,
                    Hostel_Id = c.Hostel_Id,
                    UserName = c.UserName,
                    FullName = c.FullName,
                    Hostel_FullName = c.Hostel_FullName,
                    HostelRoom_Id = c.HostelRoom_Id

                }).ToListAsync() ?? [];
        }
        //public async Task<HostalViewDto> GetByIdAsync(int StudentId)
        //{
        //    return await _db.HostalView
        //        .AsNoTracking()
        //        .Select(c => new HostalViewDto
        //        {
        //            Id = c.Id,
        //            Hostal_Room_Id = c.Hostal_Room_Id,
        //            Student_Id = c.Student_Id,
        //            UserName = c.UserName,
        //            FullName = c.FullName,
        //            Hostal_FullName = c.Hostal_FullName

        //        }).Where(x => x.Student_Id == StudentId)
        //        .FirstOrDefaultAsync()?? new HostalViewDto();
        //}
        public async Task<HostalRoomDto> CreateAsync(HostalRoomDto dto)
        {
            if (dto.Hostel_Id is null )
            {
                throw new ValidationException("All fields are required.");
            }

            //if (await _db.HostalRoom.AnyAsync(x =>
            //   x.Student_Id == dto.Student_Id))
            //{
            //    throw new InvalidOperationException("Student is already allocated.");
            //}

            var entity = new HostalRoomDto
            {
                Capacity = dto.Capacity,
                Hostel_Id = dto.Hostel_Id,
                Room_Number = dto.Room_Number,


            };

            try
            {
                _db.HostalRoom.Add(entity);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Hostal Room already exists.");
            }

            dto.Id = entity.Id;
            return dto;
        }
        public async Task<HostalRoomDto> UpdateAsync(HostalRoomDto dto)
        {
            var entity = await _db.HostalRoom.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Hostalt Room not found");

            entity.Capacity = dto.Capacity;
            entity.Room_Number = dto.Room_Number;
            entity.Hostel_Id = dto.Hostel_Id;



            _db.HostalRoom.Update(entity);
            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            if (await _db.StudentsView.AnyAsync(x => x.HostelRoom_Id == id))
            {
                throw new InvalidOperationException("Cannot delete: Room is occupied by students.");
            }

            var entity = await _db.HostalRoom.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Hostel Room not found");

            try
            {
                _db.HostalRoom.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Cannot delete: Room is in use or referenced by other records.");
            }
        }
        public async Task DeallocateAsync(string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                throw new KeyNotFoundException("Student not found");

            user.HostelRoom_Id = null;
            await _db.SaveChangesAsync();
        }

        
    }

}

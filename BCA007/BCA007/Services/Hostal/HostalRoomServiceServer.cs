using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.HostalRoom
{
    public class HostalRoomServiceServer : IHostalRoomService
    {

        private readonly ApplicationDbContext _db;

        public HostalRoomServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<HostalOccupiedViewDto>> GetAllAsync()
        {
            var _List = await _db.HostalOccView.Select(c => new HostalOccupiedViewDto
            {
                Id = c.Id,
                Hostel_Id = c.Hostel_Id,
                Hostel_Name = c.Hostel_Name,
                Room_Number = c.Room_Number,
                Capacity = c.Capacity,
            }).ToListAsync() ?? [];
            return _List;
        }

        public async Task<HostalRoomDto> CreateAsync(HostalRoomDto dto)
        {
            if (await _db.HostalRoom.AnyAsync(x => x.Room_Number == dto.Room_Number))
                throw new InvalidOperationException("Hostal Room already exists");

            var entity = new HostalRoomDto
            {
                Hostel_Id = dto.Hostel_Id,
                Room_Number = dto.Room_Number,
                Capacity = dto.Capacity,
                
            };

            _db.HostalRoom.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<HostalRoomDto> UpdateAsync(HostalRoomDto dto)
        {
            var entity = await _db.HostalRoom.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Hostal Room not found");

            if (entity.Room_Number != dto.Room_Number)
            {
                if (await _db.HostalRoom.AnyAsync(x => x.Room_Number == dto.Room_Number))
                    throw new InvalidOperationException("HostalRoom name already exists");
            }

            entity.Hostel_Id = dto.Hostel_Id;
            entity.Room_Number = dto.Room_Number;
            entity.Capacity = dto.Capacity;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.HostalRoom.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Hostal Room not found");

            _db.HostalRoom.Remove(entity);
            await _db.SaveChangesAsync();
        }

    }
}


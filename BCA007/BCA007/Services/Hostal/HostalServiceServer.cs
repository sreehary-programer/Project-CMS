using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Hostal
{
    public class HostalServiceServer : IHostalServices
    {
        private readonly ApplicationDbContext _db;

        public HostalServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<HostalDto>> GetAllAsync()
        {
            var _List = await _db.Hostal.Select(c => new HostalDto
            {
                Id = c.Id,
                Hostel_Name = c.Hostel_Name,
                Warden_Name = c.Warden_Name,
                Type_Id = c.Type_Id
            }).ToListAsync() ?? [];
            return _List;
        }

        public async Task<HostalDto> CreateAsync(HostalDto dto)
        {
            if (await _db.Hostal.AnyAsync(x => x.Hostel_Name == dto.Hostel_Name))
                throw new InvalidOperationException("Hostal already exists");

            var entity = new HostalDto
            {
               Warden_Name = dto.Warden_Name,
                Hostel_Name = dto.Hostel_Name,
                Type_Id = dto.Type_Id
            };

            _db.Hostal.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<HostalDto> UpdateAsync(HostalDto dto)
        {
            var entity = await _db.Hostal.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Hostal not found");

            if (entity.Hostel_Name != dto.Hostel_Name)
            {
                if (await _db.Hostal.AnyAsync(x => x.Hostel_Name == dto.Hostel_Name))
                    throw new InvalidOperationException("Hostal name already exists");
            }

            entity.Warden_Name = dto.Warden_Name;
            entity.Hostel_Name = dto.Hostel_Name;
            entity.Type_Id = dto.Type_Id;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Hostal.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Hostal not found");

            _db.Hostal.Remove(entity);
            await _db.SaveChangesAsync();
        }

    }
}

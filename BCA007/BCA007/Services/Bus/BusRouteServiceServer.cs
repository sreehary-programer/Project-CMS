using BCA007.Data;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Bus
{
    public class BusRouteServiceServer : IBusRouteService
    {
        private readonly ApplicationDbContext _db;

        public BusRouteServiceServer(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<BusRouteDto>> GetAllAsync()
        {
            var query = from r in _db.BusRoute
                        join b in _db.Bus on r.Bus_Id equals b.Id into bGroup
                        from b in bGroup.DefaultIfEmpty()
                        select new BusRouteDto
                        {
                            Id = r.Id,
                            Route_Name = r.Route_Name,
                            Route_Price = r.Route_Price,
                            Bus_Id = r.Bus_Id,
                            Bus_Name = b != null ? b.Bus_Name : null
                        };

            return await query.ToListAsync();
        }
        public async Task<BusRouteDto> CreateAsync(BusRouteDto dto)
        {
            if (await _db.BusRoute.AnyAsync(x => x.Route_Name == dto.Route_Name))
                throw new InvalidOperationException("Bus Route already exists");

            var entity = new BusRouteDto
            {
                Route_Name = dto.Route_Name,
                Route_Price = dto.Route_Price,
                Bus_Id = dto.Bus_Id
            };

            _db.BusRoute.Add(entity);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BusRouteDto> UpdateAsync(BusRouteDto dto)
        {
            var entity = await _db.BusRoute.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Bus Route not found");

            if (entity.Route_Name != dto.Route_Name)
            {
                if (await _db.BusRoute.AnyAsync(x => x.Route_Name == dto.Route_Name))
                    throw new InvalidOperationException("Bus Route name already exists");
            }

            entity.Route_Name = dto.Route_Name;
            entity.Route_Price = dto.Route_Price;
            entity.Bus_Id = dto.Bus_Id;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BusRoute.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Bus Route not found");

            _db.BusRoute.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

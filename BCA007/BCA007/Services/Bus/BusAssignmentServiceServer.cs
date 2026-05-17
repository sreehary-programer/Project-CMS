using BCA007.Data;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Bus
{
    public class BusAssignmentServiceServer : IBusAssignmentService
    {
        private readonly ApplicationDbContext _db;

        public BusAssignmentServiceServer(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<List<BusAssignmentDto>> GetAllAsync()
        {
            var _List = await _db.BusAssignment.Select(c => new BusAssignmentDto
            {
                Id = c.Id,
                Route_Id = c.Route_Id,
                Start_Date = c.Start_Date,
                End_Date = c.End_Date,
                Bus_Id = c.Bus_Id,
                Student_Id = c.Student_Id

            }).ToListAsync() ?? [];
            return _List;
        }
        public async Task<BusAssignmentDto> CreateAsync(BusAssignmentDto dto)
        {
            //if (await _db.BusAssignment.AnyAsync(x => x.Assignment_Name == dto.Assignment_Name))
               // throw new InvalidOperationException("Assignment_Name already exists");

            var entity = new BusAssignmentDto
            {
                Route_Id = dto.Route_Id,
                Start_Date = dto.Start_Date,
                End_Date = dto.End_Date,
                Bus_Id = dto.Bus_Id,
                Student_Id = dto.Student_Id
            };

            _db.BusAssignment.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<BusAssignmentDto> UpdateAsync(BusAssignmentDto dto)
        {
            var entity = await _db.BusAssignment.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Bus Assignment not found");

            if (entity.Route_Id != dto.Route_Id)
            {
                if (await _db.BusAssignment.AnyAsync(x => x.Route_Id == dto.Route_Id))
                    throw new InvalidOperationException("Book Category name already exists");
            }

            entity.Route_Id = dto.Route_Id;
            entity.Start_Date = dto.Start_Date;
            entity.End_Date = dto.End_Date;
            entity.Bus_Id = dto.Bus_Id;
             entity.Student_Id = dto.Student_Id;

            await _db.SaveChangesAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.BusAssignment.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book Category not found");

            _db.BusAssignment.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}


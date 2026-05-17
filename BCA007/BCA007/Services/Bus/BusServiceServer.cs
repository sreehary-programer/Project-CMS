using BCA007.Data;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BCA007.Services.Bus
{
    public class BusServiceServer : IBusService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BusServiceServer(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<List<BusViewsDto>> GetAllAsync()
        {
            var query = from b in _db.Bus
                        join ba in _db.BusAssignment on b.Id equals ba.Bus_Id into baGroup
                        from ba in baGroup.DefaultIfEmpty()
                        join br in _db.BusRoute on (ba != null ? ba.Route_Id : 0) equals br.Id into brGroup
                        from br in brGroup.DefaultIfEmpty()
                        join u in _db.Users on b.Driver_Id equals u.Id into uGroup
                        from u in uGroup.DefaultIfEmpty()
                        select new BusViewsDto
                        {
                            Id = b.Id,
                            Bus_id = b.Id,
                            Bus_Name = b.Bus_Name,
                            Capacity = b.Capacity,
                            Route_Name = br != null ? br.Route_Name : null,
                            //Start_Point = br != null ? br.Start_Point : null,
                            //End_Point = br != null ? br.End_Point : null,
                            Start_Date = ba != null ? ba.Start_Date : null,
                            End_date = ba != null ? ba.End_Date : null,
                            Route_Id = br != null ? br.Id : null,
                            
                            Driver_Id = b.Driver_Id,
                            Driver_Name = u != null ? u.FullName : null,
                            Emergency_Number = b.Emergency_Number,
                            Insurance_Expiry_Date = b.Insurance_Expiry_Date,
                            Road_Permit_Expiry_Date = b.Road_Permit_Expiry_Date,
                            Next_Service_Date = b.Next_Service_Date
                        };

            return await query.ToListAsync();
        }
        public async Task<BusDto> CreateAsync(BusDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = new BusDto
            {
                Id = dto.Id,
                Bus_Name = dto.Bus_Name,
                Capacity = dto.Capacity,
                Driver_Id = dto.Driver_Id,
                Emergency_Number = dto.Emergency_Number,
                Insurance_Expiry_Date = dto.Insurance_Expiry_Date,
                Road_Permit_Expiry_Date = dto.Road_Permit_Expiry_Date,
                Next_Service_Date = dto.Next_Service_Date
                
            };

            try
            {
                _db.Bus.Add(entity);
                await _db.SaveChangesAsync();

                dto.Id = entity.Id;

                int parsedRouteId = 0;
                if (dto.CreateNewRoute && !string.IsNullOrEmpty(dto.New_Route_Name))
                {
                    var newRoute = new BusRouteDto
                    {
                        Route_Name = dto.New_Route_Name,
                        //Start_Point = dto.New_Start_Point ?? "",
                        //End_Point = dto.New_End_Point ?? ""
                    };
                    _db.BusRoute.Add(newRoute);
                    await _db.SaveChangesAsync();
                    parsedRouteId = newRoute.Id;
                }
                else if (!string.IsNullOrEmpty(dto.Route_Id))
                {
                    int.TryParse(dto.Route_Id, out parsedRouteId);
                }

                if (parsedRouteId != 0)
                {
                    var assignment = new BusAssignmentDto
                    {
                        Bus_Id = entity.Id,
                        Route_Id = parsedRouteId,
                        Start_Date = dto.Start_Date,
                        End_Date = dto.End_Date,
                        Student_Id = 0
                    };
                    _db.BusAssignment.Add(assignment);
                    await _db.SaveChangesAsync();
                }

              //  if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
              //  {
               //     var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.Id);
               //     entity.ThumbURL = profileUrl;
                    await _db.SaveChangesAsync();
               // }
               //dto.ThumbURL = entity.ThumbURL;
              return dto;
           }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        /* private async Task<string> SaveProfileImageAsync(Stream fileStream, string fileName, int bookid)
         {
             var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "Bus");
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
             return $"/uploads/bus/{uniqueFileName}";
         }*/
        public async Task<BusDto> UpdateAsync(BusDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _db.Bus.FindAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Bus not found");


            entity.Id = dto.Id;
            entity.Id = dto.Id;
            entity.Bus_Name = dto.Bus_Name;
            entity.Capacity = dto.Capacity;
            entity.Driver_Id = dto.Driver_Id;
            entity.Emergency_Number = dto.Emergency_Number;
            entity.Insurance_Expiry_Date = dto.Insurance_Expiry_Date;
            entity.Road_Permit_Expiry_Date = dto.Road_Permit_Expiry_Date;
            entity.Next_Service_Date = dto.Next_Service_Date;
        

           // if (fileStream != null && !string.IsNullOrWhiteSpace(fileName))
         //   {
          //      var profileUrl = await SaveProfileImageAsync(fileStream, fileName, dto.Id);
            //    entity.ThumbURL = profileUrl;
           // }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }

            var routeId = dto.Route_Id;
            
            if (!string.IsNullOrEmpty(routeId) && int.TryParse(routeId, out int rid))
            {
                var existingAssignment = await _db.BusAssignment.FirstOrDefaultAsync(x => x.Bus_Id == entity.Id);
                if (existingAssignment != null)
                {
                     existingAssignment.Route_Id = rid;
                     existingAssignment.Start_Date = dto.Start_Date;
                     existingAssignment.End_Date = dto.End_Date;
                }
                else
                {
                     _db.BusAssignment.Add(new BusAssignmentDto
                     {
                         Bus_Id = entity.Id,
                         Route_Id = rid,
                         Start_Date = dto.Start_Date,
                         End_Date = dto.End_Date,
                         Student_Id = 0
                     });
                }

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                     throw new InvalidOperationException($"Database error (Assignment): {ex.InnerException?.Message ?? ex.Message}");
                }
            }
           return dto;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Bus.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Book not found");

            try
            {
                _db.Bus.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<List<DriverViewDto>> GetDriversAsync()
        {
            return await _db.Users.Select(u => new DriverViewDto
            {
                Id = u.Id,
                FullName = u.FullName ?? u.UserName // Fallback to UserName if FullName is null
            }).ToListAsync();
        }
    }
}

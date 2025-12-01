using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.DispatchServices
{
    public interface IDispatchRepository : IRepository<Dispatch>
    {
        Task<ResponseModel<Dispatch>> GetAllDispatches();
        Task<ResponseModel<Dispatch>> GetDispatchById(Guid id);
        Task<ResponseModel<Dispatch>> GetDriverInfoByNumberPlate(string numberPlate);
    }
    public class DispatchRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Dispatch>(contextFactory), IDispatchRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Dispatch>> GetAllDispatches()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Dispatches
                    .Include(x => x.Booking)
                    .Include(x => x.Booking.Project)
                    .Include(x => x.Carrier)
                    .Include(x => x.Port)
                    .Include(x => x.Customs)
                    .ToListAsync();
                return new ResponseModel<Dispatch>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Dispatch> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Dispatch>> GetDispatchById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Dispatches
                    .Where(x => x.Id == id)
                    .Include(x => x.Booking)
                    .Include(x => x.Carrier)
                    .Include(x => x.Port)
                    .Include(x => x.Customs)
                    .ToListAsync();
                return new ResponseModel<Dispatch>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Dispatch> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Dispatch>> GetDriverInfoByNumberPlate(string numberPlate)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Dispatches
                    .Where(x => x.NumberPlate == numberPlate)
                    .FirstOrDefaultAsync();
                return new ResponseModel<Dispatch>
                {
                    Message = Messages.Received,
                    Entities = [res],
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Dispatch> { Message = err.Message, Success = false };
            }
        }
    }
}

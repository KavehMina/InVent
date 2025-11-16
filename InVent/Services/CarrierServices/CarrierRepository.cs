using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using InVent.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.CarrierServices
{
    public interface ICarrierRepositpry : IRepository<Carrier>
    {
        Task<ResponseModel<Carrier>> GetAllWithBanks();
        Task<ResponseModel<Carrier>> GetWithBank(Guid carrierId);
    }
    public class CarrierRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Carrier>(contextFactory), ICarrierRepositpry
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
        public async Task<ResponseModel<Carrier>> GetAllWithBanks()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Carriers
                    .Include(x => x.Bank)
                    .ToListAsync();
                return new ResponseModel<Carrier> { Message = Messages.Received, Entities = res, Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Carrier> { Message=err.Message, Success = false };
            }

        }

        public async Task<ResponseModel<Carrier>> GetWithBank(Guid carrierId)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Carriers
                    .Where(x => x.Id == carrierId)
                    .Include(x => x.Bank)
                    .FirstOrDefaultAsync();
                return new ResponseModel<Carrier> { Message = Messages.Received, Entities = [res], Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Carrier> { Message = err.Message, Success = false };
            }
        }
    }
}

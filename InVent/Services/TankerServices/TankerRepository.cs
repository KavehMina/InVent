using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using InVent.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.TankerServices
{
    public interface ITankerRepository : IRepository<Tanker>
    {
        Task<ResponseModel<Tanker>> GetAllTankers();
        Task<ResponseModel<Tanker>> GetTankerById(Guid id);

    }
    public class TankerRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Tanker>(contextFactory), ITankerRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Tanker>> GetTankerById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Tankers
                    .Where(x => x.Id == id)
                    .Include(x => x.DriverBank)
                    .Include(x => x.OwnerBank)
                    .FirstOrDefaultAsync();
                return new ResponseModel<Tanker> { Message = Messages.Received, Entities = [res], Success = true };

            }
            catch (Exception err)
            {
                return new ResponseModel<Tanker> { Message = err.Message, Success = false };
            }

        }
        public async Task<ResponseModel<Tanker>> GetAllTankers()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            var list = await context.Tankers
                .Include(x => x.DriverBank)
                .Include(x => x.OwnerBank)                
                .ToListAsync();
            return new ResponseModel<Tanker> { Success = true, Entities = list, Message = Messages.Received };
        }
    }
}

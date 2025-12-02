using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.EntryServices
{
    public interface IEntryRepository : IRepository<Entry>
    {
        Task<ResponseModel<Entry>> GetAllEntries();
        Task<ResponseModel<Entry>> GetEntryById(Guid id);
    }
    public class EntryRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Entry>(contextFactory), IEntryRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Entry>> GetAllEntries()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Entries
                    .Include(x => x.Product)
                    .Include(x => x.Package)
                    .Include(x => x.Refinery)
                    .Include(x => x.DeliveryOrder)
                    .Include(x => x.DeliveryOrder.Project)
                    .Include(x => x.Tanker)
                    .ToListAsync();
                return new ResponseModel<Entry> { Message = Messages.Received, Entities = res, Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Entry> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Entry>> GetEntryById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Entries
                    .Where(x => x.Id == id)
                    .Include(x => x.Product)
                    .Include(x => x.Package)
                    .Include(x => x.Refinery)
                    .Include(x => x.DeliveryOrder)
                    .Include(x => x.Tanker)
                    .ToListAsync();
                return new ResponseModel<Entry> { Message = Messages.Received, Entities = res, Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Entry> { Message = err.Message, Success = false };
            }
        }
    }
}

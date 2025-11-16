using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.EntryServices
{
    public interface IEntryRepository : IRepository<Entry>
    {

    }
    public class EntryRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Entry>(contextFactory), IEntryRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

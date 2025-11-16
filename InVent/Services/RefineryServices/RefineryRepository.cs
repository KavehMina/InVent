using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.RefineryServices
{
    public interface IRefineryRepository : IRepository<Refinery>
    {

    }
    public class RefineryRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Refinery>(contextFactory) , IRefineryRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
    }
}

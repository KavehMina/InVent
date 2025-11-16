using InVent.Data;
using InVent.Data.Models;
using InVent.Services.PackageServices;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.PortServices
{
    public interface IPortRepository : IRepository<Port>
    {

    }
    public class PortRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Port>(contextFactory), IPortRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

using InVent.Data.Models;
using InVent.Data;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.PackageServices
{
    public interface IPackageRepository : IRepository<Package>
    {

    }
    public class PackageRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Package>(contextFactory), IPackageRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

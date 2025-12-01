using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.SupplierServices
{
    public interface ISupplierRepository:IRepository<Supplier>
    {

    }
    public class SupplierRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Supplier>(contextFactory), ISupplierRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
    }
}

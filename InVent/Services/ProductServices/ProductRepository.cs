using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.ProductServices
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public class ProductRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Product>(contextFactory), IProductRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
    }
}

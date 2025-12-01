using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.ProductServices
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ResponseModel<Product>> GetAllProducts();
        Task<ResponseModel<Product>> GetProductById(Guid id);
    }
    public class ProductRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Product>(contextFactory), IProductRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Product>> GetAllProducts()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Products
                    .Include(x => x.Refinery)
                    .ToListAsync();
                return new ResponseModel<Product> { Message = Messages.Received, Entities = res, Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Product> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Product>> GetProductById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Products
                    .Where(x => x.Id == id)
                    .Include(x => x.Refinery)
                    .ToListAsync();
                return new ResponseModel<Product> { Message = Messages.Received, Entities = res, Success = true };
            }
            catch (Exception err)
            {
                return new ResponseModel<Product> { Message = err.Message, Success = false };
            }
        }
    }
}

using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.ProductServices
{
    public class ProductService(IProductRepository repository)
    {
        public async Task<ResponseModel<Product>> GetAll() => await repository.GetAllProducts();
        public async Task<ResponseModel<Product>> GetById(Guid id) => await repository.GetProductById(id);
        public async Task<ResponseModel<Product>> Add(ProductDTO product) => await repository.Add(Mapper.Map(product, new Product()));
        public async Task<ResponseModel<Product>> Update(ProductDTO product) => await repository.Update(Mapper.Map(product, new Product()));
        public async Task<ResponseModel<Product>> Delete(Guid id) => await repository.DeleteById(id);
    }
}

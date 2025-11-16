using InVent.Data.Models;

namespace InVent.Services.ProductServices
{
    public class ProductService(IProductRepository repository)
    {
        public async Task<ResponseModel<Product>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Product>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Product>> Add(Product product) => await repository.Add(product);
        public async Task<ResponseModel<Product>> Update(Product product) => await repository.Update(product);
        public async Task<ResponseModel<Product>> Delete(Product product) => await repository.Delete(product);
    }
}

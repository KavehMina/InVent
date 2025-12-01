using InVent.Data.Models;

namespace InVent.Services.SupplierServices
{
    public class SupplierService(ISupplierRepository repository)
    {
        public async Task<ResponseModel<Supplier>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Supplier>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Supplier>> Add(Supplier supplier) => await repository.Add(supplier);
        public async Task<ResponseModel<Supplier>> Update(Supplier supplier) => await repository.Update(supplier);
        public async Task<ResponseModel<Supplier>> Delete(Guid id) => await repository.DeleteById(id);
    }
}

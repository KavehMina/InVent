using InVent.Data.Models;

namespace InVent.Services.CustomerServices
{
    public class CustomerService(ICustomerRepository repository)
    {
        public async Task<ResponseModel<Customer>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Customer>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Customer>> Add(Customer customer) => await repository.Add(customer);
        public async Task<ResponseModel<Customer>> Update(Customer customer) => await repository.Update(customer);
        public async Task<ResponseModel<Customer>> Delete(Customer customer) => await repository.Delete(customer);
    }
}

using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.CustomerServices
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
    public class CustomerRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Customer>(contextFactory), ICustomerRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
    }
}

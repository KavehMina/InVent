using InVent.Data;
using InVent.Data.Models;
using InVent.Services.RefineryServices;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.CustomsServices
{
    public interface ICustomsRepository : IRepository<Customs>
    {

    }
    public class CustomsRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Customs>(contextFactory), ICustomsRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

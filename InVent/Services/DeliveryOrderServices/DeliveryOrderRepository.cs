using InVent.Data.Models;
using InVent.Data;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.DeliveryOrderServices
{
    public interface IDeliveryOrderRepository : IRepository<DeliveryOrder>
    {

    }
    public class DeliveryOrderRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<DeliveryOrder>(contextFactory), IDeliveryOrderRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

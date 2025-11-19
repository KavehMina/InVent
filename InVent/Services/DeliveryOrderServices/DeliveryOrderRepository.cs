using InVent.Data.Models;
using InVent.Data;
using Microsoft.EntityFrameworkCore;
using InVent.Data.Constants;

namespace InVent.Services.DeliveryOrderServices
{
    public interface IDeliveryOrderRepository : IRepository<DeliveryOrder>
    {
        Task<ResponseModel<DeliveryOrder>> GetDeliveryOrderById(Guid id);
        Task<ResponseModel<DeliveryOrder>> GetAllDeliveryOrders();
    }
    public class DeliveryOrderRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<DeliveryOrder>(contextFactory), IDeliveryOrderRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<DeliveryOrder>> GetAllDeliveryOrders()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.DeliveryOrders
                    .Include(x => x.Project)
                    .ToListAsync();
                return new ResponseModel<DeliveryOrder>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<DeliveryOrder> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<DeliveryOrder>> GetDeliveryOrderById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();

            try
            {
                var res = await context.DeliveryOrders
                    .Where(x => x.Id == id)
                    .Include(x => x.Project)
                    .ToListAsync();
                return new ResponseModel<DeliveryOrder>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<DeliveryOrder> { Message = err.Message, Success = false };
            }
        }
    }
}

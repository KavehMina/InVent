using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.DeliveryOrderServices
{
    public class DeliveryOrderService(IDeliveryOrderRepository repository)
    {
        public async Task<ResponseModel<DeliveryOrder>> GetAll() => await repository.GetAllDeliveryOrders();
        public async Task<ResponseModel<DeliveryOrder>> GetById(Guid id) => await repository.GetDeliveryOrderById(id);
        public async Task<ResponseModel<DeliveryOrder>> Add(DeliveryOrderDTO deliveryOrder) => await repository.Add(Mapper.Map(deliveryOrder,new DeliveryOrder()));
        public async Task<ResponseModel<DeliveryOrder>> Update(DeliveryOrderDTO deliveryOrder) => await repository.Update(Mapper.Map(deliveryOrder, new DeliveryOrder()));
        public async Task<ResponseModel<DeliveryOrder>> Delete(DeliveryOrder deliveryOrder)
        {
            var res = await repository.Delete(deliveryOrder);
            return res.Success ? res : new ResponseModel<DeliveryOrder>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
    }
}

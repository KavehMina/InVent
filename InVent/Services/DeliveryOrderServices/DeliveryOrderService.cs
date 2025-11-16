using InVent.Data.Models;

namespace InVent.Services.DeliveryOrderServices
{
    public class DeliveryOrderService(IDeliveryOrderRepository repository)
    {
        public async Task<ResponseModel<DeliveryOrder>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<DeliveryOrder>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<DeliveryOrder>> Add(DeliveryOrder deliveryOrder) => await repository.Add(deliveryOrder);
        public async Task<ResponseModel<DeliveryOrder>> Update(DeliveryOrder deliveryOrder) => await repository.Update(deliveryOrder);
        public async Task<ResponseModel<DeliveryOrder>> Delete(DeliveryOrder deliveryOrder) => await repository.Delete(deliveryOrder);
    }
}

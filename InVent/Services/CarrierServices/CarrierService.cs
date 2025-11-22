using InVent.Data.Models;

namespace InVent.Services.CarrierServices
{
    public class CarrierService(ICarrierRepositpry repository)
    {
        public async Task<ResponseModel<Carrier>> GetAll() => await repository.GetAllWithBanks();
        public async Task<ResponseModel<Carrier>> GetById(string id) =>
            Guid.TryParse(id, out var CarrierId) ?
                 await repository.GetWithBank(CarrierId) :
                 new ResponseModel<Carrier> { Message = "id اشتباه است.", Success = false };

        public async Task<ResponseModel<Carrier>> AddCarrier(Carrier carrier) => await repository.Add(carrier);//check for existing?
        public async Task<ResponseModel<Carrier>> UpdateCarrier(Carrier carrier) => await repository.Update(carrier);
        public async Task<ResponseModel<Carrier>> DeleteCarrier(Carrier carrier) => await repository.Delete(carrier);

    }
}

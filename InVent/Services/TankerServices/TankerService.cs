using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.TankerServices
{

    public class TankerService(ITankerRepository repository)
    {
        public async Task<ResponseModel<Tanker>> GetAll() => await repository.GetAll();
        //public async Task<ResponseModel<Tanker>> GetTankerById(string id) => await repository.GetById(new Guid(id));
        public async Task<ResponseModel<TankerViewModel>> GetTankerById(string id)
        {

            return Guid.TryParse(id, out var tankerId) ?
                await repository.GetTankerById(tankerId) :
                new ResponseModel<TankerViewModel>() { Message = "id اشتباه است.", Success = false };

        }
        public async Task<ResponseModel<Tanker>> AddTanker(Tanker tanker)
        {
            var res = await repository.Add(tanker);
            return res.Success ? res : new ResponseModel<Tanker>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
        public async Task<ResponseModel<Tanker>> EditTanker(Tanker tanker) => await repository.Update(tanker);
        public async Task<ResponseModel<Tanker>> DeleteTanker(Tanker tanker) => await repository.Delete(tanker);
        public async Task<ResponseModel<TankerViewModel>> GetAllTankersWithBankNames() => await repository.GetAllWithBankNames();

    }
}

using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.TankerServices
{

    public class TankerService(ITankerRepository repository)
    {
        public async Task<ResponseModel<Tanker>> GetAll() => await repository.GetAllTankers();
        public async Task<ResponseModel<Tanker>> GetById(Guid id) => await repository.GetTankerById(id);
        public async Task<ResponseModel<Tanker>> Add(TankerDTO tanker)
        {
            var res = await repository.Add(Mapper.Map(tanker, new Tanker()));
            return res.Success ? res : new ResponseModel<Tanker>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
        public async Task<ResponseModel<Tanker>> Update(TankerDTO tanker) => await repository.Update(Mapper.Map(tanker, new Tanker()));
        public async Task<ResponseModel<Tanker>> Delete(Guid id)
        {
            var res = await repository.DeleteById(id);
            return res.Success ? res : new ResponseModel<Tanker>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }

    }
}

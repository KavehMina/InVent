using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.DispatchServices
{
    public class DispatchService(IDispatchRepository repository)
    {
        public async Task<ResponseModel<Dispatch>> GetAll() => await repository.GetAllDispatches();
        public async Task<ResponseModel<Dispatch>> GetById(Guid id) => await repository.GetDispatchById(id);
        public async Task<ResponseModel<Dispatch>> Add(DispatchDTO dispatch) => await repository.Add(Mapper.Map(dispatch, new Dispatch()));
        public async Task<ResponseModel<Dispatch>> Update(DispatchDTO dispatch) => await repository.Update(Mapper.Map(dispatch, new Dispatch()));
        public async Task<ResponseModel<Dispatch>> Delete(Guid id) => await repository.DeleteById(id);
        public async Task<ResponseModel<Dispatch>> GetDriverInfoByNumberPlate(string numberplate) => await repository.GetDriverInfoByNumberPlate(numberplate);
    }
}

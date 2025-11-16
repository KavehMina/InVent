using InVent.Data.Models;

namespace InVent.Services.RefineryServices
{
    public class RefineryService(IRefineryRepository repository)
    {
        public async Task<ResponseModel<Refinery>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Refinery>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Refinery>> Add(Refinery refinery) => await repository.Add(refinery);
        public async Task<ResponseModel<Refinery>> Update(Refinery refinery) => await repository.Update(refinery);
        public async Task<ResponseModel<Refinery>> Delete(Refinery refinery) => await repository.Delete(refinery);
    }
}

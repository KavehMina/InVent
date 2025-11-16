using InVent.Data.Models;

namespace InVent.Services.PortServices
{
    public class PortService(IPortRepository repository)
    {
        public async Task<ResponseModel<Port>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Port>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Port>> Add(Port port) => await repository.Add(port);
        public async Task<ResponseModel<Port>> Update(Port port) => await repository.Update(port);
        public async Task<ResponseModel<Port>> Delete(Port port) => await repository.Delete(port);
    }
}

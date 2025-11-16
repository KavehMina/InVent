using InVent.Data.Models;

namespace InVent.Services.CustomsServices
{
    public class CustomsService(ICustomsRepository repository)
    {
        public async Task<ResponseModel<Customs>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Customs>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Customs>> Add(Customs customs) => await repository.Add(customs);
        public async Task<ResponseModel<Customs>> Update(Customs customs) => await repository.Update(customs);
        public async Task<ResponseModel<Customs>> Delete(Customs customs) => await repository.Delete(customs);
    }
}

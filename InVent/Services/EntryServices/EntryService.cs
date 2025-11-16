using InVent.Data.Models;
using InVent.Services.ProjectServices;

namespace InVent.Services.EntryServices
{
    public class EntryService(IEntryRepository repository)
    {
        public async Task<ResponseModel<Entry>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Entry>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Entry>> Add(Entry entry) => await repository.Add(entry);
        public async Task<ResponseModel<Entry>> Update(Entry entry) => await repository.Update(entry);
        public async Task<ResponseModel<Entry>> Delete(Entry entry) => await repository.Delete(entry);
    }
}

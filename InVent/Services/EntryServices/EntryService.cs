using InVent.Data.Models;
using InVent.Extensions;
using InVent.Services.ProjectServices;

namespace InVent.Services.EntryServices
{
    public class EntryService(IEntryRepository repository)
    {
        public async Task<ResponseModel<Entry>> GetAll() => await repository.GetAllEntries();
        public async Task<ResponseModel<Entry>> GetById(Guid id) => await repository.GetEntryById(id);
        public async Task<ResponseModel<Entry>> Add(EntryDTO entry) => await repository.Add(Mapper.Map(entry, new Entry()));
        public async Task<ResponseModel<Entry>> Update(EntryDTO entry) => await repository.Update(Mapper.Map(entry, new Entry()));
        public async Task<ResponseModel<Entry>> Delete(Entry entry) => await repository.Delete(entry);

    }
}

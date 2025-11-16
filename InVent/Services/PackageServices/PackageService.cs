using InVent.Data.Models;

namespace InVent.Services.PackageServices
{
    public class PackageService(IPackageRepository repository)
    {
        public async Task<ResponseModel<Package>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Package>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Package>> Add(Package package) => await repository.Add(package);
        public async Task<ResponseModel<Package>> Update(Package package) => await repository.Update(package);
        public async Task<ResponseModel<Package>> Delete(Package package) => await repository.Delete(package);
    }
}

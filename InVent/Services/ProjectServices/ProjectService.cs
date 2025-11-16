using InVent.Data.Models;
using InVent.Services.DeliveryOrderServices;

namespace InVent.Services.ProjectServices
{
    public class ProjectService(IProjectRepository repository)
    {
        public async Task<ResponseModel<Project>> GetAll() => await repository.GetAll();
        public async Task<ResponseModel<Project>> GetById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Project>> Add(Project project) => await repository.Add(project);
        public async Task<ResponseModel<Project>> Update(Project project) => await repository.Update(project);
        public async Task<ResponseModel<Project>> Delete(Project project) => await repository.Delete(project);
    }
}

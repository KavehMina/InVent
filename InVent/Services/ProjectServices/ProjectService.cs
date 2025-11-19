using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.ProjectServices
{
    public class ProjectService(IProjectRepository repository)
    {
        public async Task<ResponseModel<Project>> GetAll() => await repository.GetAllProjects();
        public async Task<ResponseModel<Project>> GetById(Guid id) => await repository.GetProjectByID(id);
        public async Task<ResponseModel<Project>> Add(ProjectDTO project) => await repository.Add(Mapper.Map(project, new Project()));
        public async Task<ResponseModel<Project>> Update(ProjectDTO project) => await repository.Update(Mapper.Map(project, new Project()));
        public async Task<ResponseModel<Project>> Delete(Project project) => await repository.Delete(project);
    }
}

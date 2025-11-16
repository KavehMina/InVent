using InVent.Data.Models;
using InVent.Data;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.ProjectServices
{
    public interface IProjectRepository : IRepository<Project>
    {

    }
    public class ProjectRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Project>(contextFactory), IProjectRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

    }
}

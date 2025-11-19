using InVent.Data.Models;
using InVent.Data;
using Microsoft.EntityFrameworkCore;
using InVent.Data.Constants;

namespace InVent.Services.ProjectServices
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<ResponseModel<Project>> GetProjectByID(Guid id);
        Task<ResponseModel<Project>> GetAllProjects();
    }
    public class ProjectRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Project>(contextFactory), IProjectRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Project>> GetAllProjects()
        {
            using var context = contextFactory.CreateDbContext();

            try
            {
                var res = await context.Projects
                    .Include(x => x.Customer)
                    .Include(x => x.Customs)
                    .Include(x => x.Product)
                    .Include(x => x.Port)
                    .Include(x => x.Package)
                    .ToListAsync();
                return new ResponseModel<Project>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Project> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Project>> GetProjectByID(Guid id)
        {
            using var context = contextFactory.CreateDbContext();

            try
            {
                var res = await context.Projects
                    .Where(x => x.Id == id)
                    .Include(x => x.Customer)
                    .Include(x => x.Customs)
                    .Include(x => x.Product)
                    .Include(x => x.Port)
                    .Include(x => x.Package)
                    .ToListAsync();
                return new ResponseModel<Project>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Project> { Message = err.Message, Success = false };
            }
        }
    }
}

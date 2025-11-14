using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Charts;

namespace InVent.Services
{

    public interface IRepository<T> where T : class, IEntity
    {
        Task<ResponseModel<T>> GetById(Guid id);
        Task<ResponseModel<T>> GetAll();
        Task<ResponseModel<T>> Add(T entity);
        Task<ResponseModel<T>> Update(T entity);
        Task<ResponseModel<T>> Delete(T entity);
    }
    public class Repository<T>(IDbContextFactory<EntityDBContext> contextFactory) : IRepository<T> where T : class, IEntity
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<T>> Add(T entity)
        {
            using var context = this.contextFactory.CreateDbContext();
            try
            {
                //var res = await context.AddAsync(entity);
                var res = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                var response = new ResponseModel<T>
                {
                    Success = true,
                    Entities = [res.Entity],
                    Message = Messages.Add
                };
                return response;
            }
            catch (Exception err)
            {
                return new ResponseModel<T>
                {
                    Success = false,
                    Message = err.Message + Environment.NewLine + err.InnerException?.Message,
                };
            }
        }



        public async Task<ResponseModel<T>> GetAll()
        {
            using var context = this.contextFactory.CreateDbContext();
            try
            {
                var res = await context.Set<T>().ToListAsync();
                var response = new ResponseModel<T>
                {
                    Success = true,
                    Entities = res,
                    Message = Messages.Received
                };
                return response;
            }
            catch (Exception err)
            {
                return new ResponseModel<T>
                {
                    Success = false,
                    Message = err.Message + Environment.NewLine + err.InnerException?.Message,
                };
            }
        }

        public async Task<ResponseModel<T>> GetById(Guid id)
        {
            using var context = this.contextFactory.CreateDbContext();
            try
            {
                var res = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                var response = new ResponseModel<T>
                {
                    Success = true,
                    Entities = [res],
                    Message = Messages.Received
                };
                return response;
            }
            catch (Exception err)
            {
                return new ResponseModel<T>
                {
                    Success = false,
                    Message = err.Message + Environment.NewLine + err.InnerException?.Message,
                };
            }
        }

        public async Task<ResponseModel<T>> Delete(T entity)
        {
            using var context = this.contextFactory.CreateDbContext();
            try
            {
                var res = context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                var response = new ResponseModel<T>
                {
                    Success = true,
                    Message = Messages.Delete
                };
                return response;
            }
            catch (Exception err)
            {
                return new ResponseModel<T>
                {
                    Success = false,
                    Message = err.Message + Environment.NewLine + err.InnerException?.Message,
                };
            }
        }

        public async Task<ResponseModel<T>> Update(T entity)
        {
            {
                using var context = this.contextFactory.CreateDbContext();
                try
                {
                    var res = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
                    if (res != null)
                    {
                        context.Entry(res).CurrentValues.SetValues(entity);
                        context.Entry(res).Property(x => x.Id).IsModified = false;
                        await context.SaveChangesAsync();
                    }
                    var response = new ResponseModel<T>
                    {
                        Success = true,
                        Message = Messages.Updated,
                        Entities = [res]
                    };
                    return response;

                }
                catch (Exception err)
                {
                    return new ResponseModel<T>
                    {
                        Success = false,
                        Message = err.Message + Environment.NewLine + err.InnerException?.Message,
                    };
                }
            }
        }

    }
}

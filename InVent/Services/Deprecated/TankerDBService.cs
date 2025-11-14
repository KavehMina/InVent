//using InVent.Data;
//using InVent.Data.Constants;
//using InVent.Data.Models;
//using Microsoft.EntityFrameworkCore;

//namespace InVent.Services
//{
//    public interface ITankerDBService
//    {
//        Task<TankerResponseModel> GetTanker(Guid id);
//        Task<TankerResponseModel> GetAllTankers();
//        Task<TankerResponseModel> AddTanker(Tanker tanker);
//        Task<TankerResponseModel> UpdateTanker(Tanker tanker);
//        Task<TankerResponseModel> DeleteTanker(Tanker tanker);
//    }


//    public class TankerDBService(IDbContextFactory<EntityDBContext> context) : ITankerDBService
//    {
//        private readonly IDbContextFactory<EntityDBContext> dbFactory = context;

//        public async Task<TankerResponseModel> AddTanker(Tanker tanker)
//        {
//            using var context = this.dbFactory.CreateDbContext();
//            try
//            {
//                var res = await context.AddAsync(tanker);
//                await context.SaveChangesAsync();
//                var response = new TankerResponseModel
//                {
//                    Success = true,
//                    Tankers = [res.Entity],
//                    Message = Messages.Add
//                };
//                return response;
//            }
//            catch (Exception err)
//            {
//                return new TankerResponseModel
//                {
//                    Success = false,
//                    Message = err.Message + Environment.NewLine + err.InnerException?.Message,
//                };
//            }
//        }
//        public async Task<TankerResponseModel> GetTanker(Guid id)
//        {
//            using var context = this.dbFactory.CreateDbContext();
//            try
//            {
//                var res = await context.Tankers.FirstOrDefaultAsync(x => x.Id == id);
//                var response = new TankerResponseModel
//                {
//                    Success = true,
//                    Tankers = [res],
//                    Message = Messages.Received
//                };
//                return response;
//            }
//            catch (Exception err)
//            {
//                return new TankerResponseModel
//                {
//                    Success = false,
//                    Message = err.Message,
//                };
//            }
//        }

//        public async Task<TankerResponseModel> GetAllTankers()
//        {
//            using var context = this.dbFactory.CreateDbContext();            
//            try
//            {
//                var res = await context.Tankers.ToListAsync();
//                var response = new TankerResponseModel
//                {
//                    Success = true,
//                    Tankers = res,
//                    Message = Messages.Received
//                };
//                return response;
//            }
//            catch (Exception err)
//            {
//                return new TankerResponseModel
//                {
//                    Success = false,
//                    Message = err.Message,
//                };
//            }
//        }

//        public async Task<TankerResponseModel> UpdateTanker(Tanker tanker)
//        {
//            using var context = this.dbFactory.CreateDbContext();
//            try
//            {
//                var res = await context.Tankers.FirstOrDefaultAsync(x => x.Id == tanker.Id);
//                if (res != null)
//                {
//                    context.Entry(res).CurrentValues.SetValues(tanker);
//                    context.Entry(res).Property(x => x.Id).IsModified = false;
//                    await context.SaveChangesAsync();
//                }
//                var response = new TankerResponseModel
//                {
//                    Success = true,
//                    Message = Messages.Updated,
//                    Tankers = [res]
//                };
//                return response;

//            }
//            catch (Exception err)
//            {
//                return new TankerResponseModel
//                {
//                    Success = false,
//                    Message = err.Message,
//                };
//            }
//        }
//        public async Task<TankerResponseModel> DeleteTanker(Tanker tanker)
//        {
//            using var context = this.dbFactory.CreateDbContext();
//            try
//            {
//                var res = context.Tankers.Remove(tanker);
//                await context.SaveChangesAsync();
//                var response = new TankerResponseModel
//                {
//                    Success = true,
//                    Message = Messages.Delete
//                };
//                return response;
//            }
//            catch (Exception err)
//            {
//                return new TankerResponseModel
//                {
//                    Success = false,
//                    Message = err.Message
//                };
//            }
//        }
//    }
//}

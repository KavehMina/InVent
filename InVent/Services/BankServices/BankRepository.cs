using InVent.Data;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace InVent.Services.BankServices
{
    public interface IBankRepository : IRepository<Bank>
    {
        Task<ResponseModel<Bank>> TestBank();
    }
    public class BankRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Bank>(contextFactory), IBankRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
        public async Task<ResponseModel<Bank>> TestBank()
        {
            try
            {
                throw new Exception("TEST", new Exception("InnerException Test"));
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error($"err.Message: {err.Message}");
            }
            using var context = contextFactory.CreateDbContext();
            var res = await context.Banks.ToListAsync();
            return new ResponseModel<Bank> { Message = "", Success = true };
        }
    }
}

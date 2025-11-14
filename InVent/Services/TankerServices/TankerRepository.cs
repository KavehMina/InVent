using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using InVent.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.TankerServices
{
    public interface ITankerRepository : IRepository<Tanker>
    {
        Task<ResponseModel<TankerViewModel>> GetAllWithBankNames();

    }
    public class TankerRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Tanker>(contextFactory), ITankerRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
        public async Task<ResponseModel<TankerViewModel>> GetAllWithBankNames()
        {
            using var context = contextFactory.CreateDbContext();
            var list = await context.Tankers
                .Select(x => Mapper.Map(x, new TankerViewModel { CargoType = "", DriverName = "", DriverPhone = "", Number = "" }))
                //.Select(x => new TankerViewModel()
                //{
                //    Id = x.Id,
                //    Number = x.Number,
                //    DriverName = x.DriverName,
                //    DriverPhone = x.DriverPhone,
                //    DriverBankNumber = x.DriverBankNumber,
                //    DriverBankId = x.DriverBankId,
                //    OwnerName = x.OwnerName,
                //    OwnerPhone = x.OwnerPhone,
                //    OwnerBankNumber = x.OwnerBankNumber,
                //    OwnerBankId = x.OwnerBankId,
                //    CargoType = x.CargoType,
                //})
                .ToListAsync();

            return new ResponseModel<TankerViewModel> { Success = true, Entities = list, Message = Messages.Received };
        }
    }
}

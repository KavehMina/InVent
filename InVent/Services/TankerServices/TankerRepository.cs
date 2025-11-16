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
        Task<ResponseModel<TankerViewModel>> GetTankerById(Guid id);

    }
    public class TankerRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Tanker>(contextFactory), ITankerRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<TankerViewModel>> GetTankerById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Tankers
                    .Where(x => x.Id == id)
                    .Include(x => x.DriverBank)
                    .Include(x => x.OwnerBank)
                    .Select(x => Mapper.Map(x, new TankerViewModel
                    {
                        CargoType = "",
                        DriverName = "",
                        DriverPhone = "",
                        Number = "",
                        DriverBankName = x.DriverBank != null ? x.DriverBank.Name : "",
                        OwnerBankName = x.OwnerBank != null ? x.OwnerBank.Name : ""
                    }))
                    .FirstOrDefaultAsync();
                return new ResponseModel<TankerViewModel> { Message = Messages.Received, Entities = [res], Success = true };

            }
            catch (Exception err)
            {
                return new ResponseModel<TankerViewModel> { Message = err.Message, Success = false };
            }

        }
        public async Task<ResponseModel<TankerViewModel>> GetAllWithBankNames()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            var list = await context.Tankers
                .Include(x => x.DriverBank)
                .Include(x => x.OwnerBank)
                .Select(x => Mapper.Map(x, new TankerViewModel
                {
                    CargoType = "",
                    DriverName = "",
                    DriverPhone = "",
                    Number = "",
                    DriverBankName = x.DriverBank != null ? x.DriverBank.Name : "",
                    OwnerBankName = x.OwnerBank != null ? x.OwnerBank.Name : ""
                }))
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
                //    DriverBankName = x.DriverBank != null ? x.DriverBank.Name : "",
                //    OwnerBankName = x.OwnerBank != null ? x.OwnerBank.Name : ""
                //})
                .ToListAsync();


            //var list = await context.Tankers
            //.Select(x => Mapper.Map(x, new TankerViewModel { CargoType = "", DriverName = "", DriverPhone = "", Number = "" }))


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
            //.ToListAsync();

            return new ResponseModel<TankerViewModel> { Success = true, Entities = list, Message = Messages.Received };
        }
    }
}

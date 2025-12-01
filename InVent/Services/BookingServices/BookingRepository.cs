using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.BookingServices
{
    public interface IBookingRepository: IRepository<Booking>
    {
        Task<ResponseModel<Booking>> GetAllBookings();
        Task<ResponseModel<Booking>> GetBookingById(Guid id);
    }
    public class BookingRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Booking>(contextFactory) ,IBookingRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Booking>> GetAllBookings()
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Bookings
                    .Include(x => x.Project)
                    .Include(x => x.Project.Product)
                    .Include(x => x.Project.Customer)
                    .Include(x => x.Project.Package)
                    .ToListAsync();
                return new ResponseModel<Booking>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Booking> { Message = err.Message, Success = false };
            }
        }

        public async Task<ResponseModel<Booking>> GetBookingById(Guid id)
        {
            using var context = contextFactory.CreateDbContext();

            try
            {
                var res = await context.Bookings
                    .Where(x => x.Id == id)
                    .Include(x => x.Project)
                    .ToListAsync();
                return new ResponseModel<Booking>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Booking> { Message = err.Message, Success = false };
            }
        }
    }
}

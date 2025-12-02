using InVent.Components.Pages.DeliveryOrderEntity;
using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.BookingServices
{
    public class BookingService(IBookingRepository repository)
    {
        public async Task<ResponseModel<Booking>> GetAll() => await repository.GetAllBookings();
        public async Task<ResponseModel<Booking>> GetByProject(Guid projectId) => await repository.GetBookingsByProject(projectId);
        public async Task<ResponseModel<Booking>> GetById(Guid id) => await repository.GetBookingById(id);
        public async Task<ResponseModel<Booking>> Add(BookingDTO booking) => await repository.Add(Mapper.Map(booking, new Booking()));
        public async Task<ResponseModel<Booking>> Update(BookingDTO booking) => await repository.Update(Mapper.Map(booking, new Booking()));
        public async Task<ResponseModel<Booking>> Delete(Guid id)
        {
            var res = await repository.DeleteById(id);
            return res.Success ? res : new ResponseModel<Booking>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
    }
}

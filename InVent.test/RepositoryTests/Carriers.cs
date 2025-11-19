using InVent.Data.Models;
using InVent.Services.CarrierServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests
{
    internal class Carriers : BaseTest
    {
        private CarrierService? Service => Host.Services.GetService<CarrierService>();

    }
}

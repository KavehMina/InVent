using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using InVent.Services.BankServices;
using InVent.Services;
using Microsoft.Extensions.DependencyInjection;
using InVent.Data;
using Microsoft.EntityFrameworkCore;
using InVent.Services.CarrierServices;
using InVent.Services.CustomerServices;
using InVent.Services.CustomsServices;
using InVent.Services.DeliveryOrderServices;
using InVent.Services.EntryServices;
using InVent.Services.PackageServices;
using InVent.Services.ProductServices;
using InVent.Services.ProjectServices;
using InVent.Services.RefineryServices;
using InVent.Services.TankerServices;
using InVent.Services.PortServices;
using InVent.Services.BookingServices;


namespace InVent.test.RepositoryTests;

[TestClass]
public class BaseTest
{
    readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-InVent-b16ffdae-8176-460a-87c3-429d51321939;Trusted_Connection=True;MultipleActiveResultSets=true";

    private IHost GetHost() => new HostBuilder()
        .UseDefaultServiceProvider(s => s.ValidateScopes = false)
        .ConfigureAppConfiguration(a => a.AddJsonFile("appsettings.json"))
        .ConfigureServices((c, s) =>
        {
            s.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //Tanker
            s.AddScoped<ITankerRepository, TankerRepository>();
            s.AddScoped<TankerService>();
            //Bank
            s.AddScoped<IBankRepository, BankRepository>();
            s.AddScoped<BankService>();
            //Carrier
            s.AddScoped<ICarrierRepositpry, CarrierRepository>();
            s.AddScoped<CarrierService>();
            //Customer
            s.AddScoped<ICustomerRepository, CustomerRepository>();
            s.AddScoped<CustomerService>();
            //Product
            s.AddScoped<IProductRepository, ProductRepository>();
            s.AddScoped<ProductService>();
            //Refinery
            s.AddScoped<IRefineryRepository, RefineryRepository>();
            s.AddScoped<RefineryService>();
            //Customs
            s.AddScoped<ICustomsRepository, CustomsRepository>();
            s.AddScoped<CustomsService>();
            //Ports
            s.AddScoped<IPortRepository, PortRepository>();
            s.AddScoped<PortService>();
            //Package
            s.AddScoped<IPackageRepository, PackageRepository>();
            s.AddScoped<PackageService>();
            //DeliveryOrder
            s.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
            s.AddScoped<DeliveryOrderService>();
            //Project
            s.AddScoped<IProjectRepository, ProjectRepository>();
            s.AddScoped<ProjectService>();
            //Entry
            s.AddScoped<IEntryRepository, EntryRepository>();
            s.AddScoped<EntryService>();
            //Booking
            s.AddScoped<IBookingRepository, BookingRepository>();
            s.AddScoped<BookingService>();
            s.AddDbContextFactory<EntityDBContext>(options => options.UseSqlServer(connectionString));
        }).Build();
    public IHost Host => GetHost();
}

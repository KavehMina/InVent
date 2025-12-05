using InVent.Components;
using InVent.Components.Account;
using InVent.Data;
using InVent.Services;
using InVent.Services.AttachmentServices;
using InVent.Services.BankServices;
using InVent.Services.BookingServices;
using InVent.Services.CarrierServices;
using InVent.Services.CustomerServices;
using InVent.Services.CustomsServices;
using InVent.Services.DeliveryOrderServices;
using InVent.Services.DispatchServices;
using InVent.Services.EntryServices;
using InVent.Services.PackageServices;
using InVent.Services.PortServices;
using InVent.Services.ProductServices;
using InVent.Services.ProjectServices;
using InVent.Services.RefineryServices;
using InVent.Services.SupplierServices;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;


//096 starts
var logger = LogManager.Setup()
                       .LoadConfigurationFromFile("NLog.config")
                       .GetCurrentClassLogger();

//096 ends



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
///096 starts
//NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog()
    .ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddNLog();
});
builder.Logging.AddFilter<NLogLoggerProvider>(
    "Microsoft.*",
    Microsoft.Extensions.Logging.LogLevel.None);
//Generic
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//Tanker
builder.Services.AddScoped<ITankerRepository, TankerRepository>();
builder.Services.AddScoped<TankerService>();
//Bank
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<BankService>();
//Carrier
builder.Services.AddScoped<ICarrierRepositpry, CarrierRepository>();
builder.Services.AddScoped<CarrierService>();
//Customer
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();
//Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
//Refinery
builder.Services.AddScoped<IRefineryRepository, RefineryRepository>();
builder.Services.AddScoped<RefineryService>();
//Customs
builder.Services.AddScoped<ICustomsRepository, CustomsRepository>();
builder.Services.AddScoped<CustomsService>();
//Ports
builder.Services.AddScoped<IPortRepository, PortRepository>();
builder.Services.AddScoped<PortService>();
//Package
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<PackageService>();
//DeliveryOrder
builder.Services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
builder.Services.AddScoped<DeliveryOrderService>();
//Project
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectService>();
//Entry
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<EntryService>();
//Booking
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingService>();
//Dispatch
builder.Services.AddScoped<IDispatchRepository, DispatchRepository>();
builder.Services.AddScoped<DispatchService>();
//Attachment
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<AttachmentService>();
//Attachment
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<SupplierService>();
//User
builder.Services.AddScoped<UserService>();
///096 ends
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
///096 starts
builder.Services.AddDbContextFactory<EntityDBContext>(options =>
{
    options.UseSqlServer(connectionString);
    //options.LogTo(_ => { }, Microsoft.Extensions.Logging.LogLevel.None);
}
);
///096 ends
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    //.AddRoleStore<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddMudServices();
var app = builder.Build();

//096 starts

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = new[] { "admin", "user" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }


    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminEmail = "amin@dbgulf.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, "123@Aa");
        await userManager.AddToRoleAsync(adminUser, "admin");
    }

}


//096 ends

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

using InVent.Data.Models;
using InVent.Extensions;
using InVent.Services.EntryServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Entries : BaseTest
{
    private EntryService? Service => Host.Services.GetService<EntryService>();
    private string id = "bfc8d347-471b-4c93-8dbc-08de262f23e9";
    [TestMethod]
    public async Task Add()
    {
        var item = new EntryDTO
        {
            Damaged = 30,
            Date = DateTime.Now,
            Filled = 30,
            DeliveryOrderId = new Guid("1397cd12-0a44-46ab-ee24-08de2627579e"),
            PackageTypeId = new Guid("f97504ea-4a27-43a7-4754-08de25768254"),
            ProductId = new Guid("74e5a8b3-39e5-4126-669c-08de25781907"),
            RefineryId = new Guid("8d4a71a8-eb89-4883-1cee-08de2578cfe0"),
            TankerId = new Guid("eaa9dcd9-f894-4914-5f35-08de2403babc"),
            RefineryEmpty = 30,
            RefineryFilled = 30,
            WarehouseEmpty = 30,
            WarehouseFilled = 30,

        };
        var res = await this.Service.Add(item);

    }

    [TestMethod]
    public async Task GetAll()
    {
        var res = await this.Service.GetAll();

    }
    [TestMethod]
    public async Task GetById()
    {
        var res = await this.Service.GetById(new Guid(this.id));

    }
    [TestMethod]
    public async Task Update()
    {
        var tar = (await this.Service.GetById(new Guid(this.id))).Entities.FirstOrDefault();
        tar.Damaged = 666;
        await this.Service.Update(Mapper.Map(tar, new EntryDTO
        {
            Damaged = 10,
            Date = DateTime.Now,
            Filled = 20,
            DeliveryOrderId = new Guid("1397cd12-0a44-46ab-ee24-08de2627579e"),
            PackageTypeId = new Guid("f97504ea-4a27-43a7-4754-08de25768254"),
            ProductId = new Guid("74e5a8b3-39e5-4126-669c-08de25781907"),
            RefineryId = new Guid("8d4a71a8-eb89-4883-1cee-08de2578cfe0"),
            TankerId = new Guid("eaa9dcd9-f894-4914-5f35-08de2403babc"),
            RefineryEmpty = 10,
            RefineryFilled = 20,
            WarehouseEmpty = 10,
            WarehouseFilled = 20,
        }));

    }

    [TestMethod]
    public async Task Delete()
    {
        var tar = (await this.Service.GetById(new Guid(this.id))).Entities.FirstOrDefault();
        tar.Damaged = 666;
        //await this.Service.Delete(Mapper.Map(tar, new EntryDTO
        //{
        //    Damaged = 10,
        //    Date = DateTime.Now,
        //    Filled = 20,
        //    DeliveryOrderId = new Guid("1397cd12-0a44-46ab-ee24-08de2627579e"),
        //    PackageTypeId = new Guid("f97504ea-4a27-43a7-4754-08de25768254"),
        //    ProductId = new Guid("74e5a8b3-39e5-4126-669c-08de25781907"),
        //    RefineryId = new Guid("8d4a71a8-eb89-4883-1cee-08de2578cfe0"),
        //    TankerId = new Guid("eaa9dcd9-f894-4914-5f35-08de2403babc"),
        //    RefineryEmpty = 10,
        //    RefineryFilled = 20,
        //    WarehouseEmpty = 10,
        //    WarehouseFilled = 20,
        //}));

    }
}

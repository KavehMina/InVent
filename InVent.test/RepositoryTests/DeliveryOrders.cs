using InVent.Data.Models;
using InVent.Extensions;
using InVent.Services.DeliveryOrderServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class DeliveryOrders : BaseTest
{
    private DeliveryOrderService? Service => Host.Services.GetService<DeliveryOrderService>();
    private string id = "d2273ed3-7ad1-4687-40fe-08de26270a76";
    [TestMethod]
    public async Task Add()
    {
        var item = new DeliveryOrderDTO
        {
            DeliveryOrderId = "667",
            Status = false,
            Weight = 666,
            ProjectId = new Guid("3343dfca-f71a-4d3a-5595-08de25a2f2ac")
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
        tar.Weight = 500;
        await this.Service.Update(Mapper.Map(tar, new DeliveryOrderDTO
        {
            DeliveryOrderId = "123",
            Status = true,
            Weight = 100,
            ProjectId = new Guid("3343dfca-f71a-4d3a-5595-08de25a2f2ac")
        }));

    }

    [TestMethod]
    public async Task Delete()
    {
        //var tar = (await this.Service.GetById(new Guid(this.id))).Entities.FirstOrDefault();
        //await this.Service.Delete(Mapper.Map(tar, new DeliveryOrderDTO
        //{
        //    DeliveryOrderId = "123",
        //    Status = true,
        //    Weight = 100,
        //    ProjectId = new Guid("3343dfca-f71a-4d3a-5595-08de25a2f2ac")
        //}));

    }
}

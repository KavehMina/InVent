
using InVent.Data.Models;
using InVent.Extensions;
using InVent.Services.ProjectServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Projects : BaseTest
{
    private ProjectService? Service => Host.Services.GetService<ProjectService>();
    private Guid id = new Guid("3343dfca-f71a-4d3a-5595-08de25a2f2ac");
    [TestMethod]
    public async Task Add()
    {
        var item = new ProjectDTO
        {
            CustomerId = new Guid("9b274bd0-63b6-4817-3227-08de2570f777"),
            CustomsId = new Guid("7d0fa990-565b-4bf1-73d8-08de2573add0"),
            PackageId = new Guid("f97504ea-4a27-43a7-4754-08de25768254"),
            PortId = new Guid("341d1cc4-1635-4fac-63ee-08de2577a188"),
            ProductId = new Guid("74e5a8b3-39e5-4126-669c-08de25781907"),
            Status = true
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
        var res = await this.Service.GetById(id);
    }
    [TestMethod]
    public async Task Update()
    {
        var tar = (await this.Service.GetById(id)).Entities.FirstOrDefault();
        tar.Number = 18;
        await this.Service.Update(Mapper.Map(tar, new ProjectDTO
        {
            CustomerId =Guid.Empty,
            CustomsId = Guid.Empty,
            PackageId = Guid.Empty,
            PortId = Guid.Empty,
            ProductId = Guid.Empty,
            Status = true
        }));

    }

    [TestMethod]
    public async Task Delete()
    {
        var tar = (await this.Service.GetById(id)).Entities.FirstOrDefault();
        await this.Service.Delete(Mapper.Map(tar, new ProjectDTO
        {
            CustomerId = Guid.Empty,
            CustomsId = Guid.Empty,
            PackageId = Guid.Empty,
            PortId = Guid.Empty,
            ProductId = Guid.Empty,
            Status = true
        }));

    }
}

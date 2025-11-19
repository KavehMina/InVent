using InVent.Data.Models;
using InVent.Services.RefineryServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Refineries : BaseTest
{
    private RefineryService? Service => Host.Services.GetService<RefineryService>();
    private string id = "6d96d2e0-ff4a-46d8-d166-08de25788f98";
    [TestMethod]
    public async Task Add()
    {
        var item = new Refinery { Name = "Test2", City = "Teh" };
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
        tar.Name = "Test-Edited";
        await this.Service.Update(tar);

    }

    [TestMethod]
    public async Task Delete()
    {
        var tar = (await this.Service.GetById(new Guid(this.id))).Entities.FirstOrDefault();
        await this.Service.Delete(tar);

    }
}

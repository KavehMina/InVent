using InVent.Data.Models;
using InVent.Services.PortServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Ports : BaseTest
{
    private PortService? Service => Host.Services.GetService<PortService>();
    private string id = "341d1cc4-1635-4fac-63ee-08de2577a188";
    [TestMethod]
    public async Task Add()
    {
        var item = new Port { Name = "Test", Number = 666 };
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

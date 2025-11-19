using InVent.Data.Models;
using InVent.Services.PackageServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Packages : BaseTest
{
    private PackageService? Service => Host.Services.GetService<PackageService>();
    private string id = "b019620d-22a8-4c46-0569-08de257668ed";
    [TestMethod]
    public async Task Add()
    {
        var item = new Package { Name = "Test2", Weight=120 };
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

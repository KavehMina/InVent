using InVent.Data.Models;
using InVent.Services.CustomsServices;
using Microsoft.Extensions.DependencyInjection;
namespace InVent.test.RepositoryTests;

[TestClass]
public class CustomsTest : BaseTest
{
    private CustomsService? Service => Host.Services.GetService<CustomsService>();
    [TestMethod]
    public async Task AddCustoms()
    {
        var item = new Customs { Name = "Test2", Number = 123 };
        var res = await this.Service.Add(item);

    }

    [TestMethod]
    public async Task GetAllCustoms()
    {
        var res = await this.Service.GetAll();

    }
    [TestMethod]
    public async Task GetById()
    {
        var res = await this.Service.GetById(new Guid("01f8b156-fa44-490f-d739-08de25738764"));

    }
    [TestMethod]
    public async Task Update()
    {
        var tar = (await this.Service.GetById(new Guid("01f8b156-fa44-490f-d739-08de25738764"))).Entities.FirstOrDefault();
        tar.Name = "Test-Edited";
        await this.Service.Update(tar);

    }

    [TestMethod]
    public async Task Delete()
    {
        var tar = (await this.Service.GetById(new Guid("01f8b156-fa44-490f-d739-08de25738764"))).Entities.FirstOrDefault();
        await this.Service.Delete(tar);

    }
}

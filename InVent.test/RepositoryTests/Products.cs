using InVent.Data.Models;
using InVent.Services.ProductServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Products : BaseTest
{
    private ProductService? Service => Host.Services.GetService<ProductService>();
    private string id = "e42c2424-4ae7-42b5-9c5a-08de25780cc7";
    [TestMethod]
    public async Task Add()
    {
        var item = new Product { Name = "Test2", Grade = "high" };
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

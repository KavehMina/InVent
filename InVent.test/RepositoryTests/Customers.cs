using InVent.Data.Models;
using InVent.Services.CustomerServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Customers : BaseTest
{

    private CustomerService? Service => Host.Services.GetService<CustomerService>();

    [TestMethod]
    public async Task AddCustomer()
    {
        var item = new Customer() { Name = "Test" };
        var res = await Service.Add(item);
    }
    [TestMethod]
    public async Task GetAllCustomer()
    {
        var res = await Service.GetAll();
    }
    [TestMethod]
    public async Task GetCustomer()
    {
        var t = (await Service.GetAll()).Entities?.FirstOrDefault();
        var res = await Service.GetById(t != null ? t.Id : Guid.Empty);
    }
    [TestMethod]
    public async Task EditCustomer()
    {
        try
        {
            var item = new Customer() { Name = "Test" };
            var response = (await Service.Add(item)).Entities.FirstOrDefault();
            response.Name = "Test-Edited";
            var res = await Service.Update(response);

        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
        }

    }
    [TestMethod]
    public async Task DeleteCustomer()
    {
        var item = new Customer() { Name = "Test" };
        try
        {
            var added = (await Service.Add(item)).Entities.FirstOrDefault();
            var res = await Service.Delete(added);
        }
        catch (Exception err)
        {
        }
    }
}

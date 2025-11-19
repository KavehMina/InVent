using InVent.Services.BankServices;
using Microsoft.Extensions.DependencyInjection;

namespace InVent.test.RepositoryTests;

[TestClass]
public class Banks : BaseTest
{
    private BankService? Service => Host.Services.GetService<BankService>();

    [TestMethod]
    public async Task TestMethod1()
    {
        var res = await Service.GetAllBanks();
    }
}

using System;
//using System.Security.Cryptography.X509Certificates;

using InVent.Data.Models;
using InVent.Extensions;
using InVent.Services;
using InVent.Services.TankerServices;
using Microsoft.Identity.Client;
using System.Reflection;

namespace InVent.test
{
    [TestClass]
    public sealed class Test1(TankerRepository tankerRepository)//IRepository<Tanker> repository, IServiceProvider serviceProvider,)
    {
        public required TankerRepository TankerRepository = tankerRepository;

        //public readonly IRepository<Tanker> Repository;
        //public readonly IServiceProvider ServiceProvider;
        public class TestModel
        {
            public int Id { get; set; }
            public string? Telephone { get; set; }
            public List<int>? Ints { get; set; }
            public string? Number { get; set; }
        }
        [TestMethod]
        public void URL()
        {
            const string a = "https://localhost:7235/"; //4
            const string b = "https://localhost:7235/Tankers";//4
            const string c = "https://localhost:7235/Tankers/AddNew";//5
            const string d = "https://localhost:7235/Tankers/Edit/a18ec6f6-1d1c-4d30-2ceb-08de1f73c88f";//6

            string[] urls = { a, b, c, d };
            List<string[]> splits = [];

            string Title = string.Empty;

            foreach (string url in urls)
            {
                var x = url.Split('/');
                if (x[3] == "Tankers")
                {
                    if (x.Length == 4)
                    {
                        Title = "لیست تانکرها";
                    }
                    else if (x.Length > 4)
                    {
                        switch (x[4])
                        {
                            case "AddNew":
                                Title = "اضافه کردن تانکر جدید";
                                break;
                            case "Edit":
                                Title = "ویرایش تانکر";
                                break;
                            default:
                                break;
                        }

                    }
                }
                else if (x[3] == "Customers")
                {
                    if (x.Length == 4)
                    {
                        Title = "لیست مشتری‌ها";
                    }
                    else if (x.Length > 4)
                    {
                        switch (x[4])
                        {
                            case "AddNew":
                                Title = "اضافه کردن مشتری جدید";
                                break;
                            case "Edit":
                                Title = "ویرایش مشتری";
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
        }

        [TestMethod]
        public void TankerToTankerView()
        {
            TankerViewModel model = new() { CargoType = "", DriverName = "", DriverPhone = "", Number = "" };
            //TestModel model = new TestModel();
            Tanker tanker = new() { Id = Guid.NewGuid(), CargoType = "RPO", DriverName = "Kaveh", DriverPhone = "09376318905", Number = "22222", DriverBankId = null };

            //foreach (var p in typeof(Tanker).GetProperties())
            //{
            //    typeof(TestModel).GetProperties()
            //        .Where(x => x.Name == p.Name && x.PropertyType.Name == p.PropertyType.Name)
            //        .FirstOrDefault()?
            //        .SetValue(model, p.GetValue(tanker));
            //}

            //typeof(Tanker).GetProperties()
            //    .ToList()
            //    .ForEach(p => typeof(TestModel).GetProperties()
            //        .Where(x => x.Name == p.Name && x.PropertyType.Name == p.PropertyType.Name)
            //        .FirstOrDefault()?
            //        .SetValue(model, p.GetValue(tanker)));



            //var modelProps = typeof(TestModel).GetProperties();
            //typeof(Tanker).GetProperties()
            //    .ToList()
            //    .ForEach(p =>
            //        modelProps
            //            .FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType)?
            //            .SetValue(model, p.GetValue(tanker))
            //    );



            PropertyMapperExtension<Tanker, TankerViewModel>.Map(tanker, model);
            //var t = Mapper.Map(tanker,model);

            Console.WriteLine("");
        }

        [TestMethod]
        public async Task GetAllTankerViewModels()
        {
            var res = await TankerRepository.GetAllWithBankNames();
            Console.WriteLine(res.Message);
        }
    }


}

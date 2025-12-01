using Microsoft.IdentityModel.Tokens;
using System;

namespace InVent.Extensions
{
    public static class PageTitleExtension
    {
        public static string GetPageTitle(string url)
        {
            return url.Split('/')[3] switch
            {
                "Tankers" => "تانکرها",
                "Banks" => "بانک‌ها",
                "Products" => "محصولات",
                "Carriers" => "شرکت‌های حمل ونقل",
                "Refineries" => "پالایشگاه‌ها",
                "Packages" => "بسته‌بندی‌ها",
                "Customers" => "مشتری‌ها",
                "Ports" => "گمرک‌های خروج",
                "Customs" => "گمرک‌های اظهار",
                "Projects" => "پروژه‌ها",
                "DeliveryOrders" => "حواله‌ها",
                "Entries" => "ورودی‌ها",
                "Bookings" => "بوکینگ‌ها",
                "Dispatches" => "خروجی‌ها",
                "Suppliers" => "تأمین‌کننده‌ها",
                "" => "خانه",
                _ => "InVent",
            };

        }
    }
}

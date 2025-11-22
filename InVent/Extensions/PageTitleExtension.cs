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
                "Ports" => "بندرها",
                "Customs" => "گمرک‌ها",
                "Projects" => "پروژه‌ها",
                "DeliveryOrders" => "حواله‌ها",
                "Entries" => "ورودی‌ها",
                "Bookings" => "بوکینگ‌ها",
                "Dispatches" => "خروجی‌ها",
                _ => "InVent",
            };



            //var x = url.Split('/');

            //if (x[3] == "Tankers")
            //{
            //    if (x.Length == 4)
            //    {
            //        return "لیست تانکرها";
            //    }
            //    else if (x.Length > 4)
            //    {
            //        switch (x[4])
            //        {
            //            case "AddNew":
            //                return "اضافه کردن تانکر جدید";
            //            case "Edit":
            //                return "ویرایش تانکر";
            //            default:
            //                break;
            //        }

            //    }
            //}
            //else if (x[3] == "Banks")
            //{
            //    if (x.Length == 4)
            //    {
            //        return "لیست بانک‌ها";
            //    }
            //    else if (x.Length > 4)
            //    {
            //        switch (x[4])
            //        {
            //            case "AddNew":
            //                return "اضافه کردن بانک جدید";
            //            case "Edit":
            //                return "ویرایش بانک";
            //            default:
            //                break;
            //        }

            //    }
            //}
            //else if (x[3] == "Carriers")
            //{
            //    if (x.Length == 4)
            //    {
            //        return "لیست شرکت‌های حمل‌ونقل";
            //    }
            //    else if (x.Length > 4)
            //    {
            //        switch (x[4])
            //        {
            //            case "AddNew":
            //                return "اضافه کردن بانک جدید";
            //            case "Edit":
            //                return "ویرایش بانک";
            //            default:
            //                break;
            //        }

            //    }
            //}
            //else if (x[3] == "Customers")
            //{
            //    if (x.Length == 4)
            //    {
            //        return "لیست مشتری‌ها";
            //    }
            //    else if (x.Length > 4)
            //    {
            //        switch (x[4])
            //        {
            //            case "AddNew":
            //                return "اضافه کردن مشتری جدید";
            //            case "Edit":
            //                return "ویرایش مشتری";
            //            default:
            //                break;
            //        }

            //    }
            //}
            //return "InVent";
        }
    }
}

using System;

namespace InVent.Extensions
{
    public static class PageTitleExtension
    {
        public static string GetPageTitle(string url)
        {
            var x = url.Split('/');
            if (x[3] == "Tankers")
            {
                if (x.Length == 4)
                {
                    return "لیست تانکرها";
                }
                else if (x.Length > 4)
                {
                    switch (x[4])
                    {
                        case "AddNew":
                            return "اضافه کردن تانکر جدید";
                        case "Edit":
                            return "ویرایش تانکر";
                        default:
                            break;
                    }

                }
            }
            else if (x[3] == "Customers")
            {
                if (x.Length == 4)
                {
                    return "لیست مشتری‌ها";
                }
                else if (x.Length > 4)
                {
                    switch (x[4])
                    {
                        case "AddNew":
                            return "اضافه کردن مشتری جدید";
                        case "Edit":
                            return "ویرایش مشتری";
                        default:
                            break;
                    }

                }
            }
            return "InVent";
        }
    }
}

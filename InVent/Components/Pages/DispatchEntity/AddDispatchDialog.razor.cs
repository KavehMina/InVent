using InVent.Data.Models;
using InVent.Services.AttachmentServices;
using InVent.Services.BookingServices;
using InVent.Services.CarrierServices;
using InVent.Services.CustomsServices;
using InVent.Services.PortServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Text.RegularExpressions;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class AddDispatchDialog
    {
        [Inject]
        public required BookingService BookingService { get; set; }
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        [Inject]
        public required CarrierService CarrierService { get; set; }
        [Inject]
        public required PortService PortService { get; set; }
        [Inject]
        public required CustomsService CustomsService { get; set; }

        private Booking Booking { get; set; }
        private Booking TempBooking { get; set; }
        private List<Booking> Bookings { get; set; } = [];
        private Carrier Carrier { get; set; }
        private Carrier TempCarrier { get; set; }
        private List<Carrier> Carriers { get; set; } = [];
        private Port Port { get; set; }
        private Port TempPort { get; set; }
        private List<Port> Ports { get; set; } = [];
        private Customs Customs { get; set; }
        private Customs TempCustoms { get; set; }
        private List<Customs> CustomsList { get; set; } = [];
        private int? ProjectNumber { get; set; }

        private string? DriverName { get; set; }
        private string? DriverNationalCode { get; set; }
        private string? DriverPhone { get; set; }
        //private string? NumberPlate { get; set; }
        private int? FullWeight { get; set; }
        private int? EmptyWeight { get; set; }
        private int? NetWeight { get; set; }
        private int? PackageCount { get; set; }
        private int? Fare { get; set; }
        private int? TempFare { get; set; }
        private bool IsExport { get; set; }
        private bool IsDischarged { get; set; }
        private bool IsPaid { get; set; }
        private bool TempIsExport { get; set; }
        private string? InternationalNumber1 { get; set; }
        private string? InternationalNumber2 { get; set; }
        private DateTime? Date { get; set; } = DateTime.UtcNow;
        private MudDatePicker _picker;
        private string ExportLabel => this.IsExport ? "حمل یک‌سره" : "حمل ترکیبی";

        public required string First { get; set; }
        public required string Second { get; set; } = "ع";
        public required string Third { get; set; }
        public required string Fourth { get; set; }


        /// <NOTE START>
        /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
        /// results in incorrect text order caused by the persian letter in the middle of a text.
        public string NumberPlate => Third + Fourth + Second + First;
        /// </NOTE END>

        protected override async void OnInitialized()
        {
            try
            {
                this.Bookings = (await BookingService.GetAll()).Entities ?? [];
                this.Carriers = (await CarrierService.GetAll()).Entities ?? [];
                this.Ports = (await PortService.GetAll()).Entities ?? [];
                this.CustomsList = (await CustomsService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }

            base.OnInitialized();
        }

        private async Task<IEnumerable<Booking>> SearchBookings(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Bookings.Where(x => x.Number.ToString().Contains(value)).ToList();
            }
            return Bookings;
        }
        private static string? BookingToString(Booking booking)
        {
            return booking?.Number.ToString();
        }

        private async Task<IEnumerable<Carrier>> SearchCarriers(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Carriers.Where(x => x.Name.Contains(value)).ToList();
            }
            return Carriers;
        }
        private static string? CarrierToString(Carrier carrier)
        {
            return carrier?.Name;
        }

        private async Task<IEnumerable<Port>> SearchPorts(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Ports.Where(x => x.Name.Contains(value)).ToList();
            }
            return Ports;
        }
        private static string? PortToString(Port port)
        {
            return port?.Name;
        }

        private async Task<IEnumerable<Customs>> SearchCustoms(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return CustomsList.Where(x => x.Name.Contains(value)).ToList();
            }
            return CustomsList;
        }
        private static string? CustomsToString(Customs customs)
        {
            return customs?.Name;
        }


        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await Submit();
        }

        private void SetFullWeight(int? value)
        {
            this.FullWeight = value;
            this.SetNetWeight();
        }
        private void SetEmptyWeight(int? value)
        {
            this.EmptyWeight = value;
            this.SetNetWeight();
        }

        private void SetNetWeight()
        {
            if (this.FullWeight != null && this.EmptyWeight != null)
                this.NetWeight = this.FullWeight - this.EmptyWeight;
            else
                this.NetWeight = null;
        }



        private readonly IList<IBrowserFile> files = [];



        private List<Attachment> Attachments = [];



        //private async Task _PrepareAttachments(Guid parentId)
        //{
        //    this.ProjectNumber = this.Booking.Project?.Number;
        //    this.Attachments = [];
        //    foreach (var file in this.files)
        //    {
        //        var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}");
        //        Directory.CreateDirectory(folder);

        //        var filePath = Path.Combine(folder, file.Name);
        //        using var stream = File.Create(filePath);
        //        await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
        //            .CopyToAsync(stream);

        //        //using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB limit
        //        //using var ms = new MemoryStream();
        //        //await stream.CopyToAsync(ms);
        //        this.Attachments.Add(new Attachment
        //        {
        //            ParentId = parentId,
        //            ParentType = "dispatch",
        //            FileName = file.Name,
        //            ContentType = file.ContentType,
        //            FileSize = file.Size,
        //            FilePath = $"/Attachments/Project-{this.ProjectNumber}/{file.Name}",
        //            Category = "file.Category"
        //            //FileData = ms.ToArray()
        //        });
        //    }
        //}

        private async Task PrepareAttachments(Guid parentId)
        {
            this.ProjectNumber = this.Booking.Project?.Number;
            foreach (var file in this.files)
            {
                var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType && x.LastModified == file.LastModified)
                    .FirstOrDefault();

                var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}/{att?.Category}");
                Directory.CreateDirectory(folder);

                var ext = file.Name.Split('.');
                var fileName = this.NumberPlate + "-" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "." + ext.LastOrDefault(); ;
                var filePath = Path.Combine(folder, fileName);
                using var stream = File.Create(filePath);
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
                    .CopyToAsync(stream);

                //var t = this.Attachments.Where(x => x.Equals(file));

                if (att != null)
                {
                    att.ParentId = parentId;
                    att.ParentType = "dispatch";
                    att.FilePath = $"/Attachments/Project-{this.ProjectNumber}/{att?.Category}/{fileName}";
                    att.FileName = fileName;

                }


                //this.Attachments.Add(new Attachment
                //{
                //    ParentId = parentId,
                //    ParentType = "dispatch",
                //    FileName = file.Name,
                //    ContentType = file.ContentType,
                //    FileSize = file.Size,
                //    FilePath = $"/Attachments/Project-{this.ProjectNumber}/{file.Name}",
                //    Category = "file.Category"
                //});
            }
        }

        private async Task Submit()
        {
            await form.Validate();
            await BeginLoadingProcess();
            if (form.IsValid)
            {
                try
                {
                    var tempDispatch = new DispatchDTO()
                    {
                        BookingId = this.Booking.Id,
                        CarrierId = this.Carrier.Id,
                        CustomsId = this.Customs.Id,
                        PortId = this.Port.Id,
                        DriverName = this.DriverName,
                        DriverNationalCode = this.DriverNationalCode,
                        DriverPhone = this.DriverPhone,
                        EmptyWeight = (int)this.EmptyWeight,
                        FullWeight = (int)this.FullWeight,
                        Fare = (int)this.Fare,
                        IsExport = this.IsExport,
                        IsDischarged = this.IsDischarged,
                        IsPaid = this.IsPaid,
                        NumberPlate = this.NumberPlate,
                        PackageCount = (int)this.PackageCount,
                        Date = this.Date,
                        InternationalNumber1 = this.InternationalNumber1,
                        InternationalNumber2 = this.InternationalNumber2,
                    };
                    var res = await DispatchService.Add(tempDispatch);
                    if (res.Success && res.Entities != null)
                    {
                        await this.PrepareAttachments(res.Entities.FirstOrDefault().Id);
                        foreach (var att in this.Attachments)
                        {
                            var attRes = await this.AttachmentService.Add(att);
                            this.HandleMessage(att.FileName, attRes.Success);
                        }

                        this.TempCustoms = this.Customs;
                        this.TempPort = this.Port;
                        this.TempIsExport = this.IsExport;
                        this.TempBooking = this.Booking;
                        this.TempCarrier = this.Carrier;
                        this.TempFare = this.Fare;
                        await form.ResetAsync();
                        this.IsExport = this.TempIsExport;
                        this.Port = this.TempPort;
                        this.Customs = this.TempCustoms;
                        this.Carrier = this.TempCarrier;
                        this.Fare = this.TempFare;
                        this.Booking = this.TempBooking;
                        this.Date = DateTime.Today;
                        this.files.Clear();
                        this.Attachments.Clear();
                        this.StateHasChanged();
                        await EndLoadingProcess();

                    }
                    this.HandleMessage(res.Message, res.Success);

                    //MudDialog?.Close(DialogResult.Ok(true));

                }
                catch (Exception err)
                {
                    HandleMessage(err.Message, false);
                }
            }
            else
            {
                HandleMessage("invalid form", false);
            }

        }
        private void SetToday()
        {
            this.Date = DateTime.Today;
            this._picker?.CloseAsync();
        }
        private async Task MoveFocus(string value, MudTextField<string> thisField, MudTextField<string> nextField)
        {

            if (thisField != null && nextField != null)
            {
                switch (thisField.InputId)
                {
                    case "1":
                        First = value;
                        if (thisField?.Value?.Length == 2)
                            await nextField.FocusAsync();
                        break;
                    case "2":
                        Second = value;
                        if (thisField?.Value?.Length == 1)
                            await nextField.FocusAsync();
                        break;
                    case "3":
                        Third = value;
                        if (thisField?.Value?.Length == 3)
                            await nextField.FocusAsync();
                        break;
                    case "4":
                        //Fourth = value;
                        //if (thisField?.Value?.Length == 2)
                        //    await nextField.FocusAsync();
                        break;
                    case "6":
                        DriverPhone = value;
                        if (thisField?.Value?.Length == 11)
                            await nextField.FocusAsync();
                        break;
                    default:
                        break;
                }
            }

        }
        private async Task FillDriverInfo(string value)
        {
            this.Fourth = value;
            if (value.Length == 2)
            {
                var res = await this.DispatchService.GetDriverInfoByNumberPlate(this.NumberPlate);
                if (res.Entities != null)
                {
                    this.DriverName = res.Entities.FirstOrDefault()?.DriverName;
                    this.DriverNationalCode = res.Entities.FirstOrDefault()?.DriverNationalCode;
                    this.DriverPhone = res.Entities.FirstOrDefault()?.DriverPhone;
                }
            }
        }

        private List<MudTextField<string>> TextFieldRefs = new(new MudTextField<string>[12]);

        public PatternMask Mask1 = new("00");
        public PatternMask Mask2 = new("a");
        public PatternMask Mask3 = new("000");
        public PatternMask MobileMask = new("00000000000");

        private string ValidateMobilePhone(string arg)
        {
            if (arg != null && arg != string.Empty)
            {
                Regex regex = new Regex("09\\d\\d\\d\\d\\d\\d\\d\\d\\d", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(arg))
                    return "موبایل نامعتبر";
            }
            return string.Empty;
        }
        private string ValidateFirstPartofNumberPlate(string arg)
        {
            if (arg?.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateSecondPartofNumberPlate(string arg)
        {
            if (arg?.Length < 1)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateThirdPartofNumberPlate(string arg)
        {
            if (arg?.Length < 3)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateForthPartofNumberPlate(string arg)
        {
            if (arg?.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }
    }
}

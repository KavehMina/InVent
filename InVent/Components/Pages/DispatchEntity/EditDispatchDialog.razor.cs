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
    public partial class EditDispatchDialog
    {
        [Inject]
        public required BookingService BookingService { get; set; }
        [Inject]
        public required CarrierService CarrierService { get; set; }
        [Inject]
        public required PortService PortService { get; set; }
        [Inject]
        public required CustomsService CustomsService { get; set; }
        [Inject]
        public required AttachmentService AttachmentService { get; set; }

        private Booking Booking { get; set; }
        private List<Booking> Bookings { get; set; } = [];
        private Carrier Carrier { get; set; }
        private List<Carrier> Carriers { get; set; } = [];
        private Port Port { get; set; }
        private List<Port> Ports { get; set; } = [];
        private Customs Customs { get; set; }
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
        private bool IsExport { get; set; }
        private bool IsDischarged { get; set; }
        private bool IsPaid { get; set; }
        private string? InternationalNumber1 { get; set; }
        private string? InternationalNumber2 { get; set; }
        private DateTime? Date { get; set; }
        private MudDatePicker _picker;
        private string ExportLabel => this.IsExport ? "صادراتی" : "داخلی";

        private List<Attachment> ExistingAttachments { get; set; } = [];

        public required string First { get; set; }
        public required string Second { get; set; } = "ع";
        public required string Third { get; set; }
        public required string Fourth { get; set; }


        /// <NOTE START>
        /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
        /// results in incorrect text order caused by the persian letter in the middle of a text.
        public string NumberPlate => Third + Fourth + Second + First;
        /// </NOTE END>

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.Bookings = (await BookingService.GetAll()).Entities ?? [];
                this.Carriers = (await CarrierService.GetAll()).Entities ?? [];
                this.Ports = (await PortService.GetAll()).Entities ?? [];
                this.CustomsList = (await CustomsService.GetAll()).Entities ?? [];
                this.ExistingAttachments = (await AttachmentService.GetAll(this.Dispatch.Id, "dispatch")).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            this.ProjectNumber = this.Bookings.Where(x => x.Id == this.Dispatch.Booking?.Id).FirstOrDefault()?.Project?.Number;
            this.Booking = this.Dispatch.Booking;
            this.Carrier = this.Dispatch.Carrier;
            this.Port = this.Dispatch.Port;
            this.Customs = this.Dispatch.Customs;
            this.DriverName = this.Dispatch.DriverName;
            this.DriverPhone = this.Dispatch.DriverPhone;
            this.DriverNationalCode = this.Dispatch.DriverNationalCode;
            //this.NumberPlate = this.Dispatch.NumberPlate;
            this.FullWeight = this.Dispatch.FullWeight;
            this.EmptyWeight = this.Dispatch.EmptyWeight;
            this.NetWeight = this.FullWeight - this.EmptyWeight;
            this.PackageCount = this.Dispatch.PackageCount;
            this.Fare = this.Dispatch.Fare;
            this.IsExport = this.Dispatch.IsExport;
            this.InternationalNumber1 = this.Dispatch.InternationalNumber1;
            this.InternationalNumber2 = this.Dispatch.InternationalNumber2;
            this.Date = this.Dispatch.Date;
            this.SplitNumberPlate();
            return base.OnParametersSetAsync();
        }

        private void SplitNumberPlate()
        {
            /// <NOTE START>
            /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
            /// results in incorrect text order caused by the persian letter in the middle of a text.
            Third = this.Dispatch.NumberPlate?.Substring(0, 3) ?? "";
            Fourth = this.Dispatch.NumberPlate?.Substring(3, 2) ?? "";
            Second = this.Dispatch.NumberPlate?.Substring(5, 1) ?? "";
            First = this.Dispatch.NumberPlate?.Substring(6, 2) ?? "";
            /// </NOTE END>
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
        

        private async Task PrepareAttachments()
        {
            foreach (var file in this.files)
            {
                var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType && x.LastModified == file.LastModified)
                   .FirstOrDefault();
                var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}/{att?.Category}");
                Directory.CreateDirectory(folder);

                var ext = file.Name.Split('.');
                var fileName = this.NumberPlate + "-" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "." + ext.LastOrDefault();
                var filePath = Path.Combine(folder, fileName);

                using var stream = File.Create(filePath);
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
                    .CopyToAsync(stream);


                if (att != null)
                {
                    att.ParentId = this.Dispatch.Id;
                    att.ParentType = "dispatch";
                    att.FilePath = $"/Attachments/Project-{this.ProjectNumber}/{att?.Category}/{fileName}";
                    att.FileName = fileName;
                }
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
                        Id = this.Dispatch.Id,
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
                        LastModifiedOn = DateTime.UtcNow,
                        InternationalNumber1 = this.InternationalNumber1,
                        InternationalNumber2 = this.InternationalNumber2,
                    };
                    var res = await DispatchService.Update(tempDispatch);
                    if (res.Success && res.Entities != null)
                    {
                        await this.PrepareAttachments();
                        foreach (var att in Attachments)
                        {
                            var attRes = await this.AttachmentService.Add(att);
                            this.HandleMessage(att.FileName, attRes.Success);
                        }
                    }
                    this.HandleMessage(res.Message, res.Success);

                    MudDialog?.Close(DialogResult.Ok(true));

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
            await EndLoadingProcess();
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

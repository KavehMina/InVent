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
        private string? NumberPlate { get; set; }
        private int? FullWeight { get; set; }
        private int? EmptyWeight { get; set; }
        private int? NetWeight { get; set; }
        private int? PackageCount { get; set; }
        private int? Fare { get; set; }
        private bool IsExport { get; set; }
        private string? InternationalNumber1 { get; set; }
        private string? InternationalNumber2 { get; set; }
        private DateTime? Date { get; set; }
        private MudDatePicker _picker;
        private string ExportLabel => this.IsExport ? "صادراتی" : "داخلی";

        private List<Attachment> ExistingAttachments { get; set; } = [];

        //protected override async void OnInitialized()
        //{
        //    try
        //    {
        //        this.Bookings = (await BookingService.GetAll()).Entities ?? [];
        //        this.Carriers = (await CarrierService.GetAll()).Entities ?? [];
        //        this.Ports = (await PortService.GetAll()).Entities ?? [];
        //        this.CustomsList = (await CustomsService.GetAll()).Entities ?? [];
        //        this.ExistingAttachments = (await AttachmentService.GetAll(this.Dispatch.Id, "dispatch")).Entities ?? [];
        //    }
        //    catch (Exception err)
        //    {
        //        HandleMessage(err.Message, false);
        //    }

        //    base.OnInitialized();
        //}

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
            this.NumberPlate = this.Dispatch.NumberPlate;
            this.FullWeight = this.Dispatch.FullWeight;
            this.EmptyWeight = this.Dispatch.EmptyWeight;
            this.NetWeight = this.FullWeight - this.EmptyWeight;
            this.PackageCount = this.Dispatch.PackageCount;
            this.Fare = this.Dispatch.Fare;
            this.IsExport = this.Dispatch.IsExport;
            this.InternationalNumber1 = this.Dispatch.InternationalNumber1;
            this.InternationalNumber2 = this.Dispatch.InternationalNumber2;
            this.Date = this.Dispatch.Date;
            return base.OnParametersSetAsync();
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

        //[Inject]
        //IDialogService DialogService { get; set; }

        //private async Task DeleteAttachments(Guid id,string filePath)
        //{
        //    var options = new DialogOptions { CloseOnEscapeKey = true };
        //    var parameters = new DialogParameters
        //    {
        //        { "DispatchId", id },
        //        { "FilePath", filePath },
        //        { "Header" , "حذف ضمیمه" },
        //        { "Message" , "آیا از حذف این ضمیمه اطمینان دارید؟" }
        //    };

        //    var dialog = await DialogService.ShowAsync<DeleteDispatchAttachmentDialog>("", parameters, options);
        //    var result = await dialog.Result;
        //    if (result != null && !result.Canceled)
        //    {
        //        this.ExistingAttachments = (await AttachmentService.GetAll(this.Dispatch.Id, "dispatch")).Entities ?? [];
        //    }
        //}

        //private async Task ViewAttachments(Attachment attachment)
        //{
        //    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
        //    var parameters = new DialogParameters
        //    {
        //        { "Attachment", attachment },
        //        { "Header" , attachment.FileName }
        //    };

        //    /*var dialog =*/
        //    await DialogService.ShowAsync<ViewDispatchAttachmentDialog>("", parameters, options);
        //    //var result = await dialog.Result;
        //    //if (result != null && !result.Canceled)
        //    //{
        //    //}
        //}


        private readonly IList<IBrowserFile> files = [];

        private List<Attachment> Attachments = [];

        private async Task UploadFiles(IReadOnlyList<IBrowserFile> files)
        {
            foreach (var file in files ?? [])
            {
                this.files.Add(file);
            }

        }

        private void RemoveFile(IBrowserFile file)
        {
            this.files.Remove(file);
            this.StateHasChanged();
        }

        private async Task _PrepareAttachments()
        {
            this.Attachments = [];
            foreach (var file in this.files)
            {
                var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}");
                Directory.CreateDirectory(folder);

                var filePath = Path.Combine(folder, file.Name);

                using var stream = File.Create(filePath);
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
                    .CopyToAsync(stream);
                //using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB limit
                //using var ms = new MemoryStream();
                //await stream.CopyToAsync(ms);
                this.Attachments.Add(new Attachment
                {
                    ParentId = this.Dispatch.Id,
                    ParentType = "dispatch",
                    FileName = file.Name,
                    ContentType = file.ContentType,
                    FileSize = file.Size,
                    FilePath = $"/Attachments/Project-{this.ProjectNumber}/{file.Name}",
                    Category ="file.Category"
                    //FileData = ms.ToArray()
                });
            }
        }

        private async Task PrepareAttachments()
        {
            foreach (var file in this.files)
            {
                var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}");
                Directory.CreateDirectory(folder);

                var filePath = Path.Combine(folder, file.Name);

                using var stream = File.Create(filePath);
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
                    .CopyToAsync(stream);

                var t = this.Attachments.Where(x => x.Equals(file));

                var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType)
                    .FirstOrDefault();
                if (att != null)
                {
                    att.ParentId = this.Dispatch.Id;
                    att.ParentType = "dispatch";
                    att.FilePath = $"/Attachments/Project-{this.ProjectNumber}/{file.Name}";

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
                        NumberPlate = this.NumberPlate,
                        PackageCount = (int)this.PackageCount,
                        Date = this.Date,
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
    }
}

using InVent.Data.Models;
using InVent.Extensions;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class Dispatches
    {

        private TableGroupDefinition<Dispatch> _groupDefinition = new()
        {
            GroupName = "پروژه",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (e) => e.Booking?.Project?.Number,
            InnerGroup = new TableGroupDefinition<Dispatch>()
            {
                GroupName = "بوکینگ",
                Indentation = false,
                IsInitiallyExpanded = false,
                Expandable = true,
                Selector = (e) => e.Booking?.Number
            }

        };

        private async Task SetIsDischarged(Dispatch dispatch, bool value)
        {
            await BeginLoadingProcess();
            try
            {
                var tempDispatch = new DispatchDTO()
                {
                    Id = dispatch.Id,
                    BookingId = dispatch.BookingId,
                    CarrierId = dispatch.CarrierId,
                    CustomsId = dispatch.CustomsId,
                    PortId = dispatch.PortId,
                    DriverName = dispatch.DriverName,
                    DriverNationalCode = dispatch.DriverNationalCode,
                    DriverPhone = dispatch.DriverPhone,
                    EmptyWeight = dispatch.EmptyWeight,
                    FullWeight = dispatch.FullWeight,
                    Fare = dispatch.Fare,
                    IsExport = dispatch.IsExport,
                    IsDischarged = value,                   //value set
                    IsPaid = dispatch.IsPaid,
                    NumberPlate = dispatch.NumberPlate,
                    PackageCount = dispatch.PackageCount,
                    Date = dispatch.Date,
                    LastModifiedOn = DateTime.UtcNow,
                    InternationalNumber1 = dispatch.InternationalNumber1,
                    InternationalNumber2 = dispatch.InternationalNumber2,
                };
                var res = await this.DispatchService.Update(tempDispatch);
                this.HandleMessage(res.Message, res.Success);
                await RefreshList();
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }
            await EndLoadingProcess();
        }

        private async Task SetIsPaid(Dispatch dispatch, bool value)
        {
            await BeginLoadingProcess();
            try
            {
                var tempDispatch = new DispatchDTO()
                {
                    Id = dispatch.Id,
                    BookingId = dispatch.BookingId,
                    CarrierId = dispatch.CarrierId,
                    CustomsId = dispatch.CustomsId,
                    PortId = dispatch.PortId,
                    DriverName = dispatch.DriverName,
                    DriverNationalCode = dispatch.DriverNationalCode,
                    DriverPhone = dispatch.DriverPhone,
                    EmptyWeight = dispatch.EmptyWeight,
                    FullWeight = dispatch.FullWeight,
                    Fare = dispatch.Fare,
                    IsExport = dispatch.IsExport,
                    IsDischarged = dispatch.IsDischarged,
                    IsPaid = value,                             //value set
                    NumberPlate = dispatch.NumberPlate,
                    PackageCount = dispatch.PackageCount,
                    Date = dispatch.Date,
                    LastModifiedOn = DateTime.UtcNow,
                    InternationalNumber1 = dispatch.InternationalNumber1,
                    InternationalNumber2 = dispatch.InternationalNumber2,
                };
                var res = await this.DispatchService.Update(tempDispatch);
                this.HandleMessage(res.Message, res.Success);
                await RefreshList();
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }
            await EndLoadingProcess();
        }




        public List<Dispatch> DispatchesList { get; set; } = [];
        public required MudTable<Dispatch> Table { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshList();
            await base.OnInitializedAsync();
        }

        private async Task RefreshList()
        {
            await BeginLoadingProcess();
            try
            {
                var res = await DispatchService.GetAll();
                if (res.Success)
                    DispatchesList = res.Entities ?? [];
                else
                    HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await EndLoadingProcess();
        }

        public async Task OpenAddDialog(MouseEventArgs e)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Header" , "خروجی جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddDispatchDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Dispatch> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Dispatch", e.Item },
                { "Header" , "خروجی" }
            };

            await DialogService.ShowAsync<ViewDispatchDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Dispatch item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Dispatch", item },
                { "Header" , "ویرایش خروجی" }
            };

            var dialog = await DialogService.ShowAsync<EditDispatchDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Dispatch item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Dispatch", item },
                { "Header" , "حذف خروجی" },
                { "Message" , "آیا از حذف این خروجی اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteDispatchDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}

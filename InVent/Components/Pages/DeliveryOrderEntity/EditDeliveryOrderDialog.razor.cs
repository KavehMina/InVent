using InVent.Data.Models;
using InVent.Services.ProjectServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.DeliveryOrderEntity
{
    public partial class EditDeliveryOrderDialog
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }
        private Project Project { get; set; }
        private List<Project> Projects { get; set; } = [];
        private string StatusText => this.DeliveryOrder?.Status == true ? "بسته" : "باز";

        protected override async void OnInitialized()
        {
            try
            {
                this.Projects = (await this.ProjectService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }

            base.OnInitialized();
        }
        private async Task<IEnumerable<Project>> SearchProjects(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.Projects.Where(x => x.Number.ToString().Contains(value)).ToList();
            }
            return Projects;
        }
        private static string? ProjectToString(Project project)
        {
            return project?.Number.ToString();
        }

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await Submit();
        }
        private async Task Submit()
        {
            await BeginLoadingProcess();
            await form.Validate();
            if (form.IsTouched && form.IsValid)
            {
                try
                {
                    var tempDeliveryOrder = new DeliveryOrderDTO()
                    {
                        Id = this.DeliveryOrder.Id,
                        DeliveryOrderId = this.DeliveryOrder.DeliveryOrderId,
                        ProjectId = this.DeliveryOrder.Project.Id,
                        Weight = (int)this.DeliveryOrder.Weight,
                        TankerFare = (int)this.DeliveryOrder.TankerFare,
                        Status = this.DeliveryOrder.Status,

                    };
                    var res = await this.DeliveryOrderService.Update(tempDeliveryOrder);
                    this.HandleMessage(res.Message, res.Success);
                }
                catch (Exception err)
                {
                    HandleMessage(err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            await EndLoadingProcess();
        }
    }
}

using InVent.Data.Models;
using InVent.Services.ProjectServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.DeliveryOrderEntity
{
    public partial class AddDeliveryOrderDialog
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }

        private string? Number { get; set; }
        private int? Weight { get; set; }
        private int? TankerFare { get; set; }
        private Project Project { get; set; }
        private List<Project> Projects { get; set; } = [];
        private bool Status { get; set; }
        private string StatusText => Status == true ? "بسته" : "باز";

        protected override async void OnInitialized()
        {
            try
            {
                this.Projects = (await this.ProjectService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
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
            await form.Validate();
            await BeginLoadingProcess();
            if (form.IsValid)
            {
                try
                {
                    var tempDeliveryOrder = new DeliveryOrderDTO()
                    {
                        DeliveryOrderId = this.Number,
                        ProjectId = this.Project.Id,
                        Weight = (int)this.Weight,
                        TankerFare = (int)this.TankerFare,
                        Status = this.Status,
                        IsDriverPaid = false

                    };
                    var res = await DeliveryOrderService.Add(tempDeliveryOrder);
                    if (res.Success)
                        await form.ResetAsync();
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
    }
}

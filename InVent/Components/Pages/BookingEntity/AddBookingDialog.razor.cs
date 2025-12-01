using InVent.Data.Models;
using InVent.Services.ProjectServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.BookingEntity
{
    public partial class AddBookingDialog
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }

        private Project Project { get; set; }
        private List<Project> Projects { get; set; } = [];
        private int? Number { get; set; }
        private string ContainerType { get; set; } = string.Empty;
        private string? Destination { get; set; }
        private string? ShippingLine { get; set; }
        private string? Forwarder { get; set; }
        private int? ContainerCount { get; set; }
        private int? PackingCount { get; set; }
        private string? Product {  get; set; }
        private string? Customer { get; set; }
        private int? Remaining { get; set; }


        protected override async void OnInitialized()
        {
            try
            {
                this.Projects = (await ProjectService.GetAll()).Entities ?? [];
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
                return Projects.Where(x => x.Number.ToString().Contains(value)).ToList();
            }
            return Projects;
        }
        private static string? ProjectToString(Project project)
        {
            return project?.Number.ToString();
        }

        private async Task SetProject(Project e)
        {
            this.Project = e;
            this.Product = this.Project?.Product?.Name;
            this.Customer = this.Project?.Customer?.Name;
            this.ContainerType = this.Project?.Package?.Name ?? string.Empty;
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
                    var tempBooking = new BookingDTO()
                    {
                        Number = (int)this.Number,
                        ContainerType = this.ContainerType,
                        ContainerCount = (int)this.ContainerCount,
                        ProjectId = Project.Id,
                        PackingCount = (int)this.PackingCount,
                        Destination = this.Destination,
                        ShippingLine = this.ShippingLine,
                        Forwarder = this.Forwarder,
                    };
                    var res = await BookingService.Add(tempBooking);
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

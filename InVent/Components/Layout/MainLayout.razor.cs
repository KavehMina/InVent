using MudBlazor;

namespace InVent.Components.Layout
{
    public partial class MainLayout
    {
        private static readonly string[] Fonts = new[] { "Sahel", "Poppins", "Helvetica", "Arial", "sans-serif" };
        private static readonly string Normal = "400";
        private static readonly string Bold = "600";
        private static readonly string Light = "200";
        private readonly MudTheme InVentTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.Blue.Default,
                Secondary = Colors.Green.Accent4,
                AppbarBackground = Colors.Red.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten1
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            },            

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = Fonts,
                    FontWeight = Normal
                },

                Body1 = new Body2Typography()
                {
                    FontWeight = Normal,
                },

                //FontsBold

                Body2 = new Body2Typography()
                {
                    FontWeight = Bold,
                },

                Button = new ButtonTypography()
                {
                    FontWeight = Bold
                },

                H1 = new H1Typography()
                {
                    FontWeight = Bold
                },

                H2 = new H2Typography()
                {
                    FontWeight = Bold
                },

                H3 = new H3Typography()
                {
                    FontWeight = Bold
                },

                H4 = new H4Typography()
                {
                    FontWeight = Bold
                },

                H5 = new H5Typography()
                {
                    FontWeight = Bold
                },

                H6 = new H6Typography()
                {
                    FontWeight = Bold
                },
                

                ////FontsLight

                

                Caption = new CaptionTypography()
                {
                    FontWeight = Light
                },

                Subtitle1 = new Subtitle1Typography()
                {
                    FontWeight = Light
                }

            }

        };
    }
}

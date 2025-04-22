using Finora.Pages;
using Finora.Services;
namespace Finora
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DatabaseHelper.Init();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(
                new NavigationPage(new Login())
                {
                    BarBackgroundColor = Colors.White,
                    BarTextColor = Colors.Black
                }
            );
        }
    }
}

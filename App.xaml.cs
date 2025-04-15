
namespace Finora
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
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

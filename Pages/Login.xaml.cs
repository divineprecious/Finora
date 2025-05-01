using Finora.Services;

namespace Finora.Pages
{
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
		}

		private async void OnLoginClicked(object sender, EventArgs e)
		{
			string email = userEmail.Text;
			string password = userPassword.Text;

			var user = await DatabaseHelper.Authenticate(email, password);
			
			if (user != null)
			{
				//Navigate to Shell-based app
                Session.CurrentUser = user;
                Application.Current.MainPage = new AppShell();
				
			}
			else
			{
				await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
			}
		}

		private void OnCreateClicked(object sender, EventArgs e)
		{
			Application.Current.MainPage = new Signup();
		}
	}
}
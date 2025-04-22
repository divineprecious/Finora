using Finora.Models;
using Finora.Services;
namespace Finora.Pages
{
	public partial class Signup : ContentPage
	{
		public Signup()
		{
			InitializeComponent();
		}
		private async void OnRegisterClicked(object sender, EventArgs e) 
		{
			var newUser = new User
			{
				Name = registerName.Text,
				Email = registerEmail.Text,
				Password = registerPassword.Text,
			};
			var result = await DatabaseHelper.AddUser(newUser);

			if (result > 0) 
			{
                await DisplayAlert("Success", "Account created!", "OK");
                Application.Current.MainPage = new Login();
            }
		}
	}
}
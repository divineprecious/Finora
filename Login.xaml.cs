namespace Finora;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e) 
	{
		string email = userEmail.Text;
		string password = userPassword.Text;

		//Basic hardcoded example
		if (email == "user@example.com" &&  password == "password123")
		{
			//Navigate to Shell-based app
			Application.Current.MainPage = new AppShell();
		}
		else 
		{
			DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
		}
	}

	private void OnCreateClicked(object sender, EventArgs e) 
	{
		Application.Current.MainPage = new Signup();
	}
}
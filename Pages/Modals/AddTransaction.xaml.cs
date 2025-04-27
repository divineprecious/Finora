using Finora.Models;
using Finora.Services;
namespace Finora.Pages.Modals;

public partial class AddTransaction : ContentPage
{
	public AddTransaction()
	{
		InitializeComponent();
	}

    private async void OnAddClicked(object sender, EventArgs e) 
	{
		var transaction = new Transaction
		{
			UserId = Session.CurrentUser.Id,
			Amount = decimal.Parse(transAmount.Text),
            Type = transType.SelectedItem as string,
            Category = transCategory.Text,
			Date = transDate.Date
		};

		await DatabaseHelper.AddTransaction(transaction);
		await Navigation.PopModalAsync();
	}
}
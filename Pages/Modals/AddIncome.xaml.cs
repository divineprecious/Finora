using Finora.Models;
using Finora.Services;
namespace Finora.Pages.Modals;

public partial class AddIncome : ContentPage
{
	public AddIncome()
	{
		InitializeComponent();
	}

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var income = new Transaction
        {
            UserId = Session.CurrentUser.Id,
            Amount = decimal.Parse(incAmount.Text),
            Type = "Income",
            Category = incCategory.Text,
            Date = incDate.Date
        };

        await DatabaseHelper.AddTransaction(income);
        await Navigation.PopModalAsync();
    }
}
using Finora.Models;
using Finora.Services;

namespace Finora.Pages.Modals;

public partial class AddExpense : ContentPage
{
	public AddExpense()
	{
		InitializeComponent();
	}

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var expense = new Transaction
        {
            UserId = Session.CurrentUser.Id,
            Amount = decimal.Parse(expAmount.Text),
            Type = "Expense",
            Category = expCategory.Text,
            Date = expDate.Date
        };

        await DatabaseHelper.AddTransaction(expense);
        await Navigation.PopModalAsync();
    }
}
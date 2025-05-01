using Finora.Models;
using Finora.Services;

namespace Finora.Pages.Modals;

public partial class UpdateSavings : ContentPage
{
	public UpdateSavings()
	{
		InitializeComponent();
	}

    private async void OnAddClicked(object sender, EventArgs e)
    {
        int month = monthSelected.SelectedIndex + 1;
        decimal SavedAmount = Convert.ToDecimal(savedAmount.Text);


        int res = await DatabaseHelper.UpdateSavedAmountAsync(Session.CurrentUser.Id, month, SavedAmount);
        
        if (res == 0)    
        {
            await DisplayAlert("Error", "No Savings Record Found","OK");
        }
        
        await Navigation.PopModalAsync();
    }
}
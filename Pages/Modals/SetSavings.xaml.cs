using Finora.Models;
using Finora.Services;

namespace Finora.Pages.Modals;

public partial class SetSavings : ContentPage
{
    public SetSavings()
    {
        InitializeComponent();
    }

    private async void OnSetClicked(object sender, EventArgs e)
    {
        int month = monthSelected.SelectedIndex + 1;
        decimal GoalAmount = Convert.ToDecimal(goalAmount.Text);


        int res = await DatabaseHelper.UpdateGoalAmountAsync(Session.CurrentUser.Id, month, GoalAmount);

        if (res == 0)
        {
            var save = new Saving
            {
                UserId = Session.CurrentUser.Id,
                GoalAmount = GoalAmount,
                SavedAmount = 0,
                Month = month
            };

            DatabaseHelper.AddSavings(save);

            await Navigation.PopModalAsync();
        }
    }
}
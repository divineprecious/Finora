using Finora.Services;
using Finora.Models;
namespace Finora.Pages
{
	public partial class Savings : ContentPage
	{
		public Savings()
		{
			InitializeComponent();
            monthPicker.SelectedIndex = DateTime.Now.Month - 1;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            int selectedMonth = monthPicker.SelectedIndex + 1;

            if (selectedMonth == 0)
            {
                await LoadDataforMonth(DateTime.Now.Month);
            }
            else
            {
                await LoadDataforMonth(selectedMonth);
            }

        }

        private async Task LoadDataforMonth(int month)
        {
            if (Session.CurrentUser == null)
            {
                return;
            }

            var savings = await DatabaseHelper.GetSavingsForUser(Session.CurrentUser.Id, month);
            decimal progress = 0;

            if (savings == null) 
            {
                decimal GoalAmount = 0;
                decimal AmountSaved = 0;
            }
            else
            {
                decimal GoalAmount = savings.GoalAmount;
                decimal AmountSaved = savings.SavedAmount;
                progress = AmountSaved / GoalAmount;
            }

            SavingProgress.Progress = decimal.ToDouble(progress);
        }
        private async void OnMonthChanged(object sender, EventArgs e) 
		{
            if (monthPicker.SelectedIndex >= 0)
            {
                int selectedMonth = monthPicker.SelectedIndex + 1;
                await LoadDataforMonth(selectedMonth);
            }
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Modals.UpdateSavings());
        }

        private async void OnGoalClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Modals.SetSavings());
        }
    }
}
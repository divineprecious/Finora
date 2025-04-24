using Finora.Services;

namespace Finora.Pages 
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            monthPicker.SelectedIndex = DateTime.Now.Month - 1;
            LoadDataforMonth(DateTime.Now.Month);

        }
        private async Task LoadDataforMonth(int month) 
        {
            var user = Session.CurrentUser;
            if (user == null) return;

            var incomeList = await DatabaseHelper.GetTransactionsForUser(user.Id, "Income", null, month);
            var expenseList = await DatabaseHelper.GetTransactionsForUser(user.Id, "Expense", null, month);

            decimal totalIncome = incomeList.Sum(t => t.Amount);
            decimal totalExpenses = expenseList.Sum(t => t.Amount);
            decimal net = totalIncome - totalExpenses;

            incomeLabel.Text = $"Total Income: {totalIncome:C}";
            expensesLabel.Text = $"Total Expenses: {totalExpenses:C}";
            netLabel.Text = $"Net: {net:C}";
        }

        private async void OnMonthChanged(object sender, EventArgs e)
        {
            if (monthPicker.SelectedIndex >= 0)
            {
                int selectedMonth = monthPicker.SelectedIndex + 1;
                await LoadDataforMonth(selectedMonth);
            }
        }
        private async void OnTransClicked(object sender, EventArgs e) 
        {
            await Navigation.PushModalAsync(new AddTransaction());
        }

    }

}

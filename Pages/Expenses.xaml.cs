using Finora.Services;
namespace Finora.Pages
{

	public partial class Expenses : ContentPage
	{
		public Expenses()
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
                await LoadExpensesforMonth(DateTime.Now.Month);
            }
            else
            {
                await LoadExpensesforMonth(selectedMonth);
            }

        }

        private async Task LoadExpensesforMonth(int month)
        {
            var user = Session.CurrentUser;
            if (user == null) return;

            var expenseList = await DatabaseHelper.GetTransactionsForUser(user.Id, "Expense", null, month);

            TransactionList.Children.Clear();

            foreach (var expense in expenseList)
            {
                var row = new Grid
                {
                    ColumnDefinitions =
                {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                },
                    Padding = new Thickness(0, 5)
                };

                var dateLabel = new Label
                {
                    Text = expense.Date.ToString("MM/dd/yyyy"),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start
                };
                Grid.SetColumn(dateLabel, 0);
                row.Children.Add(dateLabel);

                var categoryLabel = new Label
                {
                    Text = expense.Category,
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(categoryLabel, 1);
                row.Children.Add(categoryLabel);

                var amountLabel = new Label
                {
                    Text = expense.Amount.ToString("C"),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.End
                };
                Grid.SetColumn(amountLabel, 2);
                row.Children.Add(amountLabel);

                TransactionList.Children.Add(row);
            }
        }
        private async void OnMonthChanged(object sender, EventArgs e)
        {
            if (monthPicker.SelectedIndex >= 0)
            {
                int selectedMonth = monthPicker.SelectedIndex + 1;
                await LoadExpensesforMonth(selectedMonth);
            }
        }

        private async void OnExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Modals.AddExpense());
        }
    }
}
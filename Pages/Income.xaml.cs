using Finora.Services;
namespace Finora.Pages
{
	public partial class Income : ContentPage
	{
		public Income()
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
                await LoadIncomeforMonth(DateTime.Now.Month);
            }
            else
            {
                await LoadIncomeforMonth(selectedMonth);
            }

        }
        private async Task LoadIncomeforMonth(int month)
        {
            var user = Session.CurrentUser;
            if (user == null) return;

            var incomeList = await DatabaseHelper.GetTransactionsForUser(user.Id, "Income", null, month);

            TransactionList.Children.Clear();

            foreach (var income in incomeList)
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
                    Text = income.Date.ToString("MM/dd/yyyy"),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start
                };
                Grid.SetColumn(dateLabel, 0);
                row.Children.Add(dateLabel);

                var categoryLabel = new Label
                {
                    Text = income.Category,
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(categoryLabel, 1);
                row.Children.Add(categoryLabel);

                var amountLabel = new Label
                {
                    Text = income.Amount.ToString("C"),
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
                await LoadIncomeforMonth(selectedMonth);
            }
        }

        private async void OnIncomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Modals.AddIncome());
        }
    }
}
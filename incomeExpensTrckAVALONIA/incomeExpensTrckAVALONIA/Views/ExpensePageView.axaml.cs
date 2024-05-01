using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using incomeExpensTrckAVALONIA.Models;
using incomeExpensTrckAVALONIA.Services;
using incomeExpensTrckAVALONIA.ViewModels;
using System.Diagnostics;

namespace incomeExpensTrckAVALONIA.Views
{
    public partial class ExpensePageView : UserControl
    {
        private readonly ExpensePageViewModel expensePageViewModel;
        private readonly MainViewModel mainViewModel;

        //public ExpensePageView(ExpensePageViewModel expensePageViewModel)
        public ExpensePageView()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            //this.expensePageViewModel = expensePageViewModel;
            var expenseService = new ExpenseService(); // Ensure ExpenseService is properly instantiated
            //DataContext = new ExpensePageViewModel(expenseService, mainViewModel);
            expensePageViewModel = new ExpensePageViewModel(expenseService, mainViewModel);
            DataContext = expensePageViewModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string id = button.Tag as string; // Assuming Id is a string, adjust the type accordingly
                if (expensePageViewModel != null)
                {
                    expensePageViewModel.NavToAndGetExpenseDetail(id);
                }
                else
                {
                    Debug.WriteLine("ExpensePageViewModel is not initialized.");
                }
            }
        }

    }
}

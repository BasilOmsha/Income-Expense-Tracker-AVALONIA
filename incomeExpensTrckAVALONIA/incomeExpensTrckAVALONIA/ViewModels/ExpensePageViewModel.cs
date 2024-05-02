using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Models;
using incomeExpensTrckAVALONIA.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class ExpensePageViewModel : ViewModelBase
    {
        private readonly ExpenseService expenseService;
        private readonly MainViewModel mainViewModel;

        public ObservableCollection<Expense> Expenses { get; private set; } = new();
        public string Message => "This is the Expense Page";

        public ExpensePageViewModel(ExpenseService expenseService, MainViewModel mainViewModel)
        //public ExpensePageViewModel(ExpenseService expenseService)
        {
            this.expenseService = expenseService;
            this.mainViewModel = mainViewModel;
            Expenses.Clear();
            GetExpenseList();
        }

        [RelayCommand]
       public void NavToAndGetExpenseDetail(string id)
        {
            if (id == string.Empty) return;

            //await Shell.Current.GoToAsync($"{nameof(ExpenseDetailPageView)}?Id={id}", true);
            //mainViewModel.NavToExpenseDetail(id);

            Debug.WriteLine($"Navigating to Expense Detail Page with Id: {id}");
            mainViewModel.NavToExpenseDetail(id);

        }

        [RelayCommand]
        public void RefreshExpenseList()
        {
            GetExpenseList();
        }

        [RelayCommand]
        void GetExpenseList()
        {
            if (Expenses.Any()) Expenses.Clear();

            var expenses = expenseService.GetExpenses();
            foreach (var expense in expenses) Expenses.Add(expense);
        }
    }
}

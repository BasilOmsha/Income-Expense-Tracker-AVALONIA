using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Models;
using incomeExpensTrckAVALONIA.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class ExpensePageViewModel : ViewModelBase
    {
        private readonly ExpenseService expenseService;

        public ObservableCollection<Expense> Expenses { get; private set; } = new();
        public string Message => "This is the Expense Page";

        public ExpensePageViewModel(ExpenseService expenseService)
        {
            this.expenseService = expenseService;
            Expenses.Clear();
            GetExpenseList();
        }

        [RelayCommand]
        void RefreshExpenseList()
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

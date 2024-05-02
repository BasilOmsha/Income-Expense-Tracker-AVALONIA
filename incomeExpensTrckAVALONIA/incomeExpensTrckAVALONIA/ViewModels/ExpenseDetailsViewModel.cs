using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Helpers;
using incomeExpensTrckAVALONIA.Models;
using incomeExpensTrckAVALONIA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class ExpenseDetailsViewModel : ViewModelBase
    {
        private readonly ExpenseService expenseService;
        private readonly MainViewModel mainViewModel;

        public ObservableCollection<string> YearsList { get; private set; }
        public ObservableCollection<string> MonthsList { get; private set; }
        public ObservableCollection<string> DaysList { get; private set; }
        public ObservableCollection<string> CategoriesList { get; private set; }
        public ObservableCollection<string> AccountsList { get; private set; }

        public ExpenseDetailsViewModel(ExpenseService expenseService, string selectedId, MainViewModel mainViewModel)
        {
            Title = "Add Expense";

            YearsList = new ObservableCollection<string>(dateGenerator.GetYearList(1920));
            MonthsList = new ObservableCollection<string>(dateGenerator.GetYearMonths(1));
            DaysList = new ObservableCollection<string>(dateGenerator.GetMonthDays(1));

            CategoriesList = new ObservableCollection<string>
            {
                "Select Category",
                "Groceries",
                "Social Life",
                "Pets",
                "Transport",
                "Health",
                "Education",
                "Entertainment",
                "Utilities",
                "Others"
            };

            AccountsList = new ObservableCollection<string>
            {
                "Select Account",
                "Cash",
                "Bank Account",
                "Mobile Money",
                "Card",
                "Others"
            };
            this.expenseService = expenseService;

            Id = selectedId;
            this.mainViewModel = mainViewModel;
            LoadExpense();
        }

        [ObservableProperty]
        Expense editableExpense;

        [ObservableProperty]
        string id;

        [RelayCommand]
        void Cancel()
        {
            mainViewModel.ShowExpensePage();
        }

        public void LoadExpense()
        {
            //Expense originalExpense = ExpenseService.GetExpense(Id);
            //EditableExpense = CloneExpense(originalExpense);
            //Debug.WriteLine($"The original date: {originalExpense.Day}");

            Expense originalExpense = ExpenseService.GetExpense(Id);
            if (originalExpense != null)
            {
                EditableExpense = CloneExpense(originalExpense);
                Debug.WriteLine($"Loaded expense data for ID: {Id}");
            }
            else
            {
                Debug.WriteLine($"Failed to load expense data for ID: {Id}");
            }
        }

        [RelayCommand]
        void UpdateExpense(string id)
        {
            if (id == string.Empty)
            {
                Debug.WriteLine("Error", "Try again", "Ok");
                return;
            }

            //var confirm = await Shell.Current.DisplayAlert("Warning", "Are you sure you want to update this expense?", "Yes", "No");
            //if (!confirm) return;
            Debug.WriteLine($"The new date: {EditableExpense.Day}");
            expenseService.UpdateExpense(EditableExpense);
            Debug.WriteLine("Info", expenseService.StatusMessage, "Ok");
            //await Shell.Current.GoToAsync("..");
        }

        private Expense CloneExpense(Expense originalExpense)
        {
            Console.WriteLine($"The day: {originalExpense.Day}");
            return new Expense
            {
                Id = originalExpense.Id,
                //Date = originalExpense.Date,
                Day = originalExpense.Day,
                Month = originalExpense.Month,
                Year = originalExpense.Year,
                Amount = originalExpense.Amount,
                Category = originalExpense.Category,
                Account = originalExpense.Account,
                Location = originalExpense.Location,
                Note = originalExpense.Note,
                Description = originalExpense.Description,
            };
        }
    }
}

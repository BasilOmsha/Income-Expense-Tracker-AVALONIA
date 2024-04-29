using incomeExpensTrckAVALONIA.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Models;
using incomeExpensTrckAVALONIA.Services;
using System.Threading.Tasks;
using System.Diagnostics;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class AddExpensePageViewModel : ViewModelBase
    {
        private readonly ExpenseService expenseService;
        private readonly MainViewModel mainViewModel;

        public ObservableCollection<string> YearsList { get; private set; }
        public ObservableCollection<string> MonthsList { get; private set; }
        public ObservableCollection<string> DaysList { get; private set; }
        public ObservableCollection<string> CategoriesList { get; private set; }
        public ObservableCollection<string> AccountsList { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AddExpensePageViewModel(ExpenseService expenseService, MainViewModel mainViewModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Title = "Add Expense";

            this.expenseService = expenseService;
            this.mainViewModel = mainViewModel;
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

            Day = DateTime.Now.Day.ToString();
            Month = DateTime.Now.Month.ToString();
            Year = DateTime.Now.Year.ToString();
        }

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string day;

        [ObservableProperty]
        string month;

        [ObservableProperty]
        string year;

        [ObservableProperty]
        string amount;

        [ObservableProperty]
        string category;

        //[ObservableProperty]
        //string selectedCategory;

        //[ObservableProperty]
        //string selectedAccount;


        [ObservableProperty]
        string account;

        [ObservableProperty]
        string location;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Location))]
        string latitude;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Location))]
        string longitude;

        [ObservableProperty]
        string note;

        [ObservableProperty]
        string description;


        [RelayCommand]
        void AddExpense()
        {
            if (string.IsNullOrEmpty(Day) || string.IsNullOrEmpty(Month) || string.IsNullOrEmpty(Year) || string.IsNullOrEmpty(Amount) || string.IsNullOrEmpty(Category) || string.IsNullOrEmpty(Account)
                || Account.Equals("Select Account") || Category.Equals("Select Category") || string.IsNullOrEmpty(Note) || !decimal.TryParse(Amount, out _) )
            {
                Debug.WriteLine("day: " + Day + " month: " + Month + " Category: " + Category);
                //await Shell.Current.DisplayAlert("Error", "Please fill in all fields", "Ok");
                Debug.WriteLine("Please fill in all fields");

                return; // return stops the execution of the method
            }
            Debug.WriteLine("all filled day: " + Day + " month: " + Month + " Category: " + Category);



            var expense = new Expense
            {
                //Date = Date.ToString(),
                Day = Day,
                Month = Month,
                Year = Year,
                Amount = double.Parse(Amount),
                Category = Category,
                Account = Account,
                Location = Location,
                Latitude = Latitude,
                Longitude = Longitude,
                Note = Note,
                Description = Description,
            };

            //foreach (var prop in expense.GetType().GetProperties())
            //{
            //    Debug.WriteLine(prop.Name + ": " + prop.GetValue(expense));
            //}

            Debug.WriteLine("Are you sure?");
            expenseService.AddExpense(expense);
            //await Shell.Current.DisplayAlert("Info", expenseService.StatusMessage, "Ok");
            ClearFields();
            //await Shell.Current.GoToAsync("..");
            mainViewModel.ShowExpensePage();
        }


        [RelayCommand]
        void ClearFields()
        {
            try
            {
                //Date = DateTime.Now;
                Day = DateTime.Now.Day.ToString();
                Month = DateTime.Now.Month.ToString();
                Year = DateTime.Now.Year.ToString();
                Amount = string.Empty;
                Category = CategoriesList[0];
                Account = AccountsList[0];
                Location = string.Empty;
                Note = string.Empty;
                Description = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}

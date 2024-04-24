using incomeExpensTrckAVALONIA.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class AddExpensePageViewModel : ViewModelBase
    {
        public ObservableCollection<string> YearsList { get; private set; }
        public ObservableCollection<string> MonthsList { get; private set; }
        public ObservableCollection<string> DaysList { get; private set; }

        //public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
        //{
        //"Groceries", "Food and Drinks", "Social Life", "Pets", "Transport",
        //"Culture", "Household", "Apparel", "Beauty", "Health", "Education", "Gifts", "Other"
        //};

        //public ObservableCollection<string> Accounts { get; } = new ObservableCollection<string>
        //{
        // "Cash", "Bank Accounts", "Card"
        //};

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AddExpensePageViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Title = "Add Expense";

            YearsList = new ObservableCollection<string>(dateGenerator.GetYearList(1920));
            MonthsList = new ObservableCollection<string>(dateGenerator.GetYearMonths(1));
            DaysList = new ObservableCollection<string>(dateGenerator.GetMonthDays(1));

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
        void ClearFields()
        {
            try
            {
                //Date = DateTime.Now;
                Day = DateTime.Now.Day.ToString();
                Month = DateTime.Now.Month.ToString();
                Year = DateTime.Now.Year.ToString();
                Amount = string.Empty;
                Category = string.Empty;
                Account = string.Empty;
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Services;
using incomeExpensTrckAVALONIA.Views;
using System.Threading.Tasks;

namespace incomeExpensTrckAVALONIA.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    ViewModelBase currentViewModel;

    public ExpenseService expenseService = new();

    [ObservableProperty]
    bool isVisible;
    public MainViewModel()
    {
        //ShowHomePage();
        ShowExpensePage();
    }

    [RelayCommand]
    public void ShowHomePage()
    {
        CurrentViewModel = new MainPageViewModel();
        Title = "Main Page";
        IsVisible = false;
    }

    [RelayCommand]
    public void ShowExpensePage()
    {
        CurrentViewModel = new ExpensePageViewModel(expenseService);
        Title = "Exp. Page";
        IsVisible = true;
    }

    [RelayCommand]
    public void NavToAddExpense()
    {
        CurrentViewModel = new AddExpensePageViewModel();
        Title = "Add an Expense";
        IsVisible = false;

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Services;

namespace incomeExpensTrckAVALONIA.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    ViewModelBase currentViewModel;

    public ExpenseService expenseService = new();

    public MainViewModel()
    {
        //CurrentViewModel = new MainPageViewModel();
        //Title = "Main Page";
        CurrentViewModel = new ExpensePageViewModel(expenseService);
        Title = "Expense Page";
    }

    [RelayCommand]
    public void ShowHomePage()
    {
        CurrentViewModel = new MainPageViewModel();
        Title = "Main Page";
    }

    [RelayCommand]
    public void ShowExpensePage()
    {
        CurrentViewModel = new ExpensePageViewModel(expenseService);
        Title = "Expense Page";
    }
}

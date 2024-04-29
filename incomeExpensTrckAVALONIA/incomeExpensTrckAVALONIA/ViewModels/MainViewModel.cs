using Avalonia.Animation;
using AvaloniaInside.Shell.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Services;
using incomeExpensTrckAVALONIA.Views;
using ReactiveUI;
using System.Threading.Tasks;

namespace incomeExpensTrckAVALONIA.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    //[ObservableProperty]
    //IPageTransition currentTranstion = PlatformSetup.TransitionForPage;
    //public string Greeting => "Welcome to Avalonia!";
    //private IPageTransition _currentTransition = PlatformSetup.TransitionForPage;
    //public IPageTransition CurrentTransition
    //{
    //    get => _currentTransition;
    //    set
    //    {
    //        this.SetProperty(ref _currentTransition, value);
    //        this.RaiseAndSetIfChanged(ref _currentTransition, value);
    //    }
    //}

    [ObservableProperty]
    ViewModelBase currentViewModel;

    public ExpenseService expenseService = new();

    [ObservableProperty]
    bool isVisible;
    private MainViewModel mainViewModel;

    public MainViewModel()
    {
        //ShowHomePage();
        mainViewModel = this;
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
        CurrentViewModel = new AddExpensePageViewModel(expenseService, mainViewModel);
        Title = "Add an Expense";
        IsVisible = false;

    }
}

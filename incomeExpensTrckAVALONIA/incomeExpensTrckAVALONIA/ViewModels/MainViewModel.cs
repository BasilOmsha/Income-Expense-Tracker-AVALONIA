using Avalonia.Animation;
using AvaloniaInside.Shell.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Services;
using incomeExpensTrckAVALONIA.Views;
using ReactiveUI;
using System.Diagnostics;
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

    [ObservableProperty]
    public static string idToSelect;

    public ExpenseService expenseService = new();

    [ObservableProperty]
    bool isVisible;

    [ObservableProperty]
    bool isVisible2;

    [ObservableProperty]
    bool isVisible3;

    private MainViewModel mainViewModel;

    public MainViewModel()
    {
        //CurrentViewModel = new ViewModelBase();
        //ShowHomePage();
        mainViewModel = this;
        ShowExpensePage();
        //CurrentViewModel = new ExpensePageViewModel(expenseService, mainViewModel);
        Debug.WriteLine("MainViewModel instantiated");
    }

    [RelayCommand]
    public void ShowHomePage()
    {
        CurrentViewModel = new MainPageViewModel();
        Title = "Main Page";
        IsVisible = false;
        IsVisible2 = false;
        IsVisible3 = false;
    }

    [RelayCommand]
    public void ShowExpensePage()
    {
        CurrentViewModel = new ExpensePageViewModel(expenseService, mainViewModel);
        Title = "Exp. Page";
        IsVisible = true;
        IsVisible2 = true;
        IsVisible3 = true;
    }

    [RelayCommand]
    public void NavToAddExpense()
    {
        CurrentViewModel = new AddExpensePageViewModel(expenseService, mainViewModel);
        Title = "Add an Expense";
        IsVisible = false;
        IsVisible2 = false;
        IsVisible3 = false;

    }

    [RelayCommand]
    public void GetId(string id)
    {
        Debug.WriteLine($"ID recieved: {id}");
        IdToSelect = id;
        Debug.WriteLine("IdToSelect set to " + IdToSelect);

        Debug.WriteLine("Check if executed");
    }

    [RelayCommand]
    public void NavToExpenseDetail2()
    {
        if (IdToSelect != null) // Ensure IdToSelect is not null
        {
            Debug.WriteLine($"Executing NavToExpenseDetail with ID: {IdToSelect}");
            CurrentViewModel = new ExpenseDetailsPageViewModel(expenseService, IdToSelect);
            Debug.WriteLine("CurrentViewModel set to ExpenseDetailsPageViewModel " + CurrentViewModel);
            Title = "Expense Detail Page";
            IsVisible = false;
            IsVisible2 = false;
            IsVisible3 = false;
        }
        else
        {
            Debug.WriteLine("IdToSelect is null");
        }

    }
}

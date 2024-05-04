using Avalonia.Animation;
using AvaloniaInside.Shell.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using incomeExpensTrckAVALONIA.Services;
using System.Diagnostics;
using Microsoft.Maui.Devices.Sensors;

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
    double lastClickedLatitude;

    [ObservableProperty]
    double lastClickedLongitude;

    public ExpenseService expenseService = new();


    [ObservableProperty]
    bool isVisible;


    private MainViewModel mainViewModel;

    //private IGeolocation IGeolocation;

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
    }

    [RelayCommand]
    public void ShowExpensePage()
    {
        CurrentViewModel = new ExpensePageViewModel(expenseService, mainViewModel);
        Title = "Exp. Page";
        IsVisible = true;
    }

    [RelayCommand]
    public void NavToAddExpense()
    {
        CurrentViewModel = new AddExpensePageViewModel(expenseService, mainViewModel, LastClickedLatitude, LastClickedLongitude);
        Title = "Add an Expense";
        IsVisible = false;


    }

    [RelayCommand]
    public void NavToExpenseDetail(string id)
    {
        //Debug.WriteLine($"Executing NavToExpenseDetail with ID: {id}");
        CurrentViewModel = new ExpenseDetailsViewModel(expenseService, id, mainViewModel);
        //Debug.WriteLine("CurrentViewModel set to ExpenseDetailsPageViewModel " + CurrentViewModel);
        Title = "Expense Details";
        IsVisible = false;
    }

    [RelayCommand]
    public void NavToMap()
    {
        CurrentViewModel = new SelectLocationViewModel(mainViewModel);
        Title = "Select Location";
        IsVisible = false;
    }
}

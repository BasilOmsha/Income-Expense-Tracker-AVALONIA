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
        Title = "Main Page Avalonia";
        IsVisible = false;
    }

    [RelayCommand]
    public void ShowExpensePage()
    {
        CurrentViewModel = new ExpensePageViewModel(expenseService, mainViewModel);
        Title = "Exp. Page Avalonia";
        IsVisible = true;
    }

    [RelayCommand]
    public void NavToAddExpense()
    {
        CurrentViewModel = new AddExpensePageViewModel(expenseService, mainViewModel, LastClickedLatitude, LastClickedLongitude);
        Title = "Add an Expense Avalonia";
        IsVisible = false;


    }

    [RelayCommand]
    public void NavToExpenseDetail(string id)
    {
        //Debug.WriteLine($"Executing NavToExpenseDetail with ID: {id}");
        CurrentViewModel = new ExpenseDetailsViewModel(expenseService, id, mainViewModel);
        //Debug.WriteLine("CurrentViewModel set to ExpenseDetailsPageViewModel " + CurrentViewModel);
        Title = "Expense Details Avalonia";
        IsVisible = false;
    }

    [RelayCommand]
    public void NavToMap()
    {
        CurrentViewModel = new SelectLocationViewModel(mainViewModel);
        Title = "Select Location Avalonia";
        IsVisible = false;
    }

    [RelayCommand]
    public void NavToExpenseMap()
    {
        CurrentViewModel = new ShowLocationsViewModel();
        Title = "Exp. Locations Avalonia";
        IsVisible = false;
    }
}

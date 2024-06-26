﻿using incomeExpensTrckAVALONIA.Services;
using incomeExpensTrckAVALONIA.ViewModels;
using incomeExpensTrckAVALONIA.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices.Sensors;

namespace incomeExpensTrckAVALONIA
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {

            collection.AddSingleton<MainViewModel>();
            collection.AddTransient<MainPageViewModel>();
            collection.AddSingleton<ExpensePageViewModel>(); // Singleton means that a single instance of the service is created and shared. Meaning that the same instance is used by all consumers.
            collection.AddSingleton<AddExpensePageViewModel>(); // Transient means that a new instance of the service is created each time it is requested.
            collection.AddTransient<ExpenseDetailsViewModel>();
            collection.AddSingleton<ShowLocationsViewModel>();

            collection.AddSingleton<MainView>();
            collection.AddTransient<MainPageView>();
            collection.AddSingleton<ExpensePageView>();
            collection.AddSingleton<AddExpensePageView>();
            collection.AddTransient<ExpenseDetailsView>();
            collection.AddSingleton<ShowLocationsView>();

            collection.AddTransient<ExpenseService>();
            //collection.AddSingleton<IGeolocation>(Geolocation.Default);
        }
    }
}

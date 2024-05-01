using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using incomeExpensTrckAVALONIA.ViewModels;
using System;

namespace incomeExpensTrckAVALONIA.Views
{
    public partial class AddExpensePageView : UserControl
    {

        public AddExpensePageView()
        {
            InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //    AvaloniaXamlLoader.Load(this);
        //}

        // When doing constrcture Injection and using the below code, the app doesn't navigate to the AddExpensePageView
        //private void RefreshContainerPage_RefreshRequested(object? sender, RefreshRequestedEventArgs e)
        //{
        //    // Retrieve a deferral object.
        //    var deferral = e.GetDeferral();

        //    //addExpensePageViewModel.ClearFieldsCommand.Execute(null);

        //    deferral.Complete();
        //}
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace incomeExpensTrckAVALONIA.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _title;
}


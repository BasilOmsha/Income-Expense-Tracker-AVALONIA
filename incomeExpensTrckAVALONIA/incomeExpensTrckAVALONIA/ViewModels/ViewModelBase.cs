using CommunityToolkit.Mvvm.ComponentModel;
//using ReactiveUI;

namespace incomeExpensTrckAVALONIA.ViewModels;

public partial class ViewModelBase : ObservableObject
//public partial class ViewModelBase : ReactiveObject
{
    [ObservableProperty]
    private string? _title;

}


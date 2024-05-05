using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace incomeExpensTrckAVALONIA.ViewModels
{
    public partial class SelectLocationViewModel : ViewModelBase
    {
        private readonly MainViewModel mainViewModel;

        [ObservableProperty]
        double lastClickedLatitude;

        [ObservableProperty]
        double lastClickedLongitude;


        public SelectLocationViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        [RelayCommand]
        void ConfirmLocation()
        {
            mainViewModel.LastClickedLatitude = LastClickedLatitude;
            mainViewModel.LastClickedLongitude = LastClickedLongitude;
            mainViewModel.NavToAddExpense();
        }
       
    }
}

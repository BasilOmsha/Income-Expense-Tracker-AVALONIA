using Avalonia.Controls;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Tiling;
using Mapsui.Projections;
using Mapsui.Extensions;
using Map = Mapsui.Map;
using Microsoft.Maui.Devices.Sensors;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Mapsui.Styles;
using incomeExpensTrckAVALONIA.Helpers;
using Color = Mapsui.Styles.Color;
using System.Collections.Generic;
using Mapsui.Nts;
using Mapsui.Layers;
using NetTopologySuite.Geometries;
using Microsoft.Maui.ApplicationModel;
using static Microsoft.Maui.ApplicationModel.Permissions;
//using Mapsui.UI.Maui;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using CommunityToolkit.Mvvm.ComponentModel;
using Mapsui.UI.Avalonia;
using Mapsui.UI.Avalonia.Extensions;
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

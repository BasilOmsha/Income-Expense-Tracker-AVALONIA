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
using Color = Mapsui.Styles.Color;
using System.Collections.Generic;
using Mapsui.Nts;
using NetTopologySuite.Geometries;
using incomeExpensTrckAVALONIA.Models;
using System.Collections.ObjectModel;
using incomeExpensTrckAVALONIA.Services;
using Microsoft.Maui.Controls;
using System.Linq;
using Avalonia.Interactivity;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;
using Mapsui.Widgets;

namespace incomeExpensTrckAVALONIA.Views
{
    public partial class ShowLocationsView : UserControl
    {
        private IGeolocation geolocation;

        private Location location;

        private MyLocationLayer? _myLocationLayer;

        GenericCollectionLayer<List<IFeature>> Layer = new GenericCollectionLayer<List<IFeature>>();
        public ExpenseService expenseService { get; private set; } = new();

        public ObservableCollection<Expense> ExpenseLocations { get; private set; } = new();


        public ShowLocationsView()
        {
            InitializeComponent();
            ExpenseLocations.Clear();
            MapInit();
        }

        public async void MapInit()
        {
            location = new();
            await GetMyLocation();
            DrawMap();
        }

        public async Task GetMyLocation() // This will get my current location. But with the emulator, it will get the location I set as the default location.
        {
            try
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($" {e.Message}");
            }
        }

        public void DrawMap()
        {
            var map = new Map();
            MapView.Map.Navigator.RotationLock = false;
            MapView.UnSnapRotationDegrees = 30;
            MapView.ReSnapRotationDegrees = 5;

            _myLocationLayer?.Dispose();
            _myLocationLayer = new MyLocationLayer(map)
            {
                IsCentered = false,
            };

            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center });
            map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40, BackColor = Color.White });
            map.Widgets.Add(new MapInfoWidget(map));
            map.Layers.Add(_myLocationLayer);
            map.Layers.Add(Layer);

            map.Navigator.ZoomToLevel(14);
            //var center = new MPoint(location.Longitude, location.Latitude);
            var center = new MPoint(24.478288503698295, 60.97635155698171); // This is the default location I set for the emulator as the location property is not working with the emulator.
            Debug.WriteLine($" {center.X} {center.Y}");

            MapView.GetMapInfo(center, 5);

            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(center.X, center.Y).ToMPoint();
            map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[4]);

            _myLocationLayer.UpdateMyLocation(sphericalMercatorCoordinate, true);

            GetExpenseLocations();


            foreach (var expense in ExpenseLocations)
            {
                Debug.WriteLine($"Expense: {expense.Amount} {expense.Latitude} {expense.Longitude}");
                var point = new MPoint(Convert.ToDouble(expense.Longitude), Convert.ToDouble(expense.Latitude));
                //var point = new MPoint(Convert.ToDouble(expense.Latitude), Convert.ToDouble(expense.Longitude));
                Debug.WriteLine($"ExpenseCoords: {point.X} {point.Y}");
                map.Info += (s, e) =>
                {
                    AddPin(point, Color.Purple);
                    Debug.WriteLine($"pinadded: {point.X} {point.Y}");
                    Layer?.DataHasChanged();
                };
            };
            MapView.Map = map;

        }

        private void AddPin(MPoint clickedPosition, Color color)
        {
            Map map = MapView.Map;

            Layer = new GenericCollectionLayer<List<IFeature>>
            {
                Style = SymbolStyles.CreatePinStyle(color)
            };
            map.Layers.Add(Layer);

            Layer?.Features.Add(new GeometryFeature
            {
                Geometry = new Point(clickedPosition.X, clickedPosition.Y)
            });

            Layer?.DataHasChanged();
        }

        public void GetExpenseLocations()
        {
            try
            {
                if (ExpenseLocations.Any()) ExpenseLocations.Clear();
                var expenses = expenseService.GetExpenses();
                foreach (var expense in expenses) ExpenseLocations.Add(expense);

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Unable to get expenses: {ex.Message}");
                Console.WriteLine($"Unable to get expenses: {ex.Message}");
                Shell.Current.DisplayAlert("Error", "Unable to get expenses", "Ok");
            }
            finally
            {

            }
        }
        public void ClickHandler(object sender, RoutedEventArgs args)
        {

            ExpenseLocations.Clear();
            MapInit();
        }

    }
}

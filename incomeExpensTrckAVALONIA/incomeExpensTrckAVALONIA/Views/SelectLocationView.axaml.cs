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
using Mapsui.UI.Maui;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using Avalonia.Interactivity;
using incomeExpensTrckAVALONIA.ViewModels;

namespace incomeExpensTrckAVALONIA.Views
{
    public partial class SelectLocationView : UserControl
    {

        private IGeolocation geolocation;

        private Location location;

        private MyLocationLayer? _myLocationLayer;

        GenericCollectionLayer<List<IFeature>> Layer = new GenericCollectionLayer<List<IFeature>>();

        //List<double> PinStored = new List<double>();

        //public ObservableCollection<double> PinStored = new ObservableCollection<double>();
        ObservableCollection<Tuple<double, double>> PinStored = new ObservableCollection<Tuple<double, double>>();


        public SelectLocationView()
        {
            InitializeComponent();
            MapInit();
            //DrawMap();
            //var map = new Map();
            //MapView.Map = map;
            //SelectLocationViewModel vm = this.DataContext as SelectLocationViewModel;
            //MapView.Map= vm.MapView.Map;
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
            catch (Exception e)
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
            //var location = new Location(location.Longitude, location.Latitude);
            //Debug.WriteLine($" {location.Longitude} {location.Latitude}");

            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.Layers.Add(_myLocationLayer);

            MPoint point = new MPoint(location.Longitude, location.Latitude);
            Debug.WriteLine($"The current location {point.X} {point.Y}");
            AddPin(point, Color.Blue);

            //map.Navigator.ZoomToLevel(14);
            //var center = new MPoint(24.478288503698295, 60.97635155698171);
            //var center = new MPoint(location.Longitude, location.Latitude);
            var center = new MPoint(24.478288503698295, 60.97635155698171); // This is the default location I set for the emulator as the location property is not working with the emulator.
            Debug.WriteLine($" {center.X} {center.Y}");

            MapView.GetMapInfo(center, 5);

            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(center.X, center.Y).ToMPoint();
            // Set the center of the viewport to the coordinate. The UI will refresh automatically
            // Additionally you might want to set the resolution, this could depend on your specific purpose

            map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[14]);

            _myLocationLayer.UpdateMyLocation(sphericalMercatorCoordinate, true);


            // Create a MemoryLayer to hold the pins
            //memoryLayer = new MemoryLayer();
            //map.Layers.Add(memoryLayer);

            MapView.Map = map;

            MapView.Info += OnMapClicked;

        }

        public void OnMapClicked(object sender, MapInfoEventArgs e)
        {

            // Get the clicked position
            var clickedPosition = e.MapInfo.WorldPosition;
            Debug.WriteLine($"clickedPosition:  {clickedPosition.X} {clickedPosition.Y}");

            SelectLocationViewModel vm = this.DataContext as SelectLocationViewModel;

            vm.LastClickedLatitude = clickedPosition.Y;
            vm.LastClickedLongitude = clickedPosition.X;

            // Remove all existing pins
            RemovePinsFromUI();

            AddPin(clickedPosition, Color.Purple);

            PinStored.Clear(); // Clear existing coordinates

            // Add the coordinates to the PinStored collection as a tuple
            PinStored.Add(Tuple.Create(clickedPosition.X, clickedPosition.Y));

            // Print the coordinates
            foreach (var coordinate in PinStored)
            {
                Debug.WriteLine($"ObjectList:   {coordinate.Item1} {coordinate.Item2}");
            }
            MapView.Refresh();
        }

        private void RemovePinsFromUI()
        {
            MapView.Map.Layers.Remove(Layer);
            Layer = new GenericCollectionLayer<List<IFeature>>();
            MapView.Refresh();
        }


        private void AddPin(MPoint clickedPosition, Color color)
        {
            Map map = MapView.Map;

            //var pinStyle = new SymbolStyle
            //{
            //    SymbolType = SymbolType.Ellipse,
            //    Fill = new Mapsui.Styles.Brush(color), // Set the pin color
            //    Outline = new Mapsui.Styles.Pen(color), // Set the outline color
            //    SymbolScale = 0.7F // Adjust the scale if needed
            //};

            Layer = new GenericCollectionLayer<List<IFeature>>
            {
                Style = SymbolStyles.CreatePinStyle(color)
                //Style = pinStyle

            };
            map.Layers.Add(Layer);

            // Add a point to the layer using the Info position
            Layer?.Features.Add(new GeometryFeature
            {
                Geometry = new Point(clickedPosition.X, clickedPosition.Y)
            });

            // To notify the map that a redraw is needed.
            Layer?.DataHasChanged();
        }
    }
}

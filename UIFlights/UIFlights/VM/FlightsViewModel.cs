﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using BE.Flights;
using BL;
using Microsoft.Maps.MapControl.WPF;
using static System.Net.Mime.MediaTypeNames;

namespace UIFlights
{
    public class FlightsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BLImp bl = BLImp.theInstance();
        private Random random = new Random();
        private ObservableCollection<FlightInfoPartial> listIncomingFlights;
        private ObservableCollection<FlightInfoPartial> listOutgoingFlights;
        private Dictionary<string, List<FlightInfoPartial>> Flights = new Dictionary<string, List<FlightInfoPartial>>();
        private FlightCommand flightcommand = new FlightCommand();
        private ExportCommand exportCommand = new ExportCommand();
        private Map myMap;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ResourceDictionary mainWindowResource;
        private bool isLoadingFlight = false;

        public bool IsLoadingFlight
        {
            get { return isLoadingFlight; }
            set
            {
                isLoadingFlight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoadingFlight"));
                }
            }
        }
        private bool isServerProblem = false;
        public bool IsServerProblem
        {
            get { return isServerProblem; }
            set
            {
                isServerProblem = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsServerProblem"));
                }
            }
        }
        private bool isNetworkProblem = false;
        public bool IsNetworkProblem
        {
            get { return isNetworkProblem; }
            set
            {
                isNetworkProblem = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsNetworkProblem"));
                }
            }
        }

        private FlightModel selectedFlightModel;
        public FlightModel SelectedFlightModel
        {
            get
            {
                return selectedFlightModel;
            }
            set
            {
                selectedFlightModel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedFlightModel"));
                }
            }
        }


        public FlightCommand Flightcommand
        {
            get 
            { 
                return flightcommand; 
            }
            set 
            { 
                flightcommand = value; 
            }
        }
        public ExportCommand ExportCommand
        {
            get 
            { 
                return exportCommand;
            }
            set 
            { 
                exportCommand = value; 
            }
        }
        public ObservableCollection<FlightInfoPartial> ListIncomingFlights
        {
            get
            {

                return listIncomingFlights;
            }
            set
            {
                listIncomingFlights = value;
            }
        }
        public ObservableCollection<FlightInfoPartial> ListOutgoingFlights
        {
            get
            {
                return listOutgoingFlights;
            }
            set { listOutgoingFlights = value; }
        }
        public FlightsViewModel(Map map, ResourceDictionary resources)
        {
            myMap = map;
            mainWindowResource = resources;
            //add functions to FlightCommand
            Flightcommand.SelectedFlight += UpdateCurrentFlight;
            Flightcommand.SelectedFlight += saveSelectedFlight;
            try
            {
                //init the flights lists for the first seconds of the running
                Flights = bl.GetCurrentFlightsSync();
                ListIncomingFlights = new ObservableCollection<FlightInfoPartial>(Flights["incoming"]);
                ListOutgoingFlights = new ObservableCollection<FlightInfoPartial>(Flights["outgoing"]);
            }
            catch (Exception ex)
            {
                if (ex is BE.NoDataException)
                    IsServerProblem = true;
                else
                {
                    IsNetworkProblem = true;
                }
            }
            UpdateAirplaneOnMap(null, null);
            initDispatcher();
        }
        private void initDispatcher()
        {
            dispatcherTimer.Tick += UpdateCurrentFlights;
            dispatcherTimer.Tick += UpdateAirplaneOnMap;
            dispatcherTimer.Tick += UpdateCurrentFlight;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }
        ~FlightsViewModel()
        {
            dispatcherTimer.Stop();
        }
        private void UpdateAirplaneOnMap(object sender, EventArgs e)
        {
            List<Pushpin> mapPushpinToRemove = new List<Pushpin>();
            foreach (var item in myMap.Children)
            {
                if (item is Pushpin)
                {
                    var p = item as Pushpin;
                    if (p.Name == "flightPin")
                        mapPushpinToRemove.Add(item as Pushpin);
                }
            }
            mapPushpinToRemove.ForEach(myMap.Children.Remove);
            addAirplanesToMap(ListOutgoingFlights);
            addAirplanesToMap(ListIncomingFlights);
        }
        private void addAirplanesToMap(ObservableCollection<FlightInfoPartial> flights)
        {
            try
            {
                foreach (var f in flights)
                {
                    Pushpin PinCurrent = new Pushpin { ToolTip = "source: " + f.Source + "\n destination: " + f.Destination };
                    PinCurrent.Name = "flightPin";
                    PinCurrent.Height = 20;
                    PinCurrent.Width = 20;
                    MouseBinding mouseBinding = new MouseBinding(Flightcommand, new MouseGesture(MouseAction.LeftClick));// add command definition 
                    mouseBinding.CommandParameter = f.FlightID;
                    PinCurrent.InputBindings.Add(mouseBinding);
                    PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };
                    MapLayer.SetPositionOrigin(PinCurrent, origin);
                    PinCurrent.DataContext = f;
                    PinCurrent.Style = (Style)mainWindowResource["flightStyle"];
                    var PlaneLocation = new Location { Latitude = f.Lat, Longitude = f.Long };
                    PinCurrent.Location = PlaneLocation;
                    myMap.Children.Add(PinCurrent);
                }
            }
            catch { }
        }
        /// <summary>
        /// update the flights in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateCurrentFlights(object sender, EventArgs e)
        {
            IsNetworkProblem = false;
            IsServerProblem = false;
            try
            {
                Flights = await bl.GetCurrentFlightsAsync();
                ListIncomingFlights.Clear();
                ListOutgoingFlights.Clear();
                try
                {
                    Flights["incoming"].ForEach(ListIncomingFlights.Add);
                    Flights["outgoing"].ForEach(ListOutgoingFlights.Add);
                }
                catch { }
            }
            catch (Exception ex)
            {
                if (ex is BE.NoDataException)
                    IsServerProblem = true;
                else
                {
                    IsNetworkProblem = true;
                }
            }
        }
        /// <summary>
        /// for dispatcher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateCurrentFlight(object sender, EventArgs e)
        {
            if (SelectedFlightModel != null)
            {
                string id = SelectedFlightModel.FlightId;
                SelectedFlightModel = await new FlightModel().initialize(id);
                ShowTrail(SelectedFlightModel.Trail);
            }
        }
        /// <summary>
        /// for the command 
        /// </summary>
        /// <param name="id"></param>
        private async void UpdateCurrentFlight(string id)
        {
            SelectedFlightModel = null;
            IsLoadingFlight = true;
            SelectedFlightModel = await new FlightModel().initialize(id);
            ShowTrail(SelectedFlightModel.Trail);
            IsLoadingFlight = false;
        }
        void ShowTrail(List<Trail> Route)
        {
            try
            {
                if (Route.Count == 0)
                    return;
                Route = (from f in Route
                         orderby f.ts
                         select f).ToList<Trail>();
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Purple);
                polyline.StrokeThickness = 3;
                polyline.Opacity = 1;
                polyline.Locations = new LocationCollection();
                foreach (var item in Route)
                {
                    polyline.Locations.Add(new Location(item.lat, item.lng));
                }
                List<MapPolyline> mapPolylinesToRemove = new List<MapPolyline>();
                List<Pushpin> mapPushpinToRemove = new List<Pushpin>();
                foreach (var item in myMap.Children)
                {
                    if (item is MapPolyline) { mapPolylinesToRemove.Add(item as MapPolyline); }
                    else if (item is Pushpin)
                    {
                        var p = item as Pushpin;
                        if (p.Name == "source")
                            mapPushpinToRemove.Add(item as Pushpin);
                    }
                }
                mapPolylinesToRemove.ForEach(myMap.Children.Remove);
                mapPushpinToRemove.ForEach(myMap.Children.Remove);
                myMap.Children.Add(polyline);
                Pushpin sourcePushPin = new Pushpin();
                sourcePushPin.Style = (Style)mainWindowResource["originPushpin"];
                sourcePushPin.Name = "source";
                sourcePushPin.Height = 20;
                sourcePushPin.Width = 20;
                var sourceLocation = new Location { Latitude = Route[0].lat, Longitude = Route[0].lng };
                sourcePushPin.Location = sourceLocation;
                myMap.Children.Add(sourcePushPin);
            }
            catch { }
        }


        private void saveSelectedFlight(string id)
        {
            try
            {
                var selectedFlight = listIncomingFlights.ToList().Find(f => f.FlightID == id);
                if (selectedFlight == null)
                {
                    selectedFlight = listOutgoingFlights.ToList().Find(f => f.FlightID == id);
                }
                bl.SaveFlightInfoPartial(selectedFlight);
            }
            catch { }
        }

    }
}

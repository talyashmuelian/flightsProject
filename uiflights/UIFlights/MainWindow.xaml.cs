using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE.Flights;
using System.ComponentModel;

namespace UIFlights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL bl = BLImp.theInstance();
        private FlightsViewModel flightsViewModel;
        FlightInfoPartial SelectedFlight = null; //Selected Flight
        public MainWindow()
        {

            try
            {
                InitializeComponent();
                flightsViewModel = new FlightsViewModel(myMap, Resources);
                mainGrid.DataContext = flightsViewModel;
                historyGrid.DataContext = new FlightsHistoryViewModel(progressBarHistory);
                DateGrid.DataContext = new hebrewDatesViewModel();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        void killThread(object sender, CancelEventArgs e)
        {
            try
            {
                System.Windows.MessageBox.Show("arrived");
                bl.DestroyThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //load current data
            //TrafficAdapter dal = new TrafficAdapter();
         //   var FlightKeys = bl.GetCurrentFlights();

            // this.DataContext = FlightKeys;
            //InFlightsListBox.DataContext = FlightKeys["incoming"];
            //OutFlightsListBox.DataContext = FlightKeys["outgoing"];

        }

        private void FlightsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    SelectedFlight = e.AddedItems[0] as FlightInfoPartial; //InFlightsListBox.SelectedItem as FlightInfoPartial;
        //    UpdateFlight(SelectedFlight);

        }

        //private void UpdateFlight(FlightInfoPartial selected)
        //{
        //    var Flight = bl.GetFlightData(selected.SourceId);

        //    DetailsPanel.DataContext = Flight;



        //    // Update map
        //    if (Flight != null)
        //    {
        //        var OrderedPlaces = (from f in Flight.trail
        //                             orderby f.ts
        //                             select f).ToList<Trail>();

        //        addNewPolyLine(OrderedPlaces);

        //        //MessageBox.Show(Flight.airport.destination.code.iata);
        //        Trail CurrentPlace = null;

        //        Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode };
        //        Pushpin PinOrigin = new Pushpin { ToolTip = Flight.airport.origin.name };

        //        PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };
        //        MapLayer.SetPositionOrigin(PinCurrent, origin);


        //        //Better to use RenderTransform
        //        if (Flight.airport.destination.code.iata == "TLV")
        //        {
        //            PinCurrent.Style = (Style)Resources["ToIsrael"];
        //        }
        //        else
        //        {
        //            PinCurrent.Style = (Style)Resources["FromIsrael"];
        //        }

        //        CurrentPlace = OrderedPlaces.Last<Trail>();
        //        var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
        //        PinCurrent.Location = PlaneLocation;


        //        CurrentPlace = OrderedPlaces.First<Trail>();
        //        PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
        //        PinOrigin.Location = PlaneLocation;

        //        //PinCurrent.MouseDown += Pin_MouseDown;

        //        myMap.Children.Add(PinOrigin);
        //        myMap.Children.Add(PinCurrent);

        //    }
        //}

        //private void Pin_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var pin = e.OriginalSource as Pushpin;
        //    MessageBox.Show(pin.ToolTip.ToString());
        //}


        //void addNewPolyLine(List<Trail> Route)
        //{
        //    MapPolyline polyline = new MapPolyline();
        //    //polyline.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
        //    polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
        //    polyline.StrokeThickness = 1;
        //    polyline.Opacity = 0.7;
        //    polyline.Locations = new LocationCollection();
        //    foreach (var item in Route)
        //    {
        //        polyline.Locations.Add(new Location(item.lat, item.lng));
        //    }

        //    myMap.Children.Clear();
        //    myMap.Children.Add(polyline);
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.Tick += DispatcherTimer_Tick;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
        //    dispatcherTimer.Start();
        }

        



        //private void DispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    UpdateFlight(SelectedFlight);
        //    Counter.Text = (Convert.ToInt32(Counter.Text) + 1).ToString();
        //}
    }
}

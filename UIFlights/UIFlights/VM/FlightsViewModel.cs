using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using BE.Flights;
using BL;
namespace UIFlights
{
    public class FlightsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightInfoPartial selectedFlight;
        public FlightInfoPartial SelectedFlight
        {
            get { return selectedFlight; }
            set
            {
                selectedFlight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedFlight"));
                }
            }
        }
        private FlightsModel flightsModel;
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
        private BLImp bl = BLImp.theInstance();

        private Random random = new Random();
        private ObservableCollection<FlightInfoPartial> listIncomingFlights;
        private ObservableCollection<FlightInfoPartial> listOutgoingFlights;
        private Dictionary<string, List<FlightInfoPartial>> Flights=new Dictionary<string, List<FlightInfoPartial>>();
        private FlightCommand flightcommand = new FlightCommand();
        public FlightCommand Flightcommand { get { return flightcommand; } set { flightcommand = value; } }
        public ObservableCollection<FlightInfoPartial> ListIncomingFlights {
            get
            {
                //return new ObservableCollection<FlightInfoPartial>(flightsModel.Flights["incoming"]);
                return listIncomingFlights;
            }
            set
            {
                listIncomingFlights = value;
            }
        }
        public ObservableCollection<FlightInfoPartial> ListOutgoingFlights {
            get
            {
                //return new ObservableCollection<FlightInfoPartial>(flightsModel.Flights["outgoing"]);
                return listOutgoingFlights;
            }
            set{ listOutgoingFlights = value; }
        }
        public FlightsViewModel()
        {
            flightsModel = new FlightsModel();
            flightcommand.SelectedFlight += extractSelectedFlight;
            flightcommand.SelectedFlight += saveSelectedFlight;
            //var Flights = bl.GetCurrentFlights();
            Flights = bl.GetCurrentFlightsSync();
           // ListIncomingFlights = new ObservableCollection<FlightInfoPartial>();
            //ListOutgoingFlights = new ObservableCollection<FlightInfoPartial>();
            ListIncomingFlights = new ObservableCollection<FlightInfoPartial>(Flights["incoming"]);
            ListOutgoingFlights = new ObservableCollection<FlightInfoPartial>(Flights["outgoing"]);
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_Flights;
            dispatcherTimer.Tick += DispatcherTimer_Tick_Flight;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
        }
      
        private void DispatcherTimer_Tick_Flights(object sender, EventArgs e)
        {
            bl.GetCurrentFlightsAsync(Flights);
            ListIncomingFlights.Clear();
            ListOutgoingFlights.Clear();
            try
            {
                Flights["outgoing"].Add(new FlightInfoPartial { Destination = random.Next(1, 100).ToString(), Source = "san fransisco", ID = 123 });
                Flights["incoming"].ForEach(ListIncomingFlights.Add);
                Flights["outgoing"].ForEach(ListOutgoingFlights.Add);
            }
            catch { }
        }
        private void DispatcherTimer_Tick_Flight(object sender, EventArgs e)/////////////////////////////////////////////////////
        {
            if (selectedFlight!=null)
                extractSelectedFlight(selectedFlight.FlightID);
        }

        private void extractSelectedFlight(string id)
        {
            SelectedFlightModel = new FlightModel(id);
            selectedFlight = listIncomingFlights.ToList().Find( f=>f.FlightID == id);
            if(selectedFlight==null)
            {
                selectedFlight = listOutgoingFlights.ToList().Find(f => f.FlightID == id);
            }
            //עדכון נתונים עבור טיסה בודדת

        }
        private void saveSelectedFlight(string id)
        {
            selectedFlight = listIncomingFlights.ToList().Find(f => f.FlightID == id);
            if (selectedFlight == null)
            {
                selectedFlight = listOutgoingFlights.ToList().Find(f => f.FlightID == id);
            }
            bl.SaveFlightInfoPartial(selectedFlight);
        }

    }
}

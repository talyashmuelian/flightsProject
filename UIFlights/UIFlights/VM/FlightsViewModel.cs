using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;
namespace UIFlights
{
    public class FlightsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightsModel flightsModel;
       
        public ObservableCollection<FlightInfoPartial> listIncomingFlights {
            get
            {
                return new ObservableCollection<FlightInfoPartial>( flightsModel.Flights["incoming"]);
            }
        }
        public ObservableCollection<FlightInfoPartial> listOutGoingFlights {
            get
            {
                return new ObservableCollection<FlightInfoPartial>(flightsModel.Flights["outgoing"]);
            }
        }
        public FlightsViewModel()
        {
            flightsModel = new FlightsModel();
        }

    }

}

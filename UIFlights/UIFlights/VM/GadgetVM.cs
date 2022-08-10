using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UIFlights
{
    public class GadgetVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
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
        private CloseCommand closeCommand=new CloseCommand();
        public CloseCommand CloseCommand
        {
            get { return closeCommand; }
            set { closeCommand = value; }
        }
        public GadgetVM(FlightModel flightModel)
        {
            SelectedFlightModel = flightModel;
            dispatcherTimer.Tick += DispatcherTimer_Tick_Flight;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }
        private async void DispatcherTimer_Tick_Flight(object sender, EventArgs e)/////////////////////////////////////////////////////
        {
            if (SelectedFlightModel != null)
            {
                string id = SelectedFlightModel.FlightId;
                SelectedFlightModel = await new FlightModel(id).initialize(id);
            }
        }


    }
}

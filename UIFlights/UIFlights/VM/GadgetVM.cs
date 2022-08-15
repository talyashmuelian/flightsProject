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

        public CloseCommand CloseCommand { get; set; } = new CloseCommand();
        public GadgetVM(FlightModel flightModel)
        {
            SelectedFlightModel = flightModel;
            //init the dispatcher
            dispatcherTimer.Tick += UpdateCurrentFlight;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// the function called from the dispatcher and update the gadget's flight 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateCurrentFlight(object sender, EventArgs e)
        {
            if (SelectedFlightModel != null)
            {
                string id = SelectedFlightModel.FlightId;
                SelectedFlightModel = await new FlightModel().initialize(id);
            }
        }


    }
}

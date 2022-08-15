using BE.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UIFlights
{
    /// <summary>
    /// command for show flight details
    /// </summary>
    public class FlightCommand : ICommand
    {
        public event Action<string> SelectedFlightEvent; //parameter is id flight
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string FlightID = parameter as string;
            if (SelectedFlightEvent != null)
            {
                SelectedFlightEvent(FlightID);
            }
        }
    }
}

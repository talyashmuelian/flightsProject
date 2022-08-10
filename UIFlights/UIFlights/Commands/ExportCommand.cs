using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BE;

namespace UIFlights
{
    public class ExportCommand:ICommand
    {
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
            FlightModel Flight = parameter as FlightModel;
            if (Flight == null)
            {
                MessageBox.Show("no selected flight");
            }
            else
            {
                Gadget gadget = new Gadget(Flight);
                gadget.Show();
            }
        }
    }
}

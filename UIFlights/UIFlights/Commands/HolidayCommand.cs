using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UIFlights
{
    public class HolidayCommand:ICommand
    {
        public event Action<DateTime> SelectedDate; 
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
            try
            {
                DateTime date = DateTime.Parse(parameter.ToString());

                if (SelectedDate != null)
                {
                    SelectedDate(date);
                }
            }
            catch { MessageBox.Show("no selected date"); }

        }
    }
}

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
    /// command for change date
    /// </summary>
    public class HolidayCommand : ICommand
    {
        public event Action<DateTime> SelectedDateEvent;
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

                if (SelectedDateEvent != null)
                {
                    SelectedDateEvent(date);
                }
            }
            catch { MessageBox.Show("no selected date"); }

        }
    }
}

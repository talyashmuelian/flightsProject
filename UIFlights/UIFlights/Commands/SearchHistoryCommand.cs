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
    /// command for search saved flights in specific range dates
    /// </summary>
    public class SearchHistoryCommand : ICommand
    {
        public event Action<DateTime, DateTime> SelectedRangeDatesEvent; //parameter is 2 dates
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
                List<Object> dates = (parameter as IEnumerable<object>).ToList();
                if (SelectedRangeDatesEvent != null)
                {
                    DateTime date1 = DateTime.Parse(dates[0].ToString());
                    DateTime date2 = DateTime.Parse(dates[1].ToString());
                    SelectedRangeDatesEvent(date1, date2);
                }
            }
            catch { MessageBox.Show("no selected date"); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIFlights
{
    public class SearchHistoryCommand : ICommand
    {
        public event Action<DateTime,DateTime> SelectedRangeDates; //parameter is 2 dates
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
            List<Object> dates= parameter as List<object>;
            if (SelectedRangeDates != null)
            {
                DateTime date1 = DateTime.Parse(dates[0].ToString());
                DateTime date2 = DateTime.Parse(dates[1].ToString());

                SelectedRangeDates(date1, date2);
            }
        }
    }
}

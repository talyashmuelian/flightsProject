using BE.Flights;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFlights
{
    public class FlightsHistoryViewModel
    {
        BLImp bl = BLImp.theInstance();

        private SearchHistoryCommand searchHistoryCommand = new SearchHistoryCommand();
        public SearchHistoryCommand SearchHistoryCommand { get { return searchHistoryCommand; } set { searchHistoryCommand = value; } }
        public ObservableCollection<FlightInfoPartial> SavedFlights { get; set; }
        public FlightsHistoryViewModel()
        {
            SearchHistoryCommand.SelectedRangeDates +=GetSavedFlights ;
            SavedFlights = new ObservableCollection<FlightInfoPartial>();
        }
        private void GetSavedFlights(DateTime date1, DateTime date2)
        {
            SavedFlights.Clear();
            bl.GetSavedFlights(date1, date2).ForEach(SavedFlights.Add);
        }
    }
}

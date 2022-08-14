using BE.Flights;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIFlights
{
    public class FlightsHistoryViewModel
    {
        BLImp bl = BLImp.theInstance();
        private HistoryModel historyModel = new HistoryModel();
        private SearchHistoryCommand searchHistoryCommand = new SearchHistoryCommand();
        private ObservableCollection<FlightInfoPartial> savedFlights= new ObservableCollection<FlightInfoPartial>();

        public SearchHistoryCommand SearchHistoryCommand 
        { 
            get 
            { 
                return searchHistoryCommand; 
            } 
            set 
            { 
                searchHistoryCommand = value; 
            } 
        }
        public ObservableCollection<FlightInfoPartial> SavedFlights
        {
            get
            {
                return savedFlights;
            }
            set
            {
                savedFlights = value;
            }
        }
        
        public FlightsHistoryViewModel()
        {
            SearchHistoryCommand.SelectedRangeDates +=GetSavedFlights ;
        }
        private void GetSavedFlights(DateTime date1, DateTime date2)
        {
            try
            {
                SavedFlights.Clear();
                var saved = historyModel.GetSavedFlights(date1, date2);
                saved.ForEach(SavedFlights.Add);
            }
            catch { }
        }
    }
}

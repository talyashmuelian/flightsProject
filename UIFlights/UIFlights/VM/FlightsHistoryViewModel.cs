using BE.Flights;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIFlights
{
    public class FlightsHistoryViewModel
    {
        BLImp bl = BLImp.theInstance();
        private HistoryModel historyModel = new HistoryModel();
        public SearchHistoryCommand SearchHistoryCommand { get; set; } = new SearchHistoryCommand();
        public ObservableCollection<FlightInfoPartial> SavedFlights { get; set; } = new ObservableCollection<FlightInfoPartial>();

        public FlightsHistoryViewModel()
        {
            SearchHistoryCommand.SelectedRangeDatesEvent += GetSavedFlights;
        }

        /// <summary>
        /// run when command is called
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        private void GetSavedFlights(DateTime date1, DateTime date2)
        {
            try
            {
                if (date2<date1)
                { MessageBox.Show("end date must be after start date");}
                SavedFlights.Clear();
                var saved = historyModel.GetSavedFlights(date1, date2);
                saved.ForEach(SavedFlights.Add);
            }
            catch { }
        }
    }
}

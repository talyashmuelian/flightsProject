﻿using BE.Flights;
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
    public class FlightsHistoryViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        BLImp bl = BLImp.theInstance();

        private SearchHistoryCommand searchHistoryCommand = new SearchHistoryCommand();


        public SearchHistoryCommand SearchHistoryCommand { get { return searchHistoryCommand; } set { searchHistoryCommand = value; } }
        public ObservableCollection<FlightInfoPartial> SavedFlights { get; set; }
        public ProgressBar ProgressBar { get; set; }
        private bool isLoadingHistory = false;
        public bool IsLoadingHistory
        {
            get { return isLoadingHistory; }
            set
            {
                isLoadingHistory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoadingHistory"));
                }
            }
        }
        public FlightsHistoryViewModel(ProgressBar progressBar)
        {
            ProgressBar = progressBar;
            SearchHistoryCommand.SelectedRangeDates +=GetSavedFlights ;
            SavedFlights = new ObservableCollection<FlightInfoPartial>();
        }
        private void GetSavedFlights(DateTime date1, DateTime date2)
        {
            //IsLoadingHistory = true;
            ProgressBar.Visibility = System.Windows.Visibility.Visible;
            SavedFlights.Clear();
            var saved = bl.GetSavedFlights(date1, date2);
            saved.Sort((i, j) => j.CreateTime.CompareTo(i.CreateTime));
            // bl.GetSavedFlights(date1, date2).ForEach(SavedFlights.Add);
            saved.ForEach(SavedFlights.Add);
            ProgressBar.Visibility = System.Windows.Visibility.Hidden;
            //IsLoadingHistory = false;
        }
    }
}

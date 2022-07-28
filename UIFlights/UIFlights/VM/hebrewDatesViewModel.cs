using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using BL;

namespace UIFlights
{
    public class hebrewDatesViewModel : INotifyPropertyChanged
    {
        private BLImp bl = BL.BLImp.theInstance();
        private System.Threading.Timer timer;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public DateTime CurrentDate { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isBeforeHoliday;

        public bool IsBeforeHoliday
        {
            get { return isBeforeHoliday; }
            set { 
                isBeforeHoliday = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBeforeHoliday"));
                }
            }
        }
        public HolidayCommand HolidayCommand { get; set; }
        public hebrewDatesViewModel()
        {
            SetUpTimer(new TimeSpan(0,0,0));
            CheckIsBeforeHoliday();
            HolidayCommand.SelectedDate += CheckIsBeforeHoliday;
        }
        
        private void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            this.timer = new System.Threading.Timer(x =>
            {
                this.SomeMethodRunsAt1600();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private void SomeMethodRunsAt1600()
        {
            //this runs at 00:00:00
            //
            dispatcherTimer.Tick += CheckIsBeforeHoliday;
            dispatcherTimer.Interval = new TimeSpan(24, 0, 0);
            dispatcherTimer.Start();
        }
        private void CheckIsBeforeHoliday(object sender, EventArgs e)
        {
            CheckIsBeforeHoliday();
        }
        private async void CheckIsBeforeHoliday()
        {
            isBeforeHoliday = await bl.IsBeforeHoliday(CurrentDate);
        }
    }
}

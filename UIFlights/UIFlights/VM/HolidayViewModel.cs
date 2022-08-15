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
    public class HolidayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BLImp bl = BL.BLImp.theInstance();
        private HolidayModel holidayModel = new HolidayModel();
        private Timer timer;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool isBeforeHoliday;
        private string upcomingHoliday;
        private bool isLoadingDate = false;

        public DateTime CurrentDate { get; set; }
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

        public bool IsLoadingDate
        {
            get { return isLoadingDate; }
            set
            {
                isLoadingDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoadingDate"));
                }
            }
        }
        public string UpcomingHoliday
        {
            get { return upcomingHoliday; }
            set
            {
                upcomingHoliday = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UpcomingHoliday"));
                }
            }
        }
        public HolidayCommand HolidayCommand { get; set; }
        public HolidayViewModel()
        {
            CurrentDate = DateTime.Now;
            HolidayCommand = new HolidayCommand();
            SetUpTimer(new TimeSpan(0,0,0));
            CheckIsBeforeHoliday(CurrentDate);
            HolidayCommand.SelectedDateEvent += CheckIsBeforeHoliday;
        }
        
        /// <summary>
        /// start the dispatcher at the required time
        /// </summary>
        /// <param name="alertTime"></param>
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
                this.StartDispatcherAt12AM();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private void StartDispatcherAt12AM()
        {
            //this runs at 00:00:00
            dispatcherTimer.Tick += CheckIsBeforeHolidayDispatcher;
            dispatcherTimer.Interval = new TimeSpan(24, 0, 0);
            dispatcherTimer.Start();
        }
        private void CheckIsBeforeHolidayDispatcher(object sender, EventArgs e)
        {
           CurrentDate=CurrentDate.AddDays(1);
            CheckIsBeforeHoliday(CurrentDate);
        }

        /// <summary>
        /// call the model function to check is before holiday
        /// </summary>
        /// <param name="date"></param>
        private async void CheckIsBeforeHoliday( DateTime date)
        {
            try
            {
                IsLoadingDate = true;
                var result = await holidayModel.CheckIsBeforeHoliday(date);
                IsBeforeHoliday = result.Item1;
                UpcomingHoliday = result.Item2;
                IsLoadingDate = false;
            }
            catch { }
        }
    }
}

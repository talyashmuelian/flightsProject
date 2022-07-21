using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFlights
{
    public class hebrewDatesViewModel : INotifyPropertyChanged
    {
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
        public hebrewDatesViewModel()
        {

        }

    }
}

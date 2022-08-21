using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;
using BL;

namespace UIFlights
{
    public class HistoryModel
    {
        BLImp bl = BLImp.theInstance();
        public HistoryModel()
        {

        }

        /// <summary>
        /// return all saved flight in specific range dates, sorted reverse
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public List<FlightInfoPartial> GetSavedFlights(DateTime date1, DateTime date2)
        {
            date2 = date2.AddHours(23).AddMinutes(59).AddSeconds(59);
            var saved = bl.GetSavedFlights(date1, date2);
            saved.Sort((i, j) => j.CreateTime.CompareTo(i.CreateTime));
            return saved;
        }
    }
}

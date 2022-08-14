using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFlights
{
    public class HolidayModel
    {
        BLImp bl = BLImp.theInstance();
        public HolidayModel()
        {

        }
        public async Task<Tuple<bool, string>> CheckIsBeforeHoliday(DateTime date)
        {
            return await bl.IsBeforeHoliday(date);
        }
    }
}

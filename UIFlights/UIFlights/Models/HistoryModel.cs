﻿using System;
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
        public List<FlightInfoPartial> GetSavedFlights(DateTime date1, DateTime date2)
        {
            var saved = bl.GetSavedFlights(date1, date2);
            saved.Sort((i, j) => j.CreateTime.CompareTo(i.CreateTime));
            return saved;
        }
    }
}
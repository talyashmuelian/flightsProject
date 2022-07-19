﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Flights
{
    public class FlightInfoPartial
    {
        public int ID { get; set; }
        public string FlightID { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FlightCode { get; set; }
        public DateTime CreateTime { get; private set; }

        public FlightInfoPartial()
        {
            CreateTime = DateTime.Now;
        }

        //public override string ToString()
        //{
        //    return "id: "+ID.ToString()+" SourceId: "+SourceID+" Long: "+Long.ToString()
        //        +" Lat: "+Lat.ToString()+" Dateandtime: "+DateAndTime.ToString()+" source: "+Source+
        //        " Detination: "+Destination+" flightcode: "+FlightCode;
        //}
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}

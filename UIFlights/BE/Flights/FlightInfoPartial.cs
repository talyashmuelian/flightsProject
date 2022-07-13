using Newtonsoft.Json;
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
        public string SourceID { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FlightCode { get; set; }

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

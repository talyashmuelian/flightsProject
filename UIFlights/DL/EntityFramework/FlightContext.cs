using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;

namespace DL
{
    public class FlightContext: DbContext
    {
        public FlightContext() : base("DBFlight") { }
        public DbSet<FlightInfoPartial> Flights { get; set; }
    }
}

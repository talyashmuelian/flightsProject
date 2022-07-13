using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;
using BE.Whether;
namespace BL
{
    public interface IBL
    {
        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights();
        BE.Whether.Root GetWhether(double lon, double lat);
    }
}

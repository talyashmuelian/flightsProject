using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class IATADictAirports
    {
        private static Dictionary<string, string> IATAdict = new Dictionary<string, string>()
        {
            { "TLV", "Israel"},
            { "PFO","Cyprus" },
            { "JTR","Greece" },
            { "ATH","Greece" },
            { "VIE","Austria" },
            { "IST","Turkey" },
            { "LEJ","Germany" },
            { "LCA","Cyprus" },
            { "NAP","Italy" },
            { "AUH","United Arab\n Emirates" },
            { "LGG","Belgium" },
            { "ETM","Israel" },
            { "BUD","Hungary" },
            { "MAD","Spain" },
            { "OTP","Romania" },
            { "SAW","Turkey" },
            { "LJU","Slovenia" },
            { "FCO","Italy" },
            { "CHQ","Greece" },
            { "TRN","Italy" },
            { "MXP","Italy" },
            { "MLA","Malta" },
            { "CDG","France" },
            { "JMK","Greece" },
            { "DLM","Turkey" },
            { "TGD","Montenegro" },
            { "","" },
            { "","" },
            { "","" },
            { "","" },
            { "","" },
            { "","" },
            { "","" },
            { "","" },
        };
        public static string IATACodeToCountry(string IATACode)
        {
            try
            {
                return IATAdict[IATACode];
                    }
            catch { return ""; }
        }
    }
}

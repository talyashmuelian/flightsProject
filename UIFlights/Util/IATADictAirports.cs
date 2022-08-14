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
            { "AUH","United \nArab Emirates" },
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
            {"MRS","France" },
            {"IAS","Romania" },
            {"HER","Greece" },
            {"AYT","Turkey" },
            {"BOJ","Bulgaria" },
            {"SSH","Egypt" },
            {"BUS","Georgia" },
            {"BJV","Turkey" },
            {"AMS","Netherthelands" },
            {"CPH","Denemark" },
            {"BER","Germany" },
            {"CTU","China" },
            {"DXB","United \nArab Emirates" },
            {"GYD","Azerbaijan" },
            {"ADB","Turkey" },

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

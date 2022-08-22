﻿using System;
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
            {"RHO","Greece" },
            {"DUS","Germany" },
            {"VAR","Bulgaria" },
            {"LHR","United \nKingdom" },
            {"JFK","United \nStates" },
            {"LAX","United \nStates" },
            {"WAW","Poland" },
            {"BLQ","Italy" },
            {"BOS","United \nStates" },
            {"BAH","Bahrain" },
            {"INN","Austria" },
            {"CMN","Morocco" },
            {"FMM","Germany" },
            {"BGY","Italy" },
            {"GVA","Switzerland" },
            {"TAS","Uzbekistan" },
            {"NHT","United \nKingdom" },
            {"YYZ","Canada" },
            {"DME","Russia" },
            {"TIV","Montenegro" },
            {"EWR","United \nStates" },
            {"SOF", "Bulgaria" },
            {"SKG","Greece" },
            {"YVR","Canada" },
            {"BRU","Belgium" },
            {"YUL","Canada" },
            {"LTN","United \nKingdom" },
            {"ORY","France" },
            {"OLB","Italy" },
            {"FRA","Germany" },
            {"CGN","Germany" },
            {"AOK","Greece" },
             ///////מפה הוספתי
            {"CFU","Greece" },
            {"PRG","Czechia" },
            {"NCE","France" },
            {"CTA","Italy" },
            {"ZRH","Switzerland" },
            {"MIA","United \nStates" },
            {"MUC","Germany" },
            {"VKO","Russia" },
            {"HEL","Finland" },
            {"SFO","United \nStates" },
            {"TZX","Turkey" },
            {"ADD","Ethiopia" },
            {"TBS","Georgia" },
            {"KEF","Iceland" },
            {"KRK","Poland" },
            {"CLJ","Romania" },
            {"BSL","France" },
            {"JRO","Tanzania" },
            {"KIV","Moldova" },
            {"MRV","Russia" },
            {"CAI","Egypt" },
            {"TLS","France" },
            {"SZG","Austria" },
            {"LBG","France" },
            {"ZAG","Croatia" },
            {"CRA","Romania" },
            {"LIS","Portugal" },
            {"BCN","Spain" },
            {"RAK","Morocco" },
            {"BEG","Serbia" },
            {"CRL","Belgium" },
            {"BRI","Italy" },
            {"VRN","Italy" },
            {"BKK","Thailand" },
            //מפה
            {"FNC" ,"Portugal"},
            {"KGS","Greece" },
            {"VNO", "Lithuania"},
            {"ADJ","Jordan" },
            {"FKB","Germany" },
            {"PVK","Greece" },
            {"TSF","Italy" },
            {"SEZ","Seychelles" },
            {"DEB","Hungary" },
            {"ARN","Sweden" },
            {"VCE","Italy" },
            {"IAD","United \nStates" },
            {"LYS","France" },
            {"LGW","United \nKingdom" },
            {"OSL","Norway" },
            {"AMM","Jordan" },
            {"HRG","Egypt" },
            //מפה
            {"ORD","United \nStates" },
            {"MAN","United \nKingdom" }

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

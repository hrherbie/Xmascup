using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XmcLib
{
    public class Altersklasse
    {
        public DateTime Wettkampfdatum { get; set; }
        public int    Wettkampfjahr { get; set; }
        public int         Wettkampfalter;
        public DateTime    Geburtstag { get; set; }
        public int Jahrgang;
        public int AlterAktuell;
        public string Altersklassenbezeichnung;
        public double MastersAgeMultible;
       

        public Altersklasse(DateTime geburtstag, DateTime wkdatum)
        {
            Geburtstag = geburtstag;
            Jahrgang = Geburtstag.Year;
            Wettkampfjahr = wkdatum.Year;
            Wettkampfdatum = wkdatum;
            MakeAkAndAge(Geburtstag, Wettkampfdatum, 
                out Jahrgang,
                out Wettkampfalter, out Altersklassenbezeichnung,
                out AlterAktuell);
            MastersAgeMultible = MAM.GetMAM(Wettkampfalter);
           
        }


        public void MakeAkAndAge(DateTime geburtstag, DateTime wkdatum, 
            out int Jahrgang, out int alterwk, out string ak, out int alterakt)
        {

            DateTime wkOhneZeit = new DateTime(wkdatum.Year, wkdatum.Month, wkdatum.Day);
            DateTime aktGeburtstag = new DateTime(wkdatum.Year, geburtstag.Month, 
                geburtstag.Day
               );
           
            int result = DateTime.Compare(wkOhneZeit,aktGeburtstag);
        /*    if (result < 0)
                .WriteLine("Wettkampf date is earlier than Birthday");
            else if (result == 0)
                .WriteLine("Both Wettkampfdatum und Geburtag dates are same");
            else
                .WriteLine("Wettkampf date is later than Birthday");
        */
            Jahrgang = Geburtstag.Year;
            ak = string.Empty;
            alterwk = wkdatum.Year - Jahrgang;
            alterakt = 0;
           
            if(result < 0)
            {
                alterakt = alterwk - 1;
            }
            else
                alterakt = alterwk;


            if (Jahrgang != 0)
            {
               
               
                string _AK = string.Empty;

                if (alterwk >= 14 && alterwk <= 18) _AK = "Jugend";
                if (alterwk >= 14 && alterwk <= 15) _AK = "Jugend B";
                if (alterwk >= 16 && alterwk <= 18) _AK = "Jugend A";
                if (alterwk >= 19 && alterwk <= 23) _AK = "Junior";
                if (alterwk >= 24 && alterwk <= 39) _AK = "Allgem";
                if (alterwk >= 40 && alterwk <= 49) _AK = "Masters 1";
                if (alterwk >= 50 && alterwk <= 59) _AK = "Masters 2";
                if (alterwk >= 60 && alterwk <= 69) _AK = "Masters 3";
                if (alterwk >= 70) _AK = "Masters 4";

                ak = _AK;
            }
        }

        /**
         * <summary>Convert this instance into a string representation.</summary>
         *
         * <remarks>Beate, 26.09.2013.</remarks>
         *
         * <returns>This instance as a string.</returns>
         */

        public override string ToString()
        {
            
            string details = string.Empty;
            details += this.GetType().FullName + "\n";
            int len = details.Length;
            for ( int i = 0; i <len; i++) details += "-";
            details += 
        "\nWettkampfdatum:           " + this.Wettkampfdatum.ToShortDateString();
            details += "\nWettkampfjahr:            " + this.Wettkampfjahr;
            details += "\nGeburtstag:               " + this.Geburtstag.ToShortDateString();
            details += "\nJahrgang:                 " + this.Jahrgang;
            details += "\nAktuelles Alter:          " + this.AlterAktuell;
            details += "\nWettkampfalter:           " + this.Wettkampfalter;
           
            details += "\nAltersklassenbezeichnung: " + this.Altersklassenbezeichnung;
            details += "\nMastersAgeMultiple(MAM):  " + this.MastersAgeMultible;
            details += "\n\n";



            return details;
        }

    }
}

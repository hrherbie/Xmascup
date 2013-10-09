using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmcLib
{   
    /**
     * <summary>Gewichtsklasse.</summary>
     *
     * <remarks>Beate, 28.09.2013.</remarks>
     */

    public class Gewichtsklasse
    {
        public string Gewichtsklassenbez;
        public double WilksFaktor;
        public double Koerpergewicht { get; set; }
        public string Geschlecht { get; set; }

        public double GBC;

        public override string ToString()
        {

            string details = string.Empty;
            details += this.GetType().FullName + "\n";
            int len = details.Length;
            for (int i = 0; i < len; i++) details += "-";
            details += "\nKörpergewicht:                            " + this.Koerpergewicht;
            details += "\nGeschlecht:                               " + this.Geschlecht;
            details += "\nGewichtsklassenbezeichnung:               " + this.Gewichtsklassenbez;
            details += "\nWilksfaktor:                              " + this.WilksFaktor;
            details += "\nGlossbrenner Bodyweight Coefficient(GBC): " + this.GBC;

            details += "\n\n";



            return details;
        }

        public Gewichtsklasse(string gender, double gew)
        {
            Geschlecht = gender;
            Koerpergewicht = gew;
            GetGewichtsklasse(gender, gew, out Gewichtsklassenbez);
            GetWilksFaktor(gender, gew, out WilksFaktor);
            GBC = APFBLF.GetGBC(gew, gender);
        }


        private void GetWilksFaktor(string gender, double Gewicht,
                       out double WilksFaktor)
        {
            WilksFaktor = 0.0;
            double kg = Gewicht;
            double x = Math.Round(kg, 1);
            double divident;
            double a; double b; double c; double d; double e; double f;
            double xh2 = Math.Pow(x, 2.0);
            double xh3 = Math.Pow(x, 3.0);
            double xh4 = Math.Pow(x, 4.0);
            double xh5 = Math.Pow(x, 5.0);
            divident = 500.0;
            if (gender == "m")
            {
                a = -216.0475144;
                b = 16.2606339;
                c = -0.002388645;
                d = -0.00113732;
                e = 7.01863E-06;
                f = -1.291E-08;
            }
            else
            {
                a = 594.31747775582;
                b = -27.23842536447;
                c = 0.82112226871;
                d = -0.0093073913;
                e = 0.00004731582;
                f = -0.00000009054;
            }

            double divisor = (a + (b * x) + (c * xh2) + (d * xh3) + (e * xh4) + (f * xh5));

            double quotient = divident / divisor;
            double quotientRounded = Math.Round(quotient, 4);

            WilksFaktor = quotientRounded;
            if (WilksFaktor < 0.0)    // wenn Formelergebnis negativ wird zb bei 250kg(w)
            {
                WilksFaktor = 0.5318; // Faktor für höchstes Gewicht einsetzen
                if(gender == "w") WilksFaktor = 0.7691;
            }
        }

        private void GetGewichtsklasse(string Gender, double Gewicht,
                        out string Gewichtsklassenbez)
        {
            Gewichtsklassenbez = string.Empty;
            string gk = string.Empty;
            if (Gender == "m")
            {
                if (Gewicht <= 53) gk = "-53";
                if (Gewicht >= 53.1 && Gewicht <= 59) gk = "-59";
                if (Gewicht >= 59.1 && Gewicht <= 66) gk = "-66";
                if (Gewicht >= 66.1 && Gewicht <= 74) gk = "-74";
                if (Gewicht >= 74.1 && Gewicht <= 83) gk = "-83";
                if (Gewicht >= 83.1 && Gewicht <= 93) gk = "-93";
                if (Gewicht >= 93.1 && Gewicht <= 105) gk = "-105";
                if (Gewicht >= 105.1 && Gewicht <= 120) gk = "-120";
                if (Gewicht >= 120.1) gk = "+120";
            }
            else if (Gender == "w")
            {
                if (Gewicht <= 43) gk = "-43";
                if (Gewicht >= 43.1 && Gewicht <= 47) gk = "-47";
                if (Gewicht >= 47.1 && Gewicht <= 52) gk = "-52";
                if (Gewicht >= 52.1 && Gewicht <= 57) gk = "-57";
                if (Gewicht >= 57.1 && Gewicht <= 63) gk = "-63";
                if (Gewicht >= 63.1 && Gewicht <= 72) gk = "-72";
                if (Gewicht >= 72.1 && Gewicht <= 84) gk = "-84";
                if (Gewicht >= 84.1) gk = "+84";
            }

            Gewichtsklassenbez = gk;
        }

    }

    
}

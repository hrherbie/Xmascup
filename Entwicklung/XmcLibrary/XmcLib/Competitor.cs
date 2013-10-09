using System.ComponentModel;

using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;


namespace XmcLib
{
    public class Competitor : IDataErrorInfo, INotifyPropertyChanged, ICloneable
    {

        // Properties  Basis --------------------------------------------------------------------------------------
        #region Properties Basis Felder

            private byte[] image;

            public byte[] Image
            {
                get { return image; }
                set
                {
                    image = value;
               
                }
            }

            public bool HasImage
            {
                get { return image != null && image.Length > 0; }
            }



       
        private string nachname;
        public string Nachname
        {
            get { return nachname; }
            set
            {
                nachname = value;
                RaisePropertyChanged("Nachname");
            }
        }


        private string vorname;
        public string Vorname
        {
            get { return vorname; }
            set
            {
                vorname = value;
                RaisePropertyChanged("Vorname");
            }
        }
        private string gender;
        public string Gender 
        {
            get { return gender; }
            set
            {
                gender = value;
                RaisePropertyChanged("Gender");
            }
        }
        private int jahrgang;
        public int Jahrgang  { 
            get { return jahrgang; }
            set
            {
                jahrgang = value;
                RaisePropertyChanged("Jahrgang");
            }
        }
        
        public int Alter { get; set; }
        public string AK { get; set; }


        /*
         * Seit dem 1. Januar 2011 
         *      Frauen: bis 43 kg nur Jugend und Junioren, 47 kg, 52 kg, 57 kg, 63 kg, 72 kg, 84 kg, 84 kg +
         *      Männer: bis 53 kg nur Jugend und Junioren, 59 kg, 66 kg, 74 kg, 83 kg, 93 kg, 105 kg, 120 kg, 120 kg+
         */


       
        private double gewicht;
        public double Gewicht
        {
            get { return gewicht; }
            set
            {
                gewicht = value;
                RaisePropertyChanged("Gewicht");
            }
        }

        public string GK { get; set; }


        public double WiFak { get; set; }

        public bool Kb { get; set; }
        public bool Bd { get; set; }
        public bool Kh { get; set; }

         #endregion Properties Basis Felder


         #region Properties Wettkampfverlaufsfelder =========================
        
        // Kniebeugen  Versuche  ----------------------------------------
        #region Kniebeugen
        public double  KbV1 { get; set; }
        public bool OkKbV1  { get; set; }
        public double  KbV2 { get; set; }
        public bool OkKbV2  { get; set; }
        public double  KbV3 { get; set; }
        public bool  OkKbV3  { get; set; }
        // Kniebeuge Ergebnis -----------------------------------
        private double _KbE;
        public double  KbE {
            get { return _KbE; }
            set
            {   bool OkE;  double E;  double EP; 
                GetErgebnisse(out OkE, out E, out EP, OkKbV1, OkKbV2,OkKbV3,   KbV1, KbV2, KbV3);
                OkKbE = OkE;
                _KbE = E;
                KbEP = EP;
            }
        }
        public bool OkKbE { get; set; }
        public double KbEP { get; set; }
        

        #endregion Kniebeugen ------------------------------------------------


        // Benchpress Versuche  ----------------------------------------
        #region Benchpress
        public double BdV1 { get; set; }
        public bool OkBdV1 { get; set; }
        public double BdV2 { get; set; }
        public bool OkBdV2 { get; set; }
        public double BdV3 { get; set; }
        public bool OkBdV3 { get; set; }
        // Benchpress Ergebnis -----------------------------------
        private double _BdE;
        public double BdE
        {
            get { return _BdE; }
            set
            {
                bool OkE; double E; double EP;
                GetErgebnisse(out OkE, out E, out EP, OkBdV1, OkBdV2, OkBdV3, BdV1, BdV2, BdV3);
                OkBdE = OkE;
                _BdE = E;
                BdEP = EP;
            }
        }
        public bool OkBdE { get; set; }
        public double BdEP { get; set; }
       

        #endregion Benchpress ------------------------------------------------


        // Deadlift Versuche  ----------------------------------------
        #region Deadlift
        public double KhV1 { get; set; }
        public bool OkKhV1 { get; set; }
        public double KhV2 { get; set; }
        public bool OkKhV2 { get; set; }
        public double KhV3 { get; set; }
        public bool OkKhV3 { get; set; }
        // Deadlift Ergebnis -----------------------------------
        private double _KhE;
        public double KhE
        {
            get { return _KhE; }
            set
            {
                bool OkE; double E; double EP;
                GetErgebnisse(out OkE, out E, out EP, OkKhV1, OkKhV2, OkKhV3, KhV1, KhV2, KhV3);
                OkKhE = OkE;
                _KhE = E;
                KhEP = EP;
            }
        }

        public bool OkKhE { get; set; }
        public double KhEP { get; set; }
       



        #endregion Deadlift ------------------------------------------------
        // Platzierungen ========================================================================

        public string Platz_E_Kb { get; set; }
        public string Platz_E_Bd { get; set; }
        public string Platz_E_Kh { get; set; }

        public string Platz_Zk_BdKh { get; set; }
        public string Platz_Zk_KbKh { get; set; }
        public string Platz_Zk_KbBd { get; set; }
       
        public string Platz_Dk { get; set; }


        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Nachname":

                        if (this.nachname == null || this.nachname.Length < 2 || this.nachname.Length > 30)
                        {
                            result = String.Format("Nachname darf nicht kleiner als {0} oder größer als {1} sein.",
                               2, 30);
                        }
                        break;

                    case "Vorname":

                        if (this.vorname == null || this.vorname.Length < 2 || this.vorname.Length > 25)
                        {
                            result = String.Format("Vorname darf nicht kleiner als {0} oder größer als {1} sein.",
                               2, 25);
                        }
                        break;

                    case "Jahrgang":
                        /*
                        DateTime now = DateTime.Now;
                        if (this.jahrgang == 0 || this.jahrgang < 1921 || this.jahrgang > (now.Year - 14))
                        {
                            result = String.Format("Jahrgang muß größer als 1920 und kleiner als {0} sein.",
                              now.Year - 14);
                        }
                        */
                         int now = 2012;
                        if (this.jahrgang == 0 || this.jahrgang < 1921 || this.jahrgang > (now - 14))
                        {
                            result = String.Format("Jahrgang muß größer als 1920 und kleiner als {0} sein.",
                              now - 14);
                        }
                        break;

                    case "Gender":


                        if (this.gender == "m" || this.gender == "w")
                        {
                            ;
                        }
                        else
                        {
                            result = String.Format("Geschlecht muß mit {0} (männlich) oder {1} (weiblich) eingegeben werden",
                              "m", "w");
                        }
                        break;

                    case "Gewicht":

                        double gew = this.gewicht;

                        if (this.gewicht != 0.0 && ( this.gewicht < 50.0 || this.gewicht > 150.0))
                        {
                            result = String.Format("Gewicht muß größer als 50 und kleiner als {0} kg sein.",
                              160);
                        }
                        break;
                }
               

              
                
                return result;
            }
        }

      

        #endregion Properties
        public object Clone()
        {
            return this.MemberwiseClone();
        }
   
        private void GetErgebnisse(out bool OkE, out double E, out double EP,
               bool OkV1, bool OkV2, bool OkV3, double V1, double V2, double V3)
        {

            OkE = false; E = 0; EP = 0;
            double wert1 = 0; double wert2 = 0; double wert3 = 0;
            if (OkV1 == true)
            {
                OkE = true;
                wert1 = Convert.ToDouble(V1);
            }
            if (OkV2 == true)
            {
                OkE = true;
                wert2 = Convert.ToDouble(V2);
            }
            if (OkV3 == true)
            {
                OkE = true;
                wert3 = Convert.ToDouble(V3);
            }
            if (OkE == true)
            {
                double[] mwa = { wert1, wert2, wert3 };
                E = mwa.Max();
                EP = Math.Round(E * WiFak, 2); ;
               
            }
        }



        #region ProperyChanged Handlers

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       
        private void Changed(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
           // //MessageBox.Show("OnPropertyChanged erreicht !");
            if (PropertyChanged != null)
                PropertyChanged(this, e);

        }
  
        #endregion

        #region Unterprogramme zur Kalulation von Werten


        public void MakeAkAndAge(int Jahrgang, out int alter, out string ak )
        {
            ak = string.Empty;
            alter = 0;
            if (Jahrgang != 0)
            {
              /*  DateTime currentDate = DateTime.Now;
                int currYear = currentDate.Year; */
                int currYear = 2012;
                int jg = Convert.ToInt32(Jahrgang);
                int diff = currYear - jg;
                alter = diff;
                string _AK = string.Empty;

                if (diff >= 14 && diff <= 18) _AK = "Jugend";
                if (diff >= 14 && diff <= 15) _AK = "Jugend B";
                if (diff >= 16 && diff <= 18) _AK = "Jugend A";
                if (diff >= 19 && diff <= 23) _AK = "Junior";
                if (diff >= 24 && diff <= 39) _AK = "Allgem";
                if (diff >= 40 && diff <= 49) _AK = "Masters 1";
                if (diff >= 50 && diff <= 59) _AK = "Masters 2";
                if (diff >= 60 && diff <= 69) _AK = "Masters 3";
                if (diff >= 70) _AK = "Masters 4";

                ak = _AK;
            }
        }



        public void GetGewichtsklasseAndWifak(string Gender, double Gewicht, out string gk, out double wifak)
        {
            gk = string.Empty;

            wifak = 0;
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
            //   WilksFactor wf = xmc.DataAccess.WilksFactory.GetWilksfactor(Gewicht);
            if (Gender == "m") wifak = GetWifak(Gewicht, "m");
            if (Gender == "w") wifak = GetWifak(Gewicht, "w");

        }

        public static double GetWifak(double kg, string gender)
        {
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

            return quotientRounded;
        }
   

        #endregion

    }
}

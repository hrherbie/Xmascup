using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmcLib
{
    public class Disziplin
    {

        public string gewBp { get; set; }
        public string gewDl { get; set; }
        public string gewSq { get; set; }


        // _g = deutsch _e = englisch
        // _l = Langname _s = Kurzbezeichnung
        public  const string bp_l_g = "Bankdrücken";
        public  const string bp_l_e = "Benchpress";
        public  const string bp_s_g = "Bd.";
        public  const string bp_s_e = "Bp.";
        public  const string dl_l_g = "Kreuzheben";
        public  const string dl_l_e = "Deadlift";
        public  const string dl_s_g = "Kh.";
        public  const string dl_s_e = "Dl.";
        public  const string sq_l_g = "Kniebeugen";
        public  const string sq_l_e = "Squat";
        public  const string sq_s_g = "Kb.";
        public  const string sq_s_e = "Sq.";

        public Disziplin(string sq, string bp, string dl)
        {
            gewBp = bp;
            gewSq = sq;
            gewDl = dl;
       
        }
        public override string ToString()
        {
            string diszis = string.Empty;
            diszis += "Gewählte Disziplinen\n--------------------";
            diszis += "\n" + sq_l_g + " - " + gewSq;
            diszis += "\n" + bp_l_g +  " - " + gewBp;
            diszis += "\n" + dl_l_g +  " - " + gewDl;
            diszis += "\n\n\n";
            return diszis;
        }
    }
}

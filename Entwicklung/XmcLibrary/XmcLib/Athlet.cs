using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmcLib;

namespace XmcTstCon
{
    public class Athlet
    {
        public Person ps;
        public Altersklasse ak;
        public Gewichtsklasse gk;
        public Disziplin gewDisziplin;

        public Athlet(Person PS, Altersklasse AK, Gewichtsklasse GK, 
            Disziplin DP)
        {
            ps = PS;
            ak = AK;
            gk = GK;
            gewDisziplin = DP;
        }

        public Athlet(
            string nachname, string vorname,
            DateTime wk,DateTime geburtstag, 
            string gender, double gewicht,
            string bp, string sq, string dl
            )
        {
            ps = new Person(nachname, vorname);
            ak = new Altersklasse(geburtstag,wk);
                              
            gk = new Gewichtsklasse(gender, gewicht);
            gewDisziplin = new Disziplin(sq: sq,bp: bp,dl: dl);
        }
        public override string ToString()
        {
            string details = string.Empty + "\n" +
                "=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-" +
                "\n";
            details += this.GetType().FullName + "\n";
            details +=  
                "=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-" +
                "\n";
            details += "\n";
            return details + ps.ToString() + ak.ToString() + gk.ToString()
                + gewDisziplin +
                 "====================================================" +
                "\n";
        }
    }
}

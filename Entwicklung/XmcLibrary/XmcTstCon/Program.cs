using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using XmcLib;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Win32;



namespace XmcTstCon
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime Geburtstag = new DateTime(1941, 11, 3);
            DateTime WkDatum = DateTime.Now;
            Person ps1 = new Person("Heimburger-Reibold", "Herbert");
            Altersklasse ak1 = new Altersklasse(Geburtstag, WkDatum);
            Altersklasse ak2 = new Altersklasse(
                new DateTime(1951,5,3),
                new DateTime(2013, 9, 25));
            Altersklasse ak3 = new Altersklasse(
              new DateTime(1971, 11, 3),
              new DateTime(2013, 9, 25));
            Console.WriteLine(ak1);
            Console.WriteLine(ak2);
            Console.WriteLine(ak3);


            Gewichtsklasse gk1 = new Gewichtsklasse("m",40.52);
            Gewichtsklasse gk2 = new Gewichtsklasse("m", 30);
            Gewichtsklasse gk3 = new Gewichtsklasse("w", 40.52);

            Console.WriteLine(gk1);
            Console.WriteLine(gk2);
            Console.WriteLine(gk3);

            double tstGew = 40.52;
            double gbcMen = APFBLF.GetGBC(tstGew,"m");
            double gbcWomen = APFBLF.GetGBC(tstGew, "w");
            Console.WriteLine(
                String.Format("Gewicht = {0} GBC-MEN = {1} GBC-WOMEN = {2}",
                tstGew, gbcMen, gbcWomen));
           // Disziplin diszi1 = new Disziplin(sq: "0", dl: "0", bp: "0");
            Athlet atl1 = new Athlet("Heimburger-Reibold", "Herbert",
                WkDatum, Geburtstag, "m", 86.5,"0","0","0");
            Disziplin diszi3 = new Disziplin(sq: "0", dl: "0", bp: "0");
            Athlet atl3 = new Athlet(ps1, ak3, gk3, diszi3);
            Console.WriteLine(atl1);
            Console.WriteLine(atl3);

            
           
            XmlWithCompetitors xml = new XmlWithCompetitors();

    
            
            String xmldatei2011 = @"K:\XMASCUPTESTDATEN\Teilnehmer2011.xml";
            ObservableCollection<Competitor> oc2011 = new
            ObservableCollection<Competitor>();
            oc2011 =
                xml.LoadCompetitorsFromXml(xmldatei2011);
            foreach (var c in oc2011)
            {
                Console.WriteLine(c.Nachname + ", "+ c.Vorname);
            }
            String xmldatei2012 = @"K:\XMASCUPTESTDATEN\Teilnehmer2014.xml";
            ObservableCollection<Competitor> oc2012 = new
            ObservableCollection<Competitor>();
            oc2012 =
                xml.LoadCompetitorsFromXml(xmldatei2012);
            foreach (var c in oc2012)
            {
                Console.WriteLine(c.Nachname + ", " + c.Vorname + "  AK: " + c.AK);
            }
            Console.WriteLine("Ende");
        }
        
       
    }
}

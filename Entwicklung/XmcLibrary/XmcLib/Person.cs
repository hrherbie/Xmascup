using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmcLib
{
    public class Person
    {
        public string Nachname { get; set; }
        public string Vorname { get;set;}


        public Person(string nachname, string vorname)
        {
            Nachname = nachname;
            Vorname = vorname;
        }

        public override string ToString()
        {
            string details = string.Empty;
            details += this.GetType().FullName + "\n";
            int len = details.Length;
            for (int i = 0; i < len; i++) details += "-";
            details += "\nVorname:  " + Vorname + "\n";
            details += "Nachname: " + Nachname + "\n";
            
           
            return  details + "\n";
        }
    }
 
}

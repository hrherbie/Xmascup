using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Text;

using System.Windows;


namespace XmcGetCompetitors
{
    ///=================================================================================================
    /// <summary>   Competitors. </summary>
    ///
    /// <remarks>   Beate, 15.11.2012. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class Competitors : ObservableCollection<Competitor>
    {
        private static ObservableCollection<Competitor> competitors = 
            new ObservableCollection<Competitor>();
        
        
        static Competitors()
        {
            /*
            competitors.Add(new Competitor { Nachname = "Klug", Vorname = "Beate", Jahrgang = 1957, Gender = "w", Gewicht = 65, Kb=true, Bd=true, Kh=true });
            competitors.Add(new Competitor { Nachname = "Heimburger", Vorname = "Herbert", Jahrgang = 1941, Gender = "m", Gewicht = 75 });
            competitors.Add(new Competitor { Nachname = "Amann", Vorname = "AHerbert", Jahrgang = 1914, Gender = "m", Gewicht = 85 });
            competitors.Add(new Competitor { Nachname = "Zmann", Vorname = "ZHerbert", Jahrgang = 1990, Gender = "m", Gewicht = 95 });
             * 
             */
        }

        public static ObservableCollection<Competitor> GetCompetitors()
        {
            return competitors;
        }



        public static bool DeleteCompetitor(Competitor c)
        {
            competitors.Remove(c);
            return true;
        }

        public static bool AddCompetitor(Competitor c)
        {
            List<Competitor> al = competitors.ToList();
            foreach(var item in al)
            {
                if(item.Nachname == c.Nachname && c.Vorname == item.Vorname)
                {
                     //MessageBox.Show(c.Nachname + ", " + c.Vorname + " bereits enthalten");
                    return false;
                }
            }
           
            competitors.Add(c);
            return true;
        }

        ///=================================================================================================
        /// <summary>   Saves the competitors to xml. </summary>
        ///
        /// <remarks>   Beate, 15.11.2012. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool SaveCompetitorsToXml()
        {

            var atl1 = from item in competitors
                       where item.Nachname != string.Empty
                       select item;


            DateTime fileTime = DateTime.Now;
            StringBuilder xmlStr = new StringBuilder();
            xmlStr.Append("<Competitors Jahr=\"");
            xmlStr.Append(fileTime.Year.ToString());
            xmlStr.Append("\">\n");

            foreach (Competitor a in atl1)
            {
                GetFieldNamesOfCompetitor(xmlStr, a);
            }
            
            xmlStr.Append("</Competitors>\n");
           
         //   string fd = fileTime.ToShortDateString();
            string fd = fileTime.Year.ToString() + fileTime.Month.ToString() + fileTime.Day.ToString();
            string ft = fileTime.ToString();
            ft = ft.Replace(':', '.');
            string h = fileTime.Hour.ToString();
            string m = fileTime.Minute.ToString();
            string s = fileTime.Second.ToString();
            ft = h + m + s;
            string fileNameToSave = "../../competitorArchiv/Competitors-" + fd + "-" + ft + ".xml";
            string fileNameInWork = "Competitors.xml";
           
            try
            {
                File.Copy(fileNameInWork, fileNameToSave);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
            using (StreamWriter wXml = File.CreateText(fileNameInWork))
            {
                string xxx = xmlStr.ToString();
                wXml.WriteLine(xxx);
            }

            return true;
        }

        ///=================================================================================================
        /// <summary>   Gets a field names of competitor. </summary>
        ///
        /// <remarks>   Beate, 15.11.2012. </remarks>
        ///
        /// <param name="xmlStr">   The xml string. </param>
        /// <param name="a">        The Competitor to process. </param>
        ///-------------------------------------------------------------------------------------------------

        private static void GetFieldNamesOfCompetitor(StringBuilder xmlStr, Competitor a)
        {

            Type myType2 = a.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType2.GetProperties());

            xmlStr.Append("\t<Competitor>\n");
            foreach (PropertyInfo prop in props)
            {
                string pn = prop.Name;
                if (pn == "Item") continue;  // wegen der "public virtual string Error" Methode
                object propValue = prop.GetValue(a, null);

                // Do something with propValue
                xmlStr.Append("\t\t<" + prop.Name + ">" + propValue + "</" + prop.Name + ">\n");
            }
            xmlStr.Append("\t</Competitor>\n");
        }

        ///=================================================================================================
        /// <summary>   Loads the competitors from xml. </summary>
        ///
        /// <remarks> 
        /// </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool LoadCompetitorsFromXml()
        {
            string xml = string.Empty;
            try
            {
             //   //MessageBox.Show("LoadCompetitorsFromXml");
                xml = File.ReadAllText("Competitors.xml");

            }
            catch (FileNotFoundException fnfEx)
            {
                MessageBox.Show(fnfEx.Message);

                StringBuilder xmlStr = new StringBuilder();
                string jahr = (DateTime.Now.Year).ToString();


                xmlStr.Append("<Competitors Jahr=\"");
                xmlStr.Append(jahr);
                xmlStr.Append("\">\n");

                xmlStr.Append("</Competitors>\n");
                string fileNameInWork = "Competitors.xml";
               
                using (StreamWriter wXml = File.CreateText(fileNameInWork))
                {
                    string xxx = xmlStr.ToString();
                    wXml.WriteLine(xxx);
                   
                }
                xml = File.ReadAllText("Competitors.xml");
            }

              competitors.Clear();

                XElement elem = XElement.Parse(xml);
                var comps = from comp in elem.Elements("Competitor")
                            select comp;

                foreach (var item in comps)
                {
                    Competitor a = new Competitor();
                    a.Nachname = item.Element("Nachname").Value;
                    a.Vorname = item.Element("Vorname").Value;
                    a.Jahrgang = Convert.ToInt32(item.Element("Jahrgang").Value);
                    a.Gender = item.Element("Gender").Value;
                    a.AK = item.Element("AK").Value;
                    a.Alter = Convert.ToInt32(item.Element("Alter").Value);
                    a.Gewicht = Convert.ToDouble(item.Element("Gewicht").Value);
                    a.GK = item.Element("GK").Value;
                    a.WiFak = Convert.ToDouble(item.Element("WiFak").Value);
                    a.Kb = Convert.ToBoolean(item.Element("Kb").Value);
                    a.Bd = Convert.ToBoolean(item.Element("Bd").Value);
                    a.Kh = Convert.ToBoolean(item.Element("Kh").Value);
                    // Versuchsfelder Kniebeugen --------------------------------------
                    a.KbV1 = Convert.ToDouble(item.Element("KbV1").Value);
                    a.KbV2 = Convert.ToDouble(item.Element("KbV2").Value);
                    a.KbV3 = Convert.ToDouble(item.Element("KbV3").Value);
                    a.OkKbV1 = Convert.ToBoolean(item.Element("OkKbV1").Value);
                    a.OkKbV2 = Convert.ToBoolean(item.Element("OkKbV2").Value);
                    a.OkKbV3 = Convert.ToBoolean(item.Element("OkKbV3").Value);
                    a.KbE = Convert.ToDouble(item.Element("KbE").Value);

                    // VersuchsfelderBankdrücken  --------------------------------------
                    a.BdV1 = Convert.ToDouble(item.Element("BdV1").Value);
                    a.BdV2 = Convert.ToDouble(item.Element("BdV2").Value);
                    a.BdV3 = Convert.ToDouble(item.Element("BdV3").Value);
                    a.OkBdV1 = Convert.ToBoolean(item.Element("OkBdV1").Value);
                    a.OkBdV2 = Convert.ToBoolean(item.Element("OkBdV2").Value);
                    a.OkBdV3 = Convert.ToBoolean(item.Element("OkBdV3").Value);
                    a.BdE = Convert.ToDouble(item.Element("BdE").Value);

                     // Versuchsfelder Kreuzheben --------------------------------------
                    a.KhV1 = Convert.ToDouble(item.Element("KhV1").Value);
                    a.KhV2 = Convert.ToDouble(item.Element("KhV2").Value);
                    a.KhV3 = Convert.ToDouble(item.Element("KhV3").Value);
                    a.OkKhV1 = Convert.ToBoolean(item.Element("OkKhV1").Value);
                    a.OkKhV2 = Convert.ToBoolean(item.Element("OkKhV2").Value);
                    a.OkKhV3 = Convert.ToBoolean(item.Element("OkKhV3").Value);
                    a.KhE = Convert.ToDouble(item.Element("KhE").Value);

                    // Ende Versuchsfelder ----------------------------------------------
                    a.Platz_Dk = item.Element("Platz_Dk").Value;
                    a.Platz_Zk_BdKh = item.Element("Platz_Zk_BdKh").Value;
                    a.Platz_Zk_KbKh = item.Element("Platz_Zk_KbKh").Value;
                    a.Platz_Zk_KbBd = item.Element("Platz_Zk_KbBd").Value;
                    a.Platz_E_Bd = item.Element("Platz_E_Bd").Value;

                    // -------------------------------------------------------------------
                    competitors.Add(a);     // Competitor in Collection bringen
                }


               
            return true;
        }

    } // Ende class competitors

}  


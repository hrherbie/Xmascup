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
using System.Windows.Forms;

using System.Windows;

namespace XmcLib
{
    public  class XmlWithCompetitors
    {
        private static ObservableCollection<Competitor> competitors = 
            new ObservableCollection<Competitor>();

        /**
         * <summary>Loads the competitors from xml.</summary>
         *
         * <remarks>Beate, 09.10.2013.</remarks>
         *
         * <param name="xmldatei">The xmldatei.</param>
         *
         * <returns>The competitors from xml.</returns>
         */

        public ObservableCollection<Competitor>
            LoadCompetitorsFromXml(string xmldatei)
        {
            string zulesendedatei = xmldatei;
            string xml = string.Empty;
            try
            {
                //   //MessageBox.Show("LoadCompetitorsFromXml");
                xml = File.ReadAllText(zulesendedatei);

            }
            catch (FileNotFoundException fnfEx)
            {
                // Erstellen neue leere "Competitor.xml" Datei
                StringBuilder xmlStr = new StringBuilder();
                string jahr = (DateTime.Now.Year).ToString();
             
                xmlStr.Append("<Competitors Jahr=\"");
                xmlStr.Append(jahr);
                xmlStr.Append("\">\n");
                xmlStr.Append("</Competitors>\n");


                xml = xmlStr.ToString();
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



            return competitors;
        }

    }
}

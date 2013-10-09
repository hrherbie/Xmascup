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
        ///===============================================================
        /// <summary>   Loads the competitors from xml. </summary>
        ///
        /// <remarks> 
        /// Betrifft die zurzeit benutzte fixe Jahreszahl '2012':
        ///     Falls das Programm  ausgeführt wird erhalten die
        ///     Dateien 'innen' 2012 und im Dateinamen das gerade lfd Jahr
        /// </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///---------------------------------------------------------------

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
                MessageBox.Show(fnfEx.Message);

                StringBuilder xmlStr = new StringBuilder();
                // string jahr = (DateTime.Now.Year).ToString();
                string jahr = "2012";

                string NoNameCompetitor = @"<Competitor>
<Image></Image>
		<HasImage>False</HasImage>
		<Nachname>nn</Nachname>
		<Vorname>nn</Vorname>
		<Gender>nn</Gender>
		<Jahrgang>0</Jahrgang>
		<Alter>0</Alter>
		<AK>unbekannt</AK>
		<Gewicht>0</Gewicht>
		<GK>unbekannt</GK>
		<WiFak>0</WiFak>
		<Kb>False</Kb>
		<Bd>False</Bd>
		<Kh>False</Kh>
		<KbV1>0</KbV1>
		<OkKbV1>False</OkKbV1>
		<KbV2>0</KbV2>
		<OkKbV2>False</OkKbV2>
		<KbV3>0</KbV3>
		<OkKbV3>False</OkKbV3>
		<KbE>0</KbE>
		<OkKbE>False</OkKbE>
		<KbEP>0</KbEP>
		<BdV1>0</BdV1>
		<OkBdV1>False</OkBdV1>
		<BdV2>0</BdV2>
		<OkBdV2>False</OkBdV2>
		<BdV3>0</BdV3>
		<OkBdV3>False</OkBdV3>
		<BdE>0</BdE>
		<OkBdE>False</OkBdE>
		<BdEP>0</BdEP>
		<KhV1>0</KhV1>
		<OkKhV1>False</OkKhV1>
		<KhV2>0</KhV2>
		<OkKhV2>False</OkKhV2>
		<KhV3>0</KhV3>
		<OkKhV3>False</OkKhV3>
		<KhE>0</KhE>
		<OkKhE>False</OkKhE>
		<KhEP>0</KhEP>
		<Platz_E_Kb></Platz_E_Kb>
		<Platz_E_Bd></Platz_E_Bd>
		<Platz_E_Kh></Platz_E_Kh>
		<Platz_Zk_BdKh></Platz_Zk_BdKh>
		<Platz_Zk_KbKh></Platz_Zk_KbKh>
		<Platz_Zk_KbBd></Platz_Zk_KbBd>
		<Platz_Dk></Platz_Dk>
		<Error></Error>
</Competitor>
";

                xmlStr.Append("<Competitors Jahr=\"");
                xmlStr.Append(jahr);
                xmlStr.Append("\">\n");
                xmlStr.Append(NoNameCompetitor);
                xmlStr.Append("</Competitors>\n");
                string fileNameInWork = zulesendedatei;

                using (StreamWriter wXml = File.CreateText(fileNameInWork))
                {
                    string xxx = xmlStr.ToString();
                    wXml.WriteLine(xxx);

                }
                xml = File.ReadAllText(zulesendedatei);
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

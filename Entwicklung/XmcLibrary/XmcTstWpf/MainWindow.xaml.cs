using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

// eigene Bibliotheken
using XmcLib;


namespace XmcLibTstWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox myTb;
        public MainWindow()
        {
            InitializeComponent();
            OpenFile();
            Testausgaben();


        }

        /**
         * <summary>Zeigt OpenFileDialog an</summary>
         *
         * <remarks>Beate, 09.10.2013.</remarks>
         */

        private static void OpenFile()
        {
            string[] allfiles = { };
            List<string> filenameOnly = new List<string>();
            OpenFileDialog inputDlg = new OpenFileDialog();
            inputDlg.Multiselect = true;
            inputDlg.InitialDirectory = @"";
            inputDlg.Filter = "Xmascup Competitor Dateien|Competitor*.xml";

            if (inputDlg.ShowDialog().Value)
            {
                allfiles = inputDlg.FileNames;
                foreach (string s in allfiles)
                {
                    string filename = System.IO.Path.GetFileName(s);
                    filenameOnly.Add(filename);

                }
                MessageBox.Show("Eingabe von  "+allfiles[0].ToString());
            }
        }

        private void Testausgaben()
        {
            myTb = (TextBox)this.FindName("tbHrtext");
            myTb.Text = "\nA n f a n g";
            DateTime Geburtstag = new DateTime(1941, 11, 3);
            DateTime WkDatum = DateTime.Now;
            Altersklasse ak1 = new Altersklasse(Geburtstag, WkDatum);
           
            Altersklasse ak2 = new Altersklasse(
                new DateTime(1951, 5, 3),
                new DateTime(2013, 9, 25));
            
            Altersklasse ak3 = new Altersklasse(
              new DateTime(1971, 11, 3),
              new DateTime(2013, 9, 25));
            myTb.Text += "\n\nak1 - " + ak1.ToString();
            myTb.Text += "\n\nak2 - " + ak2.ToString();
            myTb.Text += "\n\nak3 - " + ak3.ToString();
            myTb.Text += "\n\nE n d e";
        }

      

    }
}

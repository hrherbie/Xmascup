using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace XmcGetCompetitors
{
    /// <summary>
    /// Interaction logic for LadenOderSpeichernDerDateien.xaml
    /// </summary>

    public partial class LadenOderSpeichernDerDateien : System.Windows.Window
    {

        static public string sv = String.Empty;

        static public string HoleGewaehlteDatei()
        {
            return sv;
        }


        public LadenOderSpeichernDerDateien()
        {
            InitializeComponent();
        }


        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {
             myDialog = new ();
            string Bilder = "Bilder(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            string Teilnehmer = "Teilnehmerdateien (TeilnehmerJJJJ.xml)|Teilnehmer*.xml";
            myDialog.Filter = Teilnehmer + "|" + Bilder;
                
              
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;

            if (myDialog.ShowDialog() == true)
            {
                lstFiles.Items.Clear();
                foreach (string file in myDialog.FileNames)
                {
                    lstFiles.Items.Add(file);
                }                
            }
        }
        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog myDialog = new SaveFileDialog();

            if (myDialog.ShowDialog() == true)
            {
                lstFiles.Items.Clear();
                foreach (string file in myDialog.FileNames)
                {
                    lstFiles.Items.Add(file);
                }
            }
        }

        private void  lstFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cnt = e.AddedItems.Count;
            sv = lstFiles.SelectedValue.ToString(); 
            if (cnt != 0)
                MessageBox.Show(e.AddedItems[0].ToString());
           
            
        }

    }
}
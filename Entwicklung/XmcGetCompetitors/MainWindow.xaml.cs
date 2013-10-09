using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;



namespace XmcGetCompetitors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           
        }

        

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the current button.
            Button cmd = (Button)e.OriginalSource;
            // Umwandeln Buttonbeschriftung in ausführbares Cmd
            if ((string)cmd.Content == "Laden oder Speichern der Dateien")
            {
                // Erzeugen Commandtext zur Ausführung
                cmd.Content = "LadenOderSpeichernDerDateien";
            }
            // Create an instance of the window named
            // by the current button.
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            Window win = (Window)assembly.CreateInstance(
                type.Namespace + "." + cmd.Content);

            // Show the window.
            win.ShowDialog();
            // Wiederherstellen der Buttonbeschriftung
            cmd.Content = "Laden oder Speichern der Dateien";

            
        }
    }
}

using Notenmanager.ViewModel;
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
using System.Windows.Shapes;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für UnterrichtsfachAnlegen.xaml
    /// </summary>
    public partial class UnterrichtsfachBearbeitenWindow : Window
    {
        private DialogMode _dm;
        public UnterrichtsfachBearbeitenWindow(DialogMode dm)
        {
            InitializeComponent();
            this._dm = dm;

            this.Loaded += UnterrichtsfachAnlegen_Loaded;
            
        }

        private void UnterrichtsfachAnlegen_Loaded(object sender, RoutedEventArgs e)
        {
            if (_dm == DialogMode.Neu)
                Title = "Unterrichtsfach anlegen";
            else
                Title = "Unterrichtsfach ändern";

            this.btnSpeichern.Click += BtnSpeichern_Click;
            this.btnAbbrechen.Click += BtnAbbrechen_Click;
        }

        private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void txtPos_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}

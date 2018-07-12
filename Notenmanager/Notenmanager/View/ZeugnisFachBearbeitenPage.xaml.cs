using Notenmanager.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für FachAnlegenPage.xaml
    /// </summary>
    public partial class FachAnlegenPage : Page
    {
        private ZeugnisFach _viewmodel;
        public FachAnlegenPage()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _viewmodel = FindResource("ZFBearbeitenVM") as ZeugnisFach;

            this.comboxFachart.Items.Clear();
            foreach (Fachart a in Enum.GetValues(typeof(Fachart)))
                this.comboxFachart.Items.Add(a);

            _viewmodel.UFADialogRequest += OnUFADialogRequest;
        }

        private void OnUFADialogRequest(object sender, DialogEventArgs e)
        {
            UnterrichtsfachAnlegen dlg = new UnterrichtsfachAnlegen(e.dm);

            if (e.ResultAction != null)
                e.ResultAction(dlg.ShowDialog());
        }

        private void btnHinzufuegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEntfernen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAnlegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAendern_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
using System.Windows.Shapes;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für LehrerAuswahlWindow.xaml
    /// </summary>
    public partial class LehrerAuswahlWindow : Window
    {
        private LehrerAuswahlWindowVM _viewmodel;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _viewmodel = FindResource("LehrerAuswahlVM") as LehrerAuswahlWindowVM;
        }

        public LehrerAuswahlWindow(Unterrichtsfach selFach)
        {
            InitializeComponent();

            _viewmodel.SelUF = selFach;
            this.Loaded += LehrerAuswahlWindow_Loaded;
        }

        private void LehrerAuswahlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnHinzufuegen.Click += BtnHinzufuegen_Click;
            this.btnAbbrechen.Click += BtnAbbrechen_Click;
        }

        private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void BtnHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            UFachLehrer ufl = new UFachLehrer();
            ufl.Lehrer = _viewmodel.SelectedLehrer;
            ufl.Unterrichtsfach = _viewmodel.SelUF;
            ufl.Speichern();

            this.DialogResult = true;
            Close();
        }
    }
}

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
        public LehrerAuswahlWindow()
        {
            InitializeComponent();

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
            this.DialogResult = true;
            Close();
        }
    }
}

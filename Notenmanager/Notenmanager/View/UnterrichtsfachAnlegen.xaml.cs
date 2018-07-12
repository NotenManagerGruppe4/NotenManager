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
    public partial class UnterrichtsfachAnlegen : Window
    {
        DialogMode dm;
        private FachAnlegenPageVM vm;
        public UnterrichtsfachAnlegen(DialogMode dm)
        {
            InitializeComponent();
            this.dm = dm;
            this.Loaded += UnterrichtsfachAnlegen_Loaded;
            

        }

        private void UnterrichtsfachAnlegen_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm = FindResource("FAnlegenVM") as FachAnlegenPageVM;

            this.btnSpeichern.Click += BtnSpeichern_Click;
            this.btnAbbrechen.Click += BtnAbbrechen_Click;

            this.txtBezeichnung.KeyUp += Txt_KeyUp;
            this.txtPos.KeyUp += Txt_KeyUp;
            this.txtStunden.KeyUp += Txt_KeyUp;
        }

        private void Txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (vm.OnUFachEditCmd.CanExecute(null))
                vm.OnUFachEditCmd.Execute(null);
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

    }
}

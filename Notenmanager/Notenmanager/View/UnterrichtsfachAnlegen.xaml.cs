﻿using Notenmanager.ViewModel;
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
        private UnterrichtsfachAnlegenVM vm;
        public UnterrichtsfachAnlegen(DialogMode dm)
        {
            InitializeComponent();
            this.dm = dm;
            this.Loaded += UnterrichtsfachAnlegen_Loaded;
            

        }

        private void UnterrichtsfachAnlegen_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm = FindResource("UFAnlegenVM") as UnterrichtsfachAnlegenVM;


            this.Closing += UnterrichtsfachAnlegen_Closing;

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


        private void UnterrichtsfachAnlegen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult != true && vm.OnCancelCmd.CanExecute(null))
                vm.OnCancelCmd.Execute(null);
        }
    }
}

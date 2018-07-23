﻿using Notenmanager.Model;
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
   /// Interaktionslogik für LeistungsEditor.xaml
   /// </summary>
   public partial class LeistungsEditor : Window
   {

      private LeistungsEditorVM _vm;
      public LeistungsEditor(DialogMode mode, Leistung l, Leistungsart la = null, Unterrichtsfach uf = null, Klasse k = null)
      {
         InitializeComponent();

         _vm = FindResource("LeistungsEditorVM") as LeistungsEditorVM;
         _vm.SetMode(mode,la,uf,k);

         
         if(l != null)
            _vm.CLeistung = l;

         _vm.CloseAfterSaveRequesting += (s, e) =>
         {
            this.Close();
         };

         this.Closing += LeistungsEditor_Closing;
      }

      private void LeistungsEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (_vm.BeendenCMD.CanExecute(null))
            _vm.BeendenCMD.Execute(null);
      }

      private void btnExit_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }

   }
}

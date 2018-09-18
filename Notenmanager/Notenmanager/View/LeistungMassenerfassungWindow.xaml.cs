using Notenmanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
   /// Interaktionslogik für LeistungMassenerfassungWindow.xaml
   /// </summary>
   public partial class LeistungMassenerfassungWindow : Window
   {
      private LeistungMassenerfassungWindowVM _vm;
      public LeistungMassenerfassungWindow()
      {
         InitializeComponent();
         _vm = DataContext as LeistungMassenerfassungWindowVM;

         NotenPageVM _parentvm = FindResource("NotenPageVM") as NotenPageVM;
         if (_parentvm == null)
            throw new Exception("NotenPageVM nicht gefunden!");
         _vm.Init(_parentvm.CurrentKlasse);

         EventHandler<Model.MessageBoxEventArgs> msghandler = OnMessageBoxRequest;
         _vm.MessageBoxRequest += msghandler;

         EventHandler closer = null;
         _vm.CloseAfterSaveRequesting += closer = (s, e) =>
         {
            this.Close();
         };
         //Eventhandler abtrennen (sonst falls Fenster 2 mal hintereinander geöffnet und geschlossen --> beim 3ten mal 3 MessageBoxen die auf Event reagieren und aufgehen!)
         this.Closing += (s, e) =>
         {
            _vm.MessageBoxRequest -= msghandler;
            _vm.CloseAfterSaveRequesting -= closer;
         };

         dgCbNote.ItemsSource = _vm.Notenstufen;
      }

      private void OnMessageBoxRequest(object sender, Model.MessageBoxEventArgs e)
      {
         MessageBoxResult result = MessageBox.Show(e.MessageBoxText, e.Caption, e.MessageBoxButton, e.MessageBoxImage);
         e.ResultAction?.Invoke(result);
      }

      private void btnExit_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }
   }
}

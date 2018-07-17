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
      private ZeugnisFachBearbeitenPageVM _viewmodel;
      public FachAnlegenPage()
      {
         InitializeComponent();
      }
      private void Grid_Loaded(object sender, RoutedEventArgs e)
      {
         _viewmodel = FindResource("ZFBearbeitenVM") as ZeugnisFachBearbeitenPageVM;

         this.comboxFachart.Items.Clear();
         foreach (Fachart a in Enum.GetValues(typeof(Fachart)))
            this.comboxFachart.Items.Add(a);

         _viewmodel.UFADialogRequest += OnUFADialogRequest;
         _viewmodel.MessageBoxRequest += OnMessageBoxRequest;
         //_viewmodel.LehrerDialogRequest += OnLehrerDialogRequest;
      }

      private void OnUFADialogRequest(object sender, DialogEventArgs e)
      {
         UnterrichtsfachBearbeitenWindow dlg = new UnterrichtsfachBearbeitenWindow(e.dm);

         if (e.ResultAction != null)
            e.ResultAction(dlg.ShowDialog());
      }
      private void OnMessageBoxRequest(object sender, MessageBoxEventArgs e)
      {
         MessageBoxResult r = MessageBox.Show(e.MessageBoxText, e.Caption, e.MessageBoxButton, e.MessageBoxImage);

         if (e.ResultAction != null)
            e.ResultAction(r);
      }
      //private void OnLehrerDialogRequest(object sender, DialogEventArgs e)
      //{
      //    UnterrichtsfachBearbeitenWindow dlg = new UnterrichtsfachBearbeitenWindow(e.dm);

      //    if (e.ResultAction != null)
      //        e.ResultAction(dlg.ShowDialog());
      //}
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

//Author: Alexander Bierler 
//License: MIT

namespace Notenmanager.View.UIComp
{
   public partial class CrashMessage : Window
   {
      internal static void ShowErrorMessage(Exception e)
      {
         new CrashMessage(e, null, null, false).ShowDialog();
      }

      string _crashreppath = "";

      private CrashMessage(Exception e, string desc, string crashreppath, bool canRestart = true)
      {
         InitializeComponent();
         InitLang();

         //this.Icon = Global.ImageSourceFromBitmap(Properties.Resources.X_Red);
         this.Width += 20;
         this.Height += 20;

         txtTitle.Text ="Error: " + e.GetType().Name;
         txtSDesc.Text = e.Message;

         txtDesc.Text = (desc != null ? desc : e.ToString());

         if (string.IsNullOrWhiteSpace(crashreppath))
            btnShowReport.IsEnabled = false;
         else
            _crashreppath = crashreppath;

         btnRestart.IsEnabled = canRestart;
      }

      private void InitLang()
      {
         //btnShowReport.Content = "Bericht anzeigen";
         //btnCopyToClipboard.Content = "In Zwischenablage kopieren";
         //btnRestart.Content = "Neustarten";
         //btnExit.Content = "Programm beenden";

         //btnShowReport.Content = LangLib_Global.CrashMessage.ShowReport;
         //btnCopyToClipboard.Content = LangLib_Global.CrashMessage.CopyToClipboard;
         //btnRestart.Content = LangLib_Global.GEN.Restart;
         //btnExit.Content = LangLib_Global.CrashMessage.ProgExit;
      }

      private void btnShowReport_Click(object sender, RoutedEventArgs e)
      {
         //Global.SaveStartProcess("notepad.exe", _crashreppath);
      }

      private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
      {
         Clipboard.SetText(txtDesc.Text);
      }

      private void btnRestart_Click(object sender, RoutedEventArgs e)
      {
         //Global.Restart();
      }

      private void btnExit_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
         Thread.Sleep(100);
         Environment.Exit(0);
      }
   }
}

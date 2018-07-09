using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Notenmanager
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         DBZugriff.InitDB();

         AppDomain.CurrentDomain.UnhandledException += (sender, e2) =>
         {
            DBZugriff.CloseDB();
         };
      }

      private void Application_Exit(object sender, ExitEventArgs e)
      {
         DBZugriff.CloseDB();
      }
   }
}

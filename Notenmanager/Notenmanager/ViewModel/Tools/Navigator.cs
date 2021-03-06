﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notenmanager.ViewModel.Tools
{
   /// <summary>
   /// Klasse zum Vereinfachen der Navigation zwischen Pages dieser WPF-Anwendung.
   /// Diese Klasse ist nach dem Singleton-Pattern codiert.
   /// </summary>
   public class Navigator
   {

      private Navigator() { }

      public event EventHandler PageChangedFinished;
      public event EventHandler<NavigationEventArgs> PageChanged;

      /// <summary>
      /// einmalige Instanz
      /// </summary>
      public static Navigator Instance { get; } = new Navigator();

      /// <summary>
      /// Navigiert zu einer Seite. 
      /// </summary>
      /// <param name="resourceKey">Schlüssel aus dem ResourceDictionary der Seite, zu der navigiert werden soll</param>
      public void NavigateTo(string resourceKey)
      {
         if (!resourceKey.Equals(String.Empty))
         {
            var page = App.Current.FindResource(resourceKey) as Page;
            PageChanged?.Invoke(this, new NavigationEventArgs()
            {
               ZielPage = page,
            });
            (App.Current.FindResource("MainWindowVM") as MainWindowVM).CurrentPage = page;
            PageChangedFinished?.Invoke(this, new EventArgs());


         }
      }


      //DB fertig
      public void StartUpDone()
      {
         PageChangedFinished?.Invoke(this, new EventArgs());
      }
   }
}

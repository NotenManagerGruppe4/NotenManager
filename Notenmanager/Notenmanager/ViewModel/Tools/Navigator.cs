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
        /// <summary>
        /// einmalige Instanz
        /// </summary>
        private static Navigator _instance = new Navigator();

        private Navigator() { }

        public event EventHandler PageChanged; 

        public static Navigator Instance
        {
            get
            {
                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// Navigiert zu einer Seite. 
        /// </summary>
        /// <param name="resourceKey">Schlüssel aus dem ResourceDictionary der Seite, zu der navigiert werden soll</param>
        public void NavigateTo(string resourceKey)
        {
            (App.Current.FindResource("MainWindowVM") as MainWindowVM).CurrentPage = App.Current.FindResource(resourceKey) as Page;
            PageChanged?.Invoke(this, new EventArgs());
        }
    }
}

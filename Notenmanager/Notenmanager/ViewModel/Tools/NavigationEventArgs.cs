using System;
using System.Windows;
using System.Windows.Controls;

namespace Notenmanager.ViewModel
{
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Seite zu der navigiert werden soll.
        /// </summary>
        public Page ZielPage { get; set; }

        public Window ZielWindow { get; set; }
    }
}
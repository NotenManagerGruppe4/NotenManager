using System;
using System.Windows.Controls;

namespace Notenmanager.ViewModel
{
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Seite zu der navigiert werden soll.
        /// </summary>
        public Page ZielPage { get; set; }
    }
}
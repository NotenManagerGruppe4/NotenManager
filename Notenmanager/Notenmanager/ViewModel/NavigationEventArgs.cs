using System;
using System.Windows.Controls;

namespace Notenmanager.ViewModel
{
    public class NavigationEventArgs : EventArgs
    {
        public Page ZielPage { get; set; }
    }
}
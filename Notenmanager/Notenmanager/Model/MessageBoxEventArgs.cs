using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notenmanager.Model
{
    public class MessageBoxEventArgs
    {
        public string MessageBoxText { get; set; }
        public string Caption { get; set; }
        public MessageBoxButton MessageBoxButton { get; set; }
        public MessageBoxImage MessageBoxImage { get; set; }
        public Action<MessageBoxResult> ResultAction { get; set; }
    }
}

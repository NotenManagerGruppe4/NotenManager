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
        public MessageBoxEventArgs() { }
        public MessageBoxEventArgs(Action<MessageBoxResult> resultAction, string text, string caption, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBoxText = text;
            Caption = caption;
            MessageBoxImage = image;
            MessageBoxButton = button;
            ResultAction = resultAction;
        }
    }
}

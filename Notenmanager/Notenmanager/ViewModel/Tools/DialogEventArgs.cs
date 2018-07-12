using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.ViewModel
{
    public class DialogEventArgs
    {
        public Action<bool?> ResultAction { get; set; }
        public DialogMode dm;

        public DialogEventArgs(Action<bool?> resultAction, DialogMode dm)
        {
            this.dm = dm;
            ResultAction = resultAction;
        }
    }
}

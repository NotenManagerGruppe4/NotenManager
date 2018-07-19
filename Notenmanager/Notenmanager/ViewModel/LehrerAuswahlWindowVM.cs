using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.ViewModel
{
    public class LehrerAuswahlWindowVM
    {
        private ObservableCollection<Lehrer> lehrerListe = new ObservableCollection<Lehrer>(DBZugriff.Current.Select<Lehrer>());
    }
}

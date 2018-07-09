using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_EM;

namespace Notenmanager.ViewModel
{
    public enum Fachart { Wahlfach = 1, Pflichtfach = 2, Wahlpflichtfach = 3};

    public class FachAnlegenPageVM : BaseViewModel
    {
        private ObservableCollection<Unterrichtsfach> _lstUFach = new ObservableCollection<Unterrichtsfach>();

        public ObservableCollection<Unterrichtsfach> LstUFach
        {
            get
            {
                return _lstUFach;
            }

            set
            {
                _lstUFach = value;
                onPropertyChanged("LstUFach");
            }
        }
    }
}

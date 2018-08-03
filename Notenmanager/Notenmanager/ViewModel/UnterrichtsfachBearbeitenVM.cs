using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class UnterrichtsfachBearbeitenVM : BaseViewModel
    {
        //Connector zum Parent (ZeugnisFachVM)
        private Unterrichtsfach uf;
        public Unterrichtsfach UF
        {
            get
            {
                return uf;
            }
            set
            {
                uf = value;
                if (uf != null)
                {
                    OnPropertyChanged(nameof(Pos));
                    OnPropertyChanged(nameof(Bez));
                    OnPropertyChanged(nameof(Stunden));
                }
                OnPropertyChanged(nameof(UFachSaveAble));
            }
        }

        #region Properties
        public string Bez
        {
            get
            {
                return UF.Bez;
            }
            set
            {
                UF.Bez = value;
                OnPropertyChanged();
                SaveAbleChanged();
            }
        }
        public int Stunden
        {
            get
            {
                return UF.Stunden;
            }
            set
            {
                UF.Stunden = value;
                OnPropertyChanged();
                SaveAbleChanged();
            }
        }
        public int Pos
        {
            get
            {
                return UF.Pos;
            }
            set
            {
                UF.Pos = value;
                OnPropertyChanged();
                SaveAbleChanged();
            }
        }

        #endregion Properties

        private void SaveAbleChanged()
        {
            OnPropertyChanged(nameof(UFachSaveAble));
        }
        public bool UFachSaveAble
        {
            get
            {
                return (UF.Stunden >= 0 && UF.Pos > 0 && !string.IsNullOrWhiteSpace(UF.Bez));
            }
        }

    }
}

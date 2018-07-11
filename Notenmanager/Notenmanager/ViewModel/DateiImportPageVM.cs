using Notenmanager.Model;
using Notenmanager.Persistenz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class DateiImportPageVM : BaseViewModel
    {
        #region Instanzvariablen
        private string _dateiPfad;
        private ComboBoxItem _dateiTyp;
        private ObservableCollection<Schule> _schulen;
        private Schule _selektierteSchule;
        #endregion

        public DateiImportPageVM()
        {
            DateiImportierenCmd = new ActionCommand(OnDateiImportieren);
            Schulen = new ObservableCollection<Schule>(DBZugriff.Current.Select<Schule>());
        }

        #region Events
        #endregion

        #region Public Properties
        #region Commands
        public ICommand DateiImportierenCmd { get; set; } 
        #endregion
        public string DateiPfad
        {
            get
            {
                return _dateiPfad;
            }

            set
            {
                SetValue(ref _dateiPfad, value);
            }
        }

        public ComboBoxItem DateiTyp
        {
            get
            {
                return _dateiTyp;
            }

            set
            {
                SetValue(ref _dateiTyp, value);
            }
        }

        public ObservableCollection<Schule> Schulen
        {
            get
            {
                return _schulen;
            }

            set
            {
                SetValue(ref _schulen, value);
            }
        }

        public Schule SelektierteSchule
        {
            get
            {
                return _selektierteSchule;
            }

            set
            {
                SetValue(ref _selektierteSchule, value);
            }
        }
        #endregion

        #region Methoden
        private void OnDateiImportieren(object obj)
        {
            switch(DateiTyp.Name)
            {
                case "Klasse":
                    DateiZugriff.ImportKlassen(DateiPfad, SelektierteSchule);
                    break;
                case "Schueler":
                    DateiZugriff.ImportSchueler(DateiPfad);
                    break;
                case "Lehrer":
                    DateiZugriff.ImportLehrer(DateiPfad);
                    break;
            }
        }
        #endregion
    }
}

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
        /// <summary>
        /// Pfad der zu Importierenden Datei
        /// </summary>
        private string _dateiPfad;
        /// <summary>
        /// Legt fest, um welche Art von Stammdatendatei(Schüler-, Lehrer- oder Klassendatei) es sich handelt
        /// </summary>
        private ComboBoxItem _dateiTyp;
        /// <summary>
        /// Liste aller in der Datenbank angelegten Schulen für die ComboBox zur Auswahl einer Schule
        /// </summary>
        private ObservableCollection<Schule> _schulen;
        /// <summary>
        /// In der ComboBox zur Auswahl einer Schule selektierte Schule
        /// </summary>
        private Schule _selektierteSchule;
        /// <summary>
        /// Legt fest, ob die ComboBox zur Auswahl der Schulen gebraucht wird und (de)aktiviert diese 
        /// </summary>
        private bool _cbSchulenEnabled = false;
        #endregion

        public DateiImportPageVM()
        {
            // Commands initialisieren
            DateiImportierenCmd = new ActionCommand(OnDateiImportieren);
            CbSchulenSelectionChangedCmd = new ActionCommand(OnCbSchulenSelectionChanged);

            // Liste aller Schulen aus der Datenbank befüllen
            Schulen = new ObservableCollection<Schule>(DBZugriff.Current.Select<Schule>());
        }

        #region Events
        #endregion

        #region Public Properties
        #region Commands
        public ICommand DateiImportierenCmd { get; set; } 
        public ICommand CbSchulenSelectionChangedCmd { get; set; }
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

        public bool CbSchulenEnabled
        {
            get
            {
                return _cbSchulenEnabled;
            }

            set
            {
                SetValue(ref _cbSchulenEnabled, value);
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

        private void OnCbSchulenSelectionChanged(object obj)
        {
            CbSchulenEnabled = DateiTyp.Name == "Schule";
        }
        #endregion
    }
}

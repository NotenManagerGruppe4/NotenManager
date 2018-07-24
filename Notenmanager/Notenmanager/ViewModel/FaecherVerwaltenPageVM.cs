using Notenmanager.Model;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class FaecherVerwaltenPageVM : BaseViewModel
    {
        #region Instanzvariablen
        private ObservableCollection<Schule> _schulen;
        private ObservableCollection<Klasse> _klassen;
        private ObservableCollection<Zeugnisfach> _zfaecher;
        private ObservableCollection<Unterrichtsfach> _ufaecher;
        private Schule _selectedSchule;
        private Klasse _selectedKlasse;
        private Zeugnisfach _selectedZFach;
        private bool _buttonsEnabled = false;
        #endregion

        #region Konstruktoren
        public FaecherVerwaltenPageVM()
        {
            // Commands initialisieren
            NavigationCmd = new Command<string>(OnNavigation);
            CBoxSchulenChangedCmd = new ActionCommand(OnCBoxSchulenChanged);
            CBoxKlassenChangedCmd = new ActionCommand(OnCBoxKlassenChanged);

            // Initiales Befüllen der ComboBoxen aus der Datenbank und der Zeugnisfächerliste
            Schulen = new ObservableCollection<Schule>(DBZugriff.Current.Select<Schule>(x => x.Active == true));
            SelectedSchule = Schulen[0];
            Klassen = new ObservableCollection<Klasse>(DBZugriff.Current.Select<Klasse>(x => x.Schule == SelectedSchule));
            SelectedKlasse = Klassen[0];
            Zfaecher = new ObservableCollection<Zeugnisfach>(DBZugriff.Current.Select<Zeugnisfach>(x => x.Klasse == SelectedKlasse && x.Active == true));
        }
        #endregion

        #region Public Properties

        #region Commands
        public Command<string> NavigationCmd { get; set; }
        public ICommand CBoxKlassenChangedCmd { get; set; }
        public ICommand CBoxSchulenChangedCmd { get; set; }
        #endregion

        /// <summary>
        /// Liste aller Schulen
        /// </summary>
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

        /// <summary>
        /// Liste aller Klassen (einer ausgewählten Schule)
        /// </summary>
        public ObservableCollection<Klasse> Klassen
        {
            get
            {
                return _klassen;
            }

            set
            {
                SetValue(ref _klassen, value);
            }
        }

        /// <summary>
        /// Liste aller Zeugnisfächer (einer ausgewählten Klasse)
        /// </summary>
        public ObservableCollection<Zeugnisfach> Zfaecher
        {
            get
            {
                return _zfaecher;
            }

            set
            {
                SetValue(ref _zfaecher, value);
            }
        }

        /// <summary>
        /// Liste aller Unterrichtsfächer (zu einem Zeugnisfach)
        /// </summary>
        public ObservableCollection<Unterrichtsfach> Ufaecher
        {
            get
            {
                return _ufaecher;
            }

            set
            {
                SetValue(ref _ufaecher, value);
            }
        }

        /// <summary>
        /// Gibt an, ob die Buttons betätigt werden können oder nicht
        /// </summary>
        public bool ButtonsEnabled
        {
            get
            {
                return _buttonsEnabled;
            }

            set
            {
                SetValue(ref _buttonsEnabled, value);
            }
        }

        /// <summary>
        /// Ausgewählte Schule aus der ComboBox
        /// </summary>
        public Schule SelectedSchule
        {
            get
            {
                return _selectedSchule;
            }

            set
            {
                SetValue(ref _selectedSchule, value);
            }
        }

        /// <summary>
        /// Ausgewählte Klasse aus der ComboBox
        /// </summary>
        public Klasse SelectedKlasse
        {
            get
            {
                return _selectedKlasse;
            }

            set
            {
                SetValue(ref _selectedKlasse, value);
            }
        }

        /// <summary>
        /// Ausgewähltes Zeugnisfach aus dem DataGrid
        /// </summary>
        public Zeugnisfach SelectedZFach
        {
            get
            {
                return _selectedZFach;
            }

            set
            {
                SetValue(ref _selectedZFach, value);
                ButtonsEnabled = SelectedZFach != null;

                // Unterrichtsfächerliste aktualisieren
                Ufaecher = new ObservableCollection<Unterrichtsfach>(DBZugriff.Current.Select<Unterrichtsfach>(x => x.Zeugnisfach == SelectedZFach && x.Active == true));
            }
        }
        #endregion

        #region Handler Methoden
        /// <summary>
        /// Zeugnisfächerliste aktualisieren, wenn eine neue Klasse in der ComboBox ausgewählt wurde
        /// </summary>
        /// <param name="obj"></param>
        private void OnCBoxKlassenChanged(object obj)
        {
            Zfaecher = new ObservableCollection<Zeugnisfach>(DBZugriff.Current.Select<Zeugnisfach>(x => x.Klasse == SelectedKlasse && x.Active == true));
            Ufaecher.Clear();
        }

        /// <summary>
        /// Items der Klassen-ComboBox aktualisieren, wenn eine neue Schule in der ComboBox ausgewählt wurde
        /// </summary>
        /// <param name="obj"></param>
        private void OnCBoxSchulenChanged(object obj)
        {
            Klassen = new ObservableCollection<Klasse>(DBZugriff.Current.Select<Klasse>(x => x.Schule == SelectedSchule));
        }


        private void OnNavigation(string key)
        {
            Navigator.Instance.NavigateTo(key);
        }
        #endregion
    }
}

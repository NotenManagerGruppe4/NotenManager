using Microsoft.Win32;
using Notenmanager.Model;
using Notenmanager.Persistenz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            CBoxChangedCmd = new ActionCommand(OnCBoxSelectionChanged);
            AbbrechenCmd = new Command<string>(OnAbbrechen);
            DateiAuswaehlenCmd = new ActionCommand(OnDateiAuswaehlen);

            MainPageVM parentViewModel = App.Current.FindResource("MainPageVM") as MainPageVM;
            

            // Liste aller Schulen aus der Datenbank befüllen
            Schulen = new ObservableCollection<Schule>(DBZugriff.Current.Select<Schule>());
        }


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<NavigationEventArgs> NavigationRequest;
        #endregion

        #region Public Properties
        #region Commands
        public ICommand DateiImportierenCmd { get; set; } 
        public ICommand CBoxChangedCmd { get; set; }
        public ICommand AbbrechenCmd { get; set; }
        public ICommand DateiAuswaehlenCmd { get; set; }
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
        #region CommandHandler
        private void OnDateiAuswaehlen(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV files (*.csv)|*.csv|All files(*.*)|*.*";
            fileDialog.ShowDialog();
            DateiPfad = fileDialog.FileName;    
        }

        private void OnDateiImportieren(object obj)
        {
            bool fileFound = true;
            try
            {
                DateiPfad.ToString(); 
            }

            catch(Exception e)
            {
                // Fehlermeldung, wenn keine Datei gefunden oder falscher Dateipfad angegeben.
                MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs()
                {
                    Caption = "Datei nicht gefunden!",
                    MessageBoxText = "Datei nicht gefunden oder keine Datei ausgewählt!",
                    MessageBoxImage = System.Windows.MessageBoxImage.Information,
                    MessageBoxButton = System.Windows.MessageBoxButton.OK,
                });
                fileFound = false;
            }

            if(fileFound == true)
            {
                try
                {
                    Importstatistik rueckmeldung = null;

                    // bestimmen um welche Dateiart es sich handelt und diese entsprechend importieren
                    switch (DateiTyp.Content.ToString())
                    {
                        case "Klasse":
                            rueckmeldung = DateiZugriff.ImportKlassen(DateiPfad, SelektierteSchule);
                            break;
                        case "Schueler":
                            rueckmeldung = DateiZugriff.ImportSchueler(DateiPfad);
                            break;
                        case "Lehrer":
                            rueckmeldung = DateiZugriff.ImportLehrer(DateiPfad);
                            break;
                    }

                    // Erfolgsmeldung
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs()
                    {
                        Caption = "Datei-Import abgeschlossen",
                        MessageBoxText = $"{DateiTyp.Content.ToString()}-Datei erfolgreich importiert!\r\n\r\n{rueckmeldung?.ToString()}",
                        MessageBoxImage = System.Windows.MessageBoxImage.Information,
                        MessageBoxButton = System.Windows.MessageBoxButton.OK,
                    });    

                    DateiPfad = "";
                }
                catch (Exception e)
                {
                    // Fehlermeldung
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs()
                    {
                        Caption = "Datei-Import konnte nicht abgeschlossen werden...",
                        MessageBoxText = e.Message,
                        MessageBoxImage = System.Windows.MessageBoxImage.Exclamation,
                        MessageBoxButton = System.Windows.MessageBoxButton.OK
                    });
                }
            }

            else
            {

            }
            
        }

        private void OnCBoxSelectionChanged(object obj)
        {
            CbSchulenEnabled = DateiTyp.Content.ToString() == "Klasse";
        }

        private void OnAbbrechen(string key)
        {
            (App.Current.FindResource("MainWindowVM") as MainWindowVM).CurrentPage = App.Current.FindResource("MainPage") as Page;
        }
        #endregion
        #endregion
    }
}

using Notenmanager.Model;
using Notenmanager.View;
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
         LoeschenCmd = new ActionCommand(OnLoeschen);
         BearbeitenCmd = new Command<string>(OnBearbeiten);
         NeuCmd = new Command<string>(OnNeu);

      }

      #endregion

      #region Public Properties

      #region Commands
      public Command<string> NavigationCmd { get; set; }
      public Command<string> BearbeitenCmd { get; set; }
      public Command<string> NeuCmd { get; set; }
      public ICommand CBoxKlassenChangedCmd { get; set; }
      public ICommand CBoxSchulenChangedCmd { get; set; }
      public ICommand LoeschenCmd { get; set; }
      #endregion

      /// <summary>
      /// Liste aller Schulen
      /// </summary>
      public ObservableCollection<Schule> Schulen
      {
         get
         {
            return new ObservableCollection<Schule>(DBZugriff.Current.Select<Schule>());
         }
         set
         {
            OnPropertyChanged();
         }
      }

      /// <summary>
      /// Liste aller Klassen (einer ausgewählten Schule)
      /// </summary>
      public ObservableCollection<Klasse> Klassen
      {
         get
         {
            return new ObservableCollection<Klasse>(DBZugriff.Current.Select<Klasse>(x => x.Schule == SelectedSchule));
         }
         set
         {
            OnPropertyChanged();
         }
      }

      /// <summary>
      /// Liste aller Zeugnisfächer (einer ausgewählten Klasse)
      /// </summary>
      public ObservableCollection<Zeugnisfach> Zfaecher
      {
         get
         {
            return new ObservableCollection<Zeugnisfach>(DBZugriff.Current.Select<Zeugnisfach>(x => x.Klasse == SelectedKlasse));
         }
         set
         {
            OnPropertyChanged();
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
            if (_selectedSchule == null && Schulen.Count > 0)
               return SelectedSchule = Schulen[0];

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
            if(_selectedKlasse == null && Klassen.Count > 0)
               SelectedKlasse = Klassen[0];

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
            Ufaecher = new ObservableCollection<Unterrichtsfach>(DBZugriff.Current.Select<Unterrichtsfach>(x => x.Zeugnisfach == SelectedZFach));
         }
      }
      #endregion

      #region Events
      public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
      #endregion

      #region Handler Methoden
      /// <summary>
      /// Zeugnisfächerliste aktualisieren, wenn eine neue Klasse in der ComboBox ausgewählt wurde
      /// </summary>
      /// <param name="obj"></param>
      private void OnCBoxKlassenChanged(object obj)
      {
         //nur Updaten
         Zfaecher = null;
         Ufaecher.Clear();
      }

      /// <summary>
      /// Items der Klassen-ComboBox aktualisieren, wenn eine neue Schule in der ComboBox ausgewählt wurde
      /// </summary>
      /// <param name="obj"></param>
      private void OnCBoxSchulenChanged(object obj)
      {
         Klassen = null;
      }

      private void OnNeu(string key)
      {
         var viewModel = App.Current.FindResource("ZFBearbeitenVM") as ZeugnisFachBearbeitenPageVM;
         viewModel.Modus = DialogMode.Neu;
         viewModel.ZF.Klasse = SelectedKlasse;

         Navigator.Instance.NavigateTo(key);
      }

      private void OnBearbeiten(string key)
      {
         var viewModel = App.Current.FindResource("ZFBearbeitenVM") as ZeugnisFachBearbeitenPageVM;
         viewModel.Modus = DialogMode.Aendern;
         viewModel.ZF = SelectedZFach;

         Navigator.Instance.NavigateTo(key);
      }

      /// <summary>
      /// Ein Zeugnisfach löschen und aus der Liste entfernen
      /// </summary>
      /// <param name="obj"></param>
      private void OnLoeschen(object obj)
      {
         MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs()
         {
            Caption = "Sind Sie sicher?",
            MessageBoxText = $"Wollen sie das Zeugnisfach {SelectedZFach.Bez} wirklich löschen?",
            MessageBoxButton = System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage = System.Windows.MessageBoxImage.Question,
            ResultAction = (result) =>
            {
               if (result == System.Windows.MessageBoxResult.Yes)
               {
                  SelectedZFach.Loeschen();
                  Zfaecher.Remove(SelectedZFach);
               }
            }
         });
      }

      private void OnNavigation(string key)
      {
         Navigator.Instance.NavigateTo(key);
      }
      #endregion
   }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notenmanager.Model;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;
using Notenmanager.ViewModel.Tools;
using Notenmanager.View;

namespace Notenmanager.ViewModel
{
    
    public enum DialogMode { Neu, Aendern };

    public class ZeugnisFachBearbeitenPageVM : BaseViewModel
    {
        private ObservableCollection<Unterrichtsfach> _lstUFach = new ObservableCollection<Unterrichtsfach>();
        private ObservableCollection<UFachLehrer> _lstULehrer = new ObservableCollection<UFachLehrer>();
        private Unterrichtsfach _selFach;
        private UFachLehrer _selULehrer;
        private Zeugnisfach _zf;
        private DialogMode _modus;

        private UnterrichtsfachBearbeitenVM ufvm;
        private LehrerAuswahlWindowVM lavm;
        private FaecherVerwaltenPageVM fvvm;

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;

        #region Commands
        public ICommand OnBtnAnlegenCmd { get; set; }
        public ICommand OnBtnAendernCmd { get; set; }
        public Command<string> OnBtnSpeichernCmd { get; set; }
        public ICommand OnBtnEntfernenCmd { get; set; }
        public Command<string> OnBtnAbbrechenCmd { get; set; }
        public ICommand OnBtnHinzufuegenCmd { get; set; }
        public ICommand OnBtnLehrerEntfernenCmd { get; set; }
        public ICommand OnUFachEditCmd { get; set; }

        #endregion Commands
        public ZeugnisFachBearbeitenPageVM()
        {
            OnBtnAnlegenCmd = new ActionCommand(OnBtnAnlegen);
            OnBtnAendernCmd = new ActionCommand(OnBtnAendern);
            OnBtnSpeichernCmd = new Command<string>(OnBtnSpeichern);
            OnBtnEntfernenCmd = new ActionCommand(OnBtnEntfernen);
            OnBtnAbbrechenCmd = new Command<string>(OnBtnAbbrechen);
            OnBtnHinzufuegenCmd = new ActionCommand(OnBtnHinzufuegen);
            OnBtnLehrerEntfernenCmd = new ActionCommand(OnBtnLehrerEntfernen);

            ufvm = App.Current.FindResource("UFBearbeitenVM") as UnterrichtsfachBearbeitenVM;
            lavm = App.Current.FindResource("LehrerAuswahlVM") as LehrerAuswahlWindowVM;
            fvvm = App.Current.FindResource("FaecherVerwaltenPageVM") as FaecherVerwaltenPageVM;

        }

        #region Properties

        public ObservableCollection<Unterrichtsfach> LstUFach
        {
            get
            {
                return _lstUFach;
            }

            set
            {
                _lstUFach = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UFachLehrer> LstULehrer
        {
            get
            {
                return _lstULehrer;
            }
            set
            {
                _lstULehrer = value;
                OnPropertyChanged();
            }
        }
        public Unterrichtsfach SelFach
        {
            get
            {
                return _selFach;
            }

            set
            {
                _selFach = value;
                LstULehrer = new ObservableCollection<UFachLehrer>(DBZugriff.Current.Select<UFachLehrer>(x => x.Unterrichtsfach == SelFach));
                OnPropertyChanged();
                OnPropertyChanged(nameof(EnableButton));
            }
        }
        public UFachLehrer SelULehrer
        {
            get
            {
                return _selULehrer;
            }
            set
            {
                _selULehrer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EnableLehrerEntfernenButton));
            }
        }

        public Zeugnisfach ZF
        {
            get
            {
                return _zf;
            }

            set
            {
                _zf = value;
                LstUFach = new ObservableCollection<Unterrichtsfach>(DBZugriff.Current.Select<Unterrichtsfach>(x => x.Zeugnisfach == ZF));

                OnPropertyChanged();
                OnPropertyChanged(nameof(Bez));
                OnPropertyChanged(nameof(Pos));
                OnPropertyChanged(nameof(Fachart));
                OnPropertyChanged(nameof(AbschliessendesFach));
                OnPropertyChanged(nameof(Vorrueckungsfach));
                SaveAbleChanged();
            }
        }

        public string Bez
        {
            get
            {
                return ZF.Bez;
            }
            set
            {
                ZF.Bez = value;
                OnPropertyChanged();
                SaveAbleChanged();
            }
        }

        public int Pos
        {
            get
            {
                return ZF.Pos;
            }
            set
            {
                ZF.Pos = value;
                OnPropertyChanged();
                SaveAbleChanged();
            }
        }
        public Fachart Fachart
        {
            get
            {
                return ZF.Fachart;
            }
            set
            {
                ZF.Fachart = value;
                OnPropertyChanged();
            }
        }
        public bool AbschliessendesFach
        {
            get
            {
                return ZF.AbschliessendesFach;
            }
            set
            {
                ZF.AbschliessendesFach = value;
                OnPropertyChanged();
            }
        }
        public bool Vorrueckungsfach
        {
            get
            {
                return ZF.Vorrueckungsfach;
            }
            set
            {
                ZF.Vorrueckungsfach = value;
                OnPropertyChanged();
            }
        }

        public DialogMode Modus
        {
            get
            {
                return _modus;
            }
            set
            {
                if (value == DialogMode.Neu)
                    ZF = new Zeugnisfach();

                _modus = value;    
            } 
        }
        #endregion Properties

        #region Methoden fuer Unterrichtsfaecher
        // um neues Unterrichtsfach anzulegen
        private void OnBtnAnlegen(object obj)
        {
            ufvm.UF = new Unterrichtsfach();
            ufvm.Bez = "";

            UnterrichtsfachBearbeitenWindow dlg = new UnterrichtsfachBearbeitenWindow(DialogMode.Neu);
            bool? e = dlg.ShowDialog();
            if (e != null)
                DoAnlegen(e);
        }

        private void DoAnlegen(bool? obj)
        {
            if (obj != true)
            {
                //ufvm.UF = null;
                return;
            }

            ufvm.UF.Zeugnisfach = ZF;
            ufvm.UF.Speichern();
            LstUFach.Add(ufvm.UF);
            SelFach = ufvm.UF;
            // Liste der Unterrichtsfächer im VM der FaecherVerwaltenPage aktualisieren 
            fvvm.Ufaecher.Add(ufvm.UF);
        }

        // um bestehendes Unterrichtsfach zu ändern
        private void OnBtnAendern(object obj)
        {
            ufvm.UF = SelFach;

            UnterrichtsfachBearbeitenWindow dlg = new UnterrichtsfachBearbeitenWindow(DialogMode.Aendern);
            bool? e = dlg.ShowDialog();
            if (e != null)
                DoAendern(e);
        }
        private void DoAendern(bool? obj)
        {
            if (obj != true)
            {
                ufvm.UF.Reload();
                return;
            }
 
            int index = LstUFach.IndexOf(ufvm.UF);

            // Index löschen und neu hinzufügen 
            LstUFach.RemoveAt(index);
            LstUFach.Insert(index, ufvm.UF);

            // Selektiertes Fach setzen
            SelFach = ufvm.UF;
            SelFach.Speichern();

            // Liste der Unterrichtsfächer im VM der FaecherVerwaltenPage aktualisieren
            int i = (App.Current.FindResource("FaecherVerwaltenPageVM") as FaecherVerwaltenPageVM).Ufaecher.IndexOf(ufvm.UF);
            fvvm.Ufaecher.RemoveAt(i);
            fvvm.Ufaecher.Insert(i, ufvm.UF);
        }

        // um Unterrichtsfach zu entfehrnen 
        private void OnBtnEntfernen(object obj)
        {
            MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(DoEntfernen, "Wirklich löschen?", "Sind Sie sicher?", MessageBoxButton.YesNo, MessageBoxImage.Question));
        }

        private void DoEntfernen(MessageBoxResult obj)
        {
            if (obj == MessageBoxResult.Yes)
            {
                // Liste der Unterrichtsfächer im VM der FaecherVerwaltenPage aktualisieren 
                fvvm.Ufaecher.Remove(SelFach);
                this.SelFach.Loeschen();
                this.LstUFach.Remove(SelFach);
            }
            OnPropertyChanged(nameof(LstUFach));
        }

        // Aktivierung Buttons wenn ein Fach selektiert wurde
        public bool EnableButton
        {
            get
            {
                if (SelFach != null)
                    return true;

                return false;
            }
        }
        #endregion

        #region Methoden fuer Lehrerauswahl

        // um Lehrer hinzuzufügen
        private void OnBtnHinzufuegen(object obj)
        {
            // Übergabe des gewählten Unterrichtsfach -> Lehrer kann nicht ohne UFach bestehen
            LehrerAuswahlWindow dlg = new LehrerAuswahlWindow(SelFach);
            bool? e = dlg.ShowDialog();
            if (e != null)
                DoHinzufuegen(e);
        }
        private void DoHinzufuegen(bool? obj)
        {
            if (obj != true)
                return;

            // Liste Lehrer, die aus der DB gelesen werden zum selektierten Unterrichtsfach
            LstULehrer = new ObservableCollection<UFachLehrer>(DBZugriff.Current.Select<UFachLehrer>(x => x.Unterrichtsfach == SelFach));
        }

        // um Lehrer zu entfernen
        private void OnBtnLehrerEntfernen(object obj)
        {
            MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(DoLehrerEntfernen, "Wirklich entfernen?", "Sind Sie sicher?", MessageBoxButton.YesNo, MessageBoxImage.Question));
        }
        private void DoLehrerEntfernen(MessageBoxResult obj)
        {
            if(obj == MessageBoxResult.Yes)
            {
                // Löschen der Eintragung in der Relationstabelle UFachLehrer
                UFachLehrer ufl = DBZugriff.Current.SelectFirstOrDefault<UFachLehrer>(x => x.Lehrer == SelULehrer.Lehrer && x.Unterrichtsfach == SelULehrer.Unterrichtsfach);
                ufl.Loeschen();   
                this.LstULehrer.Remove(SelULehrer);
            }
            OnPropertyChanged(nameof(LstULehrer));
        }
        // Aktivierung der Buttons wenn ein Lehrer selektiert wurde
        public bool EnableLehrerEntfernenButton
        {
            get
            {
                if (SelULehrer != null)
                    return true;

                return false;
            }
        }
        #endregion

        // Zeugnisfach speichern und auf vorherige Seite zurückkehren
        private void OnBtnSpeichern(string obj)
        {
            ZF.Speichern();

            // Zeugnisfachliste im FaecherVerwaltenVM aktualisieren
            if (Modus == DialogMode.Neu)
                fvvm.Zfaecher.Add(ZF);
            else if (Modus == DialogMode.Aendern)
            {
                int ind = fvvm.Zfaecher.IndexOf(ZF);
                fvvm.Zfaecher.RemoveAt(ind);
                fvvm.Zfaecher.Insert(ind, ZF);
            }

            Navigator.Instance.NavigateTo(obj);
        } 

        // auf vorherige Seite zurückkehren 
        private void OnBtnAbbrechen(string obj)
        {
            Navigator.Instance.NavigateTo(obj);
        }
       
        private void SaveAbleChanged()
        {
            OnPropertyChanged(nameof(ZFachSaveAble));
        }

        // Überprüfung der "Mussfelder"
        public bool ZFachSaveAble
        {
            get
            {
                if (ZF == null)
                {
                    Trace.WriteLine("Zeugnisfach NULL!");
                    return false;
                }
                return (ZF.Pos > 0 && !string.IsNullOrWhiteSpace(ZF.Bez));
            }
        }
    }
}

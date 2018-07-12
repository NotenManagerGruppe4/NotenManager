using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notenmanager.Model;
using System.Windows.Input;
using System.Diagnostics;

namespace Notenmanager.ViewModel
{
    public enum Fachart { Wahlfach = 1, Pflichtfach = 2, Wahlpflichtfach = 3 };
    public enum DialogMode { Neu, Aendern };

    public class ZeugnisFachBearbeitenPageVM : BaseViewModel
    {
        private ObservableCollection<Unterrichtsfach> _lstZeugUFach = new ObservableCollection<Unterrichtsfach>();
        private Unterrichtsfach _selFach;
        private ObservableCollection<Unterrichtsfach> _lstUFachHinz = new ObservableCollection<Unterrichtsfach>();

        public event EventHandler<DialogEventArgs> UFADialogRequest;

        public UnterrichtsfachBearbeitenVM ufvm;

        #region Commands
        public ICommand OnBtnAnlegenCmd { get; set; }
        public ICommand OnBtnAendernCmd { get; set; }

        public ICommand OnUFachEditCmd { get; set; }

        #endregion Commands
        public ZeugnisFachBearbeitenPageVM()
        {
            OnBtnAnlegenCmd = new ActionCommand(OnBtnAnlegen);
            OnBtnAendernCmd = new ActionCommand(OnBtnAendern);

            ufvm = App.Current.FindResource("UFBearbeitenVM") as UnterrichtsfachBearbeitenVM;
        }

        #region Properties
        public ObservableCollection<Unterrichtsfach> LstZeugUFach
        {
            get
            {
                return _lstZeugUFach;
            }

            set
            {
                _lstZeugUFach = value;
                OnPropertyChanged("LstZeugUFach");
            }
        }

        public ObservableCollection<Unterrichtsfach> LstUFachHinz
        {
            get
            {
                return _lstUFachHinz;
            }

            set
            {
                _lstUFachHinz = value;
                OnPropertyChanged("LstUFachHinz");
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
                OnPropertyChanged("SelFach");
            }
        }

       


        #endregion Properties

        private void OnBtnAnlegen(object obj)
        {
            ufvm.UF = new Unterrichtsfach();
            ufvm.Bez = "";

            UFADialogRequest?.Invoke(this, new DialogEventArgs(DoAnlegen, DialogMode.Neu));
        }

        private void DoAnlegen(bool? obj)
        {
            if (obj != true)
            {
                ufvm.UF = null;
                return;
            }

            ufvm.UF.Speichern();
            LstUFachHinz.Add(ufvm.UF);
            SelFach = ufvm.UF;

        }
        private void OnBtnAendern(object obj)
        {
            ufvm.UF = SelFach;

            UFADialogRequest?.Invoke(this, new DialogEventArgs(DoAendern, DialogMode.Aendern));
        }

        private void DoAendern(bool? obj)
        {
            if (obj != true)
            {
                ufvm.UF.Reload();
                return;
            }

            SelFach = ufvm.UF;

            SelFach.Speichern();
        }

        
    }
}

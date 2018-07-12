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

    public class FachAnlegenPageVM : BaseViewModel
    {

        private ObservableCollection<Unterrichtsfach> _lstZeugUFach = new ObservableCollection<Unterrichtsfach>();
        private Unterrichtsfach _selFach;
        private Unterrichtsfach _tmpFach;
        private ObservableCollection<Unterrichtsfach> _lstUFachHinz = new ObservableCollection<Unterrichtsfach>();

        public event EventHandler<DialogEventArgs> UFADialogRequest;

        #region Commands
        public ICommand OnBtnAnlegenCmd { get; set; }
        public ICommand OnBtnAendernCmd { get; set; }

        public ICommand OnUFachEditCmd { get; set; }

        #endregion Commands
        public FachAnlegenPageVM()
        {
            OnBtnAnlegenCmd = new ActionCommand(OnBtnAnlegen);
            OnBtnAendernCmd = new ActionCommand(OnBtnAendern);
            OnUFachEditCmd = new ActionCommand(OnUFachEdit);
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

        

        public Unterrichtsfach TmpFach
        {
            get
            {
                return _tmpFach;
            }

            set
            {
                _tmpFach = value;
                OnPropertyChanged();
            }
        }

        public bool TmpFachSaveAble
        {
            get
            {
                if(TmpFach == null)
                {
                    Trace.WriteLine("TMPFACH NULL!");
                    return false;
                }
                bool re = (TmpFach.Stunden >= 0 && TmpFach.Pos > 0 && !string.IsNullOrWhiteSpace(TmpFach.Bez));
                return re;
            }
        }

        #endregion Properties

        private void OnUFachEdit(object obj)
        {
            OnPropertyChanged("TmpFachSaveAble");
        }

        private void OnBtnAnlegen(object obj)
        {
            TmpFach = new Unterrichtsfach();
            TmpFach.Bez = "";

            UFADialogRequest?.Invoke(this, new DialogEventArgs(DoAnlegen, DialogMode.Neu));
        }

        private void DoAnlegen(bool? obj)
        {
            if (obj != true)
            {
                TmpFach = null;
                return;
            }

            TmpFach.Speichern();
            LstUFachHinz.Add(TmpFach);
            SelFach = TmpFach;

        }
        private void OnBtnAendern(object obj)
        {
            TmpFach = SelFach;

            UFADialogRequest?.Invoke(this, new DialogEventArgs(DoAendern, DialogMode.Aendern));
        }

        private void DoAendern(bool? obj)
        {
            if (obj != true)
            {
                TmpFach.Reload();
                return;
            }

            SelFach = TmpFach;

            SelFach.Speichern();
        }

        
    }
}

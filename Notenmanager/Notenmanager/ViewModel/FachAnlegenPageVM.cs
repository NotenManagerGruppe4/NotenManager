using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notenmanager.Model;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public enum Fachart { Wahlfach = 1, Pflichtfach = 2, Wahlpflichtfach = 3};
    public enum DialogMode { Neu, Aendern};

    public class FachAnlegenPageVM : BaseViewModel
    {
        private ObservableCollection<Unterrichtsfach> _lstZeugUFach = new ObservableCollection<Unterrichtsfach>();
        private Unterrichtsfach _selFach;
        private ObservableCollection<Unterrichtsfach> _lstUFachHinz = new ObservableCollection<Unterrichtsfach>();
        private Unterrichtsfach uf;

        public event EventHandler<DialogEventArgs> DialogRequest;
        public ICommand OnBtnAnlegenCmd { get; set; }
        public ICommand OnBtnAendernCmd { get; set; }

        public FachAnlegenPageVM()
        {
            OnBtnAnlegenCmd = new ActionCommand(OnBtnAnlegen);
            OnBtnAendernCmd = new ActionCommand(OnBtnAendern);
        }

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

        private void OnBtnAnlegen(object obj)
        {
            DialogRequest?.Invoke(this, new DialogEventArgs(DoAnlegen, DialogMode.Neu));
        }

        private void DoAnlegen(bool? obj)
        {
            if(obj  == true)
            {
                uf = new Unterrichtsfach();
                LstUFachHinz.Add(uf);
                uf.Speichern();
                SelFach = uf;
            }
        }
        private void OnBtnAendern(object obj)
        {
            DialogRequest?.Invoke(this, new DialogEventArgs(DoAendern, DialogMode.Aendern));
        }

        private void DoAendern(bool? obj)
        {
            if(obj == true)
            {

            }
        }
    }
}

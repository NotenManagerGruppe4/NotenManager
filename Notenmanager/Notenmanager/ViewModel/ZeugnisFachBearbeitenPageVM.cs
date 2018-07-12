﻿using System;
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
    
    public enum DialogMode { Neu, Aendern };

    public class ZeugnisFachBearbeitenPageVM : BaseViewModel
    {
        private ObservableCollection<Unterrichtsfach> _lstUFach = new ObservableCollection<Unterrichtsfach>();
        private Unterrichtsfach _selFach;
        private Zeugnisfach _zf;

        private UnterrichtsfachBearbeitenVM ufvm;


        public event EventHandler<DialogEventArgs> UFADialogRequest;


        #region Commands
        public ICommand OnBtnAnlegenCmd { get; set; }
        public ICommand OnBtnAendernCmd { get; set; }
        public ICommand OnBtnSpeichernCmd { get; set; }
        public ICommand OnUFachEditCmd { get; set; }
        
        #endregion Commands
        public ZeugnisFachBearbeitenPageVM()
        {
            ZF = DBZugriff.Current.SelectFirstOrDefault<Zeugnisfach>(x => x.Id == 1);

            OnBtnAnlegenCmd = new ActionCommand(OnBtnAnlegen);
            OnBtnAendernCmd = new ActionCommand(OnBtnAendern);
            OnBtnSpeichernCmd = new ActionCommand(OnBtnSpeichern);

            ufvm = App.Current.FindResource("UFBearbeitenVM") as UnterrichtsfachBearbeitenVM;
        }

        private void OnBtnSpeichern(object obj)
        {
            ZF.Unterrichtsfaecher = LstUFach.ToList();
            ZF.Speichern();
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

        public Unterrichtsfach SelFach
        {
            get
            {
                return _selFach;
            }

            set
            {
                _selFach = value;
                OnPropertyChanged();
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
                LstUFach = new ObservableCollection<Unterrichtsfach>(ZF.Unterrichtsfaecher);

                OnPropertyChanged();
                OnPropertyChanged("Bez");
                OnPropertyChanged("Pos");
                OnPropertyChanged("Fachart");
                OnPropertyChanged("AbschliessendesFach");
                OnPropertyChanged("Vorrueckungsfach");
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
            LstUFach.Add(ufvm.UF);
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

        private void SaveAbleChanged()
        {
            OnPropertyChanged("ZFachSaveAble");
        }

        public bool ZFachSaveAble
        {
            get
            {
                if (ZF == null)
                {
                    Trace.WriteLine("Unterrichtsfach NULL!");
                    return false;
                }
                return (ZF.Pos > 0 && !string.IsNullOrWhiteSpace(ZF.Bez));
            }
        }

    }
}

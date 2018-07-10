﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notenmanager.ViewModel
{
    public class DateiImportPageVM : BaseViewModel
    {
        #region Instanzvariablen
        private string _dateiPfad;
        private ComboBoxItem _dateiTyp;
        #endregion

        public DateiImportPageVM()
        {
            
        }

        #region Events
        #endregion

        #region Public Properties
        public string DateiPfad
        {
            get
            {
                return _dateiPfad;
            }

            set
            {
                _dateiPfad = value;
                OnPropertyChanged();
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
                _dateiTyp = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}

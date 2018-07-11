using System;
using System.Collections.Generic;
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
        private string _dateiPfad;
        private ComboBoxItem _dateiTyp;
        #endregion

        public DateiImportPageVM()
        {
            DateiImportierenCmd = new ActionCommand(OnDateiImportieren);
        }

        #region Events
        #endregion

        #region Public Properties
        #region Commands
        public ICommand DateiImportierenCmd { get; set; } 
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
        #endregion

        #region Methoden
        private void OnDateiImportieren(object obj)
        {
            switch(DateiTyp.Name)
            {
                case "Klasse":
                    break;
                case "Schueler":
                    break;
                case "Lehrer":
                    break;
            }
        }
        #endregion
    }
}

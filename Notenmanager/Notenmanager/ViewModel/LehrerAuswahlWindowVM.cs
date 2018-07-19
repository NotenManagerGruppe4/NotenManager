using Notenmanager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notenmanager.ViewModel.Tools;

namespace Notenmanager.ViewModel
{
    public class LehrerAuswahlWindowVM : BaseViewModel
    {
        private ObservableCollection<Lehrer> _lehrerListe = new ObservableCollection<Lehrer>(DBZugriff.Current.Select<Lehrer>());
        private Lehrer _selectedLehrer;

        #region Properties 
        public ObservableCollection<Lehrer> LehrerListe
        {
            get
            {
                return _lehrerListe;
            }
            set
            {
                _lehrerListe = value;
                OnPropertyChanged();
            }
        }

        public Lehrer SelectedLehrer
        {
            get
            {
                return _selectedLehrer;
            }
            set
            {
                _selectedLehrer = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties
        public ICommand OnBtnHinzufuegenCmd { get; set; }

        public LehrerAuswahlWindowVM()
        {
            OnBtnHinzufuegenCmd = new ActionCommand(OnBtnHinzufuegen);
        }

        private void OnBtnHinzufuegen(object obj)
        {
            int idLehrer = SelectedLehrer.Id;

        }
    }
}

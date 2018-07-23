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

        private Unterrichtsfach _selUF;
        public Unterrichtsfach SelUF
        {
            get
            {
                return _selUF;
            }

            set
            {
                _selUF = value;
                OnPropertyChanged("LehrerListe");
            }
        }

        private Lehrer _selectedLehrer;

        #region Properties 
        public List<Lehrer> LehrerListe
        {
            get
            {
                if (SelUF?.UFaecherLehrer == null)
                    return new List<Lehrer>();

                List<Lehrer> alle = DBZugriff.Current.Select<Lehrer>();

                List<Lehrer> vorhanden = new List<Lehrer>();
                foreach(UFachLehrer ufl in SelUF?.UFaecherLehrer)
                {
                    if (!vorhanden.Contains(ufl.Lehrer) && ufl.Lehrer.Active)
                        vorhanden.Add(ufl.Lehrer);
                }

                return alle.Except(vorhanden).ToList();



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

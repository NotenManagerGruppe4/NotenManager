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


        public Unterrichtsfach SelUF { get; set; }

        private Lehrer _selectedLehrer;

        #region Properties 
        public List<Lehrer> LehrerListe
        {
            get
            {
                List<Lehrer> re = new List<Lehrer>();
                foreach (Lehrer l in DBZugriff.Current.Select<Lehrer>())
                    foreach (UFachLehrer ufl in l.UFaecherLehrer)
                        if (ufl.Unterrichtsfach == SelUF)
                            re.Add(l);

                return re;
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

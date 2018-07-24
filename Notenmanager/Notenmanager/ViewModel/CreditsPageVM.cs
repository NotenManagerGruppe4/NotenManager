using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class CreditsPageVM
    {
        public CreditsPageVM()
        {
            CreditsAbbrechenCmd = new Command<string>(OnAbbrechen);
        }

        private void OnAbbrechen(object obj)
        {
             // Zur Hauptseite navigieren
             Navigator.Instance.NavigateTo("MainPage");
        }

        #region CommandHandler

        public ICommand CreditsAbbrechenCmd { get; set; }


        
        #endregion
    }
}

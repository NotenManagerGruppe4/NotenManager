using Notenmanager.View;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class MainPageVM : BaseViewModel
    {
        public MainPageVM()
        { 
            // Comamnds initialisieren:
            NavigationCmd = new Command<string>(OnNavigation);
        }

        

        #region Events
        public event EventHandler<NavigationEventArgs> NavigateToPageRequest;
        #endregion

        #region Public Properties
        #region Commands
        public ICommand NavigateToDateiImportCmd { get; set; }
        public ICommand NavigateToFachAnlegenCmd { get; set; }
        public ICommand NavigationCmd { get; set; }
        

        #endregion
        #endregion

        #region Methoden
        #region CommandHandler
        /// <summary>
        /// Navigiert zur angegebenen Seite, deren Ressourcen-Schlüssel man per CommandParameter(in XAML) übergibt
        /// </summary>
        /// <param name="key">Schlüssel der Seite zu der navigiert werden soll aus dem ResourceDictionary der App.xaml </param>
        private static void OnNavigation(string key)
        {
            Navigator.Instance.NavigateTo(key);
        }
        #endregion
        #endregion
    }
}

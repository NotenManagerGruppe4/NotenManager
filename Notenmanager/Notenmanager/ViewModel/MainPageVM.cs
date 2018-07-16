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
            NavigateToDateiImportCmd = new ActionCommand(OnNavigateToDateiImport);
            NavigateToFachAnlegenCmd = new ActionCommand(OnNavigateToFachAnlegen);
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
        private static void OnNavigation(string key)
        {
            Navigator.Instance.NavigateTo(key);
        }


        private void OnNavigateToDateiImport(object obj)
        {
            NavigateToPageRequest?.Invoke(this, new NavigationEventArgs()
            {
                ZielPage = new DateiImportPage()
            });
        }
        private void OnNavigateToFachAnlegen(object obj)
        {
            NavigateToPageRequest?.Invoke(this, new NavigationEventArgs()
            {
                ZielPage = new FachAnlegenPage()
            });    
        }
        #endregion
        #endregion
    }
}

using Notenmanager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    public class MainPageVM : BaseViewModel
    {
        public MainPageVM()
        {
            NavigateToDateiImportCmd = new ActionCommand(OnNavigateToDateiImport);
            NavigateToDateiImportCmd = new ActionCommand(OnNavigateToFachAnlegen);
        }


        #region Events
        public event EventHandler<NavigationEventArgs> NavigateToPageRequest;
        #endregion

        #region Public Properties
        #region Commands
        public ICommand NavigateToDateiImportCmd { get; set; }
        public ICommand NavigateToFachAnlegenPageCmd { get; set; }
        #endregion
        #endregion

        #region Methoden
        #region CommandHandler
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

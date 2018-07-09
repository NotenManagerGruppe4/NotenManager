using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    class MainPageVM : BaseViewModel
    {
        public MainPageVM()
        {
            NavigateToDateiImportCmd = new ActionCommand(OnNavigateToDateiImport);
        }

        private void OnNavigateToDateiImport(object obj)
        {
            NavigateToPageRequest?.Invoke(this, new NavigationEventArgs()
            {
                
            });            
        }

        #region Events
        public event EventHandler<EventArgs> NavigateToPageRequest;
        #endregion

        #region Public Properties
        #region Commands
        public ICommand NavigateToDateiImportCmd { get; set; }
        #endregion
        #endregion
    }
}

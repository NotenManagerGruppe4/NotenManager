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
    class MainWindowVM : BaseViewModel
    {
        #region Instanzvariablen
        public Page _currentPage;
        #endregion

        #region Public Properties
        /// <summary>
        /// Enthält die aktuell im Hauptfenster angezeigte Seite
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                SetValue(ref _currentPage, value);
            }
        }

        #region Commands
        public ICommand BeendenCmd { get; set; }
        public ICommand StartCmd { get; set; }
        #endregion
        #endregion

        #region Konstruktoren
        public MainWindowVM()
        {
            CurrentPage = App.Current.FindResource("MainPage") as Page;
            BeendenCmd = new ActionCommand(OnBeenden);
            StartCmd = new ActionCommand(OnStart);
        }
        #endregion

        #region HandlerMethoden
        /// <summary>
        /// Navigiert zur Startseite
        /// </summary>
        /// <param name="obj"></param>
        private void OnStart(object obj)
        {
            Navigator.Instance.NavigateTo("MainPage");
        }

        /// <summary>
        /// Beendet die Anwendung
        /// </summary>
        /// <param name="obj">nicht verwendet</param>
        private void OnBeenden(object obj)
        {
            App.Current.Shutdown();
        }
        #endregion
    }
}

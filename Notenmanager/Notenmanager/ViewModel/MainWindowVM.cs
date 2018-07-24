using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notenmanager.ViewModel
{
    class MainWindowVM : BaseViewModel
    {
        #region Instanzvariablen
        private Page _currentPage;

        private Visibility _pBarVisibility = Visibility.Visible;
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
        public ICommand CreditsCmd { get; set; }

        public Visibility PBarVisibility
        {
            get
            {
                return _pBarVisibility;
            }

            set
            {
                _pBarVisibility = value;
                OnPropertyChanged();
                OnPropertyChanged("IsMWEnabled");
            }
        }

        public bool IsMWEnabled
        {
            get
            {
                return PBarVisibility != Visibility.Visible;
            }
        }


        #endregion
        #endregion

        #region Konstruktoren
        public MainWindowVM()
        {
            CurrentPage = App.Current.FindResource("MainPage") as Page;
            BeendenCmd = new ActionCommand(OnBeenden);
            StartCmd = new ActionCommand(OnStart);
            CreditsCmd = new ActionCommand(OnCredits);

            Navigator.Instance.PageChanged += (s, e) =>
            {
                PBarVisibility = Visibility.Visible;
            };
            Navigator.Instance.PageChangedFinished += (s, e) =>
            {
                PBarVisibility = Visibility.Hidden;
            };
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
            MessageBoxResult mbr = MessageBox.Show("Notenmanager Beenden?", "Wirklich schließen?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(mbr == MessageBoxResult.Yes)
                App.Current.Shutdown();
        }

        /// <summary>
        /// Navigiert zur Credits-Page
        /// </summary>
        /// <param name="obj"></param>
        private void OnCredits(object obj)
        {
            Navigator.Instance.NavigateTo("CreditsPage");
        }
        #endregion
    }
}

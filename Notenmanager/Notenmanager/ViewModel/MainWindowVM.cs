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
        public Page _currentPage;


        public ICommand BeendenCmd { get; set; }
        public ICommand StartCmd { get; set; }

        public MainWindowVM()
        {
            CurrentPage = App.Current.FindResource("MainPage") as Page;
            BeendenCmd = new ActionCommand(OnBeenden);
            StartCmd = new ActionCommand(OnStart);
        }
        private void OnStart(object obj)
        {
            Navigator.Instance.NavigateTo("MainPage");
        }
        private void OnBeenden(object obj)
        {
            App.Current.Shutdown();
        }

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
    }
}

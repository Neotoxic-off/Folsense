using Folsense.Bases;
using Folsense.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Folsense.ViewModels
{
    public class MainViewModel : BaseClass
    {
        private Dictionary<Type, UserControl> _viewInstances;
        public Dictionary<Type, UserControl> ViewInstances
        {
            get { return _viewInstances; }
            set { SetProperty(ref _viewInstances, value); }
        }

        private UserControl? _currentView;

        public UserControl? CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        public DelegateCommand NavigateCommand { get; set; }

        public MainViewModel()
        {
            NavigateCommand = new DelegateCommand(Navigate);
            ViewInstances = new Dictionary<Type, UserControl>()
            {
                { typeof(DashboardView), new DashboardView() },
                { typeof(AccountView), new AccountView() },
                { typeof(ManagerView), new ManagerView() },
                { typeof(SettingsView), new SettingsView() }
            };
            CurrentView = ViewInstances[typeof(DashboardView)];
        }

        private void Navigate(object viewType)
        {
            CurrentView = ViewInstances[(Type)viewType];
        }
    }
}

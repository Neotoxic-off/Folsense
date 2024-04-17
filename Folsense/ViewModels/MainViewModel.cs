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
        private Dictionary<Type, Tuple<UserControl, bool>> _viewInstances;
        public Dictionary<Type, Tuple<UserControl, bool>> ViewInstances
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
            ViewInstances = new Dictionary<Type, Tuple<UserControl, bool>>()
            {
                {
                    typeof(DashboardView),
                    new Tuple<UserControl, bool>(
                        new DashboardView(), false
                    )
                },
                {
                    typeof(VaultView),
                    new Tuple<UserControl, bool>(
                        new VaultView(), true
                    )
                },
                {
                    typeof(AccountView),
                    new Tuple<UserControl, bool>(
                        new AccountView(), false
                    )
                },
                {
                    typeof(StatisticsView),
                    new Tuple<UserControl, bool>(
                        new StatisticsView(), false
                    )
                },
                {
                    typeof(SettingsView),
                    new Tuple<UserControl, bool>(
                        new SettingsView(), false
                    )
                }
            };
            CurrentView = ViewInstances[typeof(DashboardView)].Item1;
        }

        private void Navigate(object viewType)
        {
            CurrentView = ViewInstances[(Type)viewType].Item1;
        }
    }
}

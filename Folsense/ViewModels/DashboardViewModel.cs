using Folsense.Bases;
using Folsense.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Folsense.ViewModels
{
    public class DashboardViewModel : BaseClass
    {
        private DashboardModel? _dashboardModel;
        public DashboardModel? dashboardModel
        {
            get { return _dashboardModel; }
            set { SetProperty(ref _dashboardModel, value); }
        }

        public DashboardViewModel()
        {
            dashboardModel = new DashboardModel();
        }
    }
}

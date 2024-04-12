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

using Newtonsoft;

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

        private DatabaseModel? _databaseModel;
        public DatabaseModel? databaseModel
        {
            get { return _databaseModel; }
            set { SetProperty(ref _databaseModel, value); }
        }

        public DashboardViewModel()
        {
            dashboardModel = new DashboardModel();
            databaseModel = new DatabaseModel();

            LoadDatabase();
        }

        private DatabaseModel? LoadDatabase()
        {
            FileModel? file = new FileModel(ISettings.Database, false);

            if (file.Exists == true)
            {
                databaseModel = null;
            }
        }
    }
}

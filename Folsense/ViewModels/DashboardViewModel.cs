using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Newtonsoft.Json;
using Folsense.Models.IO;
using System.IO;
using Folsense.Tools;

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

        private DatabaseManager? _databaseManager;
        public DatabaseManager? databaseManager
        {
            get { return _databaseManager; }
            set { SetProperty(ref _databaseManager, value); }
        }

        public DashboardViewModel()
        {
            dashboardModel = new DashboardModel();
            databaseModel = new DatabaseModel();
            databaseManager = new DatabaseManager();

            LoadDatabase();
        }

        private void LoadDatabase()
        {
            databaseModel.Content = databaseManager.Get();
        }
    }
}

using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Folsense.Models;

namespace Folsense.ViewModels
{
    public class SettingsViewModel : BaseClass
    {
        private SettingsModel? _settingsModel;
        public SettingsModel? settingsModel
        {
            get { return _settingsModel; }
            set { SetProperty(ref _settingsModel, value); }
        }

        public SettingsViewModel()
        {
            settingsModel = new SettingsModel();
        }
    }
}

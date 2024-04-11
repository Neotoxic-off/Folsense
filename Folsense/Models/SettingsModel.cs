using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models
{
    public class SettingsModel : BaseClass
    {
        private string? _processName;
        public string? ProcessName
        {
            get { return _processName; }
            set { SetProperty(ref _processName, value); }
        }

        private string? _modName;
        public string? ModName
        {
            get { return _modName; }
            set { SetProperty(ref _modName, value); }
        }

    }
}

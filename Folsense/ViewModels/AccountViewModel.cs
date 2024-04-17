using Folsense.Bases;
using Folsense.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Folsense.ViewModels
{
    public class AccountViewModel : BaseClass
    {
        private AccountModel? _accountModel;
        public AccountModel? accountModel
        {
            get { return _accountModel; }
            set { SetProperty(ref _accountModel, value); }
        }

        public AccountViewModel()
        {
            accountModel = new AccountModel();

            LoadAccount();
        }

        private void LoadAccount()
        {
            accountModel.MachineName = Environment.MachineName;
            accountModel.Username = Environment.UserName;
            accountModel.Databases = ISettings.Database.Path;
            accountModel.DatabasesCount = Directory.GetFiles(ISettings.Database.Path).Length;
        }
    }
}

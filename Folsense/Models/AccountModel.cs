using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models
{
    public class AccountModel : BaseClass
    {
        private string? _username;
        public string? Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string? _machineName;
        public string? MachineName
        {
            get { return _machineName; }
            set { SetProperty(ref _machineName, value); }
        }

        private string? _databases;
        public string? Databases
        {
            get { return _databases; }
            set { SetProperty(ref _databases, value); }
        }

        private int? _databasesCount;
        public int? DatabasesCount
        {
            get { return _databasesCount; }
            set { SetProperty(ref _databasesCount, value); }
        }
    }
}

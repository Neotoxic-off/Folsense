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

        private string? _id;
        public string? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

    }
}

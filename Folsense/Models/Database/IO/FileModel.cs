using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models.Database.IO
{
    public class FileModel : BaseIOClass
    {
        private string? _id;
        public string? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private byte[]? _data;
        public byte[]? Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }
    }
}

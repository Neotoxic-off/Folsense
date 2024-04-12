using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models.Database
{
    public class DatabaseModel : BaseClass
    {
        private FileModel? _file;
        public FileModel? File
        {
            get { return _file; }
            set { SetProperty(ref _file, value); }
        }

        private string? _id;
        public string? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private ObservableCollection<BaseIOClass>? _content;
        public ObservableCollection<BaseIOClass>? Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public DatabaseModel()
        {
            
        }
    }
}

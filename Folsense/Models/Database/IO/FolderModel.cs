using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models.Database.IO
{
    public class FolderModel : BaseIOClass
    {
        private string? _id;
        public string? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private ObservableCollection<FileModel>? _files;
        public ObservableCollection<FileModel>? Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }
    }
}

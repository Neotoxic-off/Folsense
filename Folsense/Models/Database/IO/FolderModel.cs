using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Folsense.Models.Database.IO
{
    public class FolderModel : BaseIOClass
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string? _path;
        public string? Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        private Guid? _id;
        override public Guid? Id
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

        public FolderModel(string path)
        {
            Id = Guid.NewGuid();
            Name = System.IO.Path.GetFileName(path);
            Path = path;
            Date = DateTime.Now;
        }
    }
}

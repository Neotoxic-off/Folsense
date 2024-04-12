using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models.IO
{
    public class FileModel : BaseClass
    {
        public string? _path;
        public string? Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        public bool? _exists;
        public bool? Exists
        {
            get { return _exists; }
            set { SetProperty(ref _exists, value); }
        }

        public bool? _force;
        public bool? Force
        {
            get { return _force; }
            set { SetProperty(ref _force, value); }
        }

        public FileModel(string path, bool force)
        {
            this.Path = path;
            this.Exists = File.Exists(path);
            this.Force = force;

            Create();
        }

        private void Create()
        {
            if (this.Exists == false && Force == true)
            {
                File.Create(this.Path);
            }
        }
    }
}

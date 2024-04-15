using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Folsense.Models.Database.IO
{
    public class FileModel : BaseIOClass
    {
        private Guid? _id;
        override public Guid? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

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

        private string? _extension;
        public string? Extension
        {
            get { return _extension; }
            set { SetProperty(ref _extension, value); }
        }

        private byte[]? _data;
        public byte[]? Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        public FileModel()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public FileModel(string path)
        {
            Id = Guid.NewGuid();
            Path = path;
            Extension = System.IO.Path.GetExtension(path);
            Name = System.IO.Path.GetFileName(path);
            Data = Tools.Security.Encrypt(LoadData(path));
            Date = DateTime.Today;
        }

        private byte[] LoadData(string path)
        {
            byte[] buffer = new byte[ISettings.HeaderSize];

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bytes_read = fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }

            return (buffer);
        }
    }
}

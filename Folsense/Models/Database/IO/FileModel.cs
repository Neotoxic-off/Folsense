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
using NReco.VideoConverter;

namespace Folsense.Models.Database.IO
{
    public class FileModel : BaseIOClass
    {
        private Guid? _id;
        public Guid? Id
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

        private BitmapSource? _thumbnail;
        public BitmapSource? Thumbnail
        {
            get { return _thumbnail; }
            set { SetProperty(ref _thumbnail, value); }
        }

        public FileModel()
        {
        }

        public FileModel(string path)
        {
            Id = Guid.NewGuid();
            Extension = Path.GetExtension(path);
            Name = Path.GetFileName(path);
            Data = File.ReadAllBytes(path);

            LoadThumbnail(path);
        }

        private void LoadThumbnail(string path)
        {
            FFMpegConverter ffMpeg = new FFMpegConverter();
            string cache = $"{ISettings.Root.Path}\\cache.tmp";
            string cache_thumbnail = $"{ISettings.Root.Path}\\cache.jpg";

            if (ISettings.Videos.Contains(Extension) == true)
            {
                if (Extension != "mp4")
                {
                    ffMpeg.ConvertMedia(path, cache, Format.mp4);
                    ffMpeg.GetVideoThumbnail(cache, cache_thumbnail);
                } else
                {
                    ffMpeg.GetVideoThumbnail(path, cache_thumbnail);
                }

                if (File.Exists(cache) == true)
                    File.Delete(cache);

                Thumbnail = ConvertByteArrayToBitmapSource(File.ReadAllBytes(cache_thumbnail));
            } else
            {
                Thumbnail = ConvertByteArrayToBitmapSource(Data);
            }
        }

        private BitmapSource ConvertByteArrayToBitmapSource(byte[] imageData)
        {
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}

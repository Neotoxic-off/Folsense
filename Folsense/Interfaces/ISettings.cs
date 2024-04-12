using Folsense.Bases;
using Folsense.Models.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models
{
    public interface ISettings
    {
        public static string Extension = "fd";
        public static FolderModel Root = new FolderModel(
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Folsense",
            true
        );
        public static FileModel Database = new FileModel(
            $"{Root.Path}\\Database.{Extension}",
            false
        );
        public static string[] Videos = new string[]
        {
            ".mp4",
            ".mov",
            ".mkv",
            ".avi"
        };
        public static string[] Images = new string[]
        {
            ".png",
            ".jpg",
            ".gif"
        };
    }
}

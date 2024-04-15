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
        public static FolderModel Database = new FolderModel(
            $"{Root.Path}\\Database",
            true
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
        public static FolderModel Download = new FolderModel(
            $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Folsense",
            true
        );
        public static byte[] Key = new byte[] {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10,
            0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x20,
            0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x40,
            0x51, 0x52
        };
        public static byte[] IV = new byte[] {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10,
            0x21, 0x22, 0x23, 0x24, 0x25, 0x26
        };
    }
}

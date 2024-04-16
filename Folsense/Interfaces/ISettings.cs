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
        public static byte[] Key = new byte[] {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
        public static string Extension = "fd";
        public static FolderModel Root = new FolderModel(
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Folsense",
            true
        );
        public static FolderModel Database = new FolderModel(
            $"{Root.Path}\\Database",
            true
        );
        public static FileModel Changelog = new FileModel(
            $"changelog.md",
            false
        );
        public static FolderModel Cache = new FolderModel(
            $"{Root.Path}\\Cache",
            true
        );
        public static int HeaderSize = 256;
    }
}

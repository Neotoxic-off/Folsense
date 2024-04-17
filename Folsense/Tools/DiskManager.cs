using Folsense.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Folsense.Tools
{
    public class DiskManager
    {
        public static long GetDatabaseStorage()
        {
            string[] databases = Directory.GetFiles(ISettings.Database.Path, $"*.{ISettings.Extension}");
            FileInfo info = null;
            long total = 0;

            foreach (string database in databases)
            {
                info = new FileInfo(database);

                total += info.Length / 1000;
            }

            return (total);
        }

        public static long GetUserStorage()
        {
            DriveInfo di = new DriveInfo(Path.GetPathRoot(ISettings.Database.Path));

            MessageBox.Show(Path.GetPathRoot(ISettings.Database.Path));

            if (di.IsReady)
            {
                return (di.TotalSize / 1000);
            }

            return (0);
        }
    }
}

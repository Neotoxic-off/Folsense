using Folsense.Bases;
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
        public static string Root = new FolderModel(
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Folsense",
            true
        );
        public static string Database = new FolderModel(
            $"{ISettings.Root}\\Database.{ISettings.Extension}",
            true
        );
    }
}

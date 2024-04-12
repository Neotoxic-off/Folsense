using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database.IO;
using Folsense.Services;
using Folsense.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Folsense.ViewModels
{
    public class ManagerViewModel : BaseClass
    {
        private DelegateCommand _addContentCommand;
        public DelegateCommand AddContentCommand
        {
            get { return _addContentCommand; }
            set { SetProperty(ref _addContentCommand, value); }
        }

        private DatabaseManager? _databaseManager;
        public DatabaseManager? databaseManager
        {
            get { return _databaseManager; }
            set { SetProperty(ref _databaseManager, value); }
        }

        private OpenFolderDialog _dialog;
        public OpenFolderDialog Dialog
        {
            get { return _dialog; }
            set { SetProperty(ref _dialog, value); }
        }

        public ManagerViewModel()
        {
            databaseManager = new DatabaseManager();

            AddContentCommand = new DelegateCommand(AddContent);
            Dialog = new OpenFolderDialog();
        }

        private void AddContent(object data)
        {
            bool? status = Dialog.ShowDialog();

            if (status == true)
            {
                Inject(
                    Directory.GetFiles(Dialog.FolderName)
                );
            }
        }

        private void Inject(string[] items)
        {
            foreach (string item in items)
            {
                databaseManager.Add(new FileModel(item));
            }
        }
    }
}

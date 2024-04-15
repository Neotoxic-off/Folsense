using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Folsense.Models.IO;
using System.IO;
using Folsense.Tools;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using Folsense.Models.Database.IO;

namespace Folsense.ViewModels
{
    public class DashboardViewModel : BaseClass
    {
        private DashboardModel? _dashboardModel;
        public DashboardModel? dashboardModel
        {
            get { return _dashboardModel; }
            set { SetProperty(ref _dashboardModel, value); }
        }

        private DatabaseModel? _databaseModel;
        public DatabaseModel? databaseModel
        {
            get { return _databaseModel; }
            set { SetProperty(ref _databaseModel, value); }
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

        private DelegateCommand _addContentCommand;
        public DelegateCommand AddContentCommand
        {
            get { return _addContentCommand; }
            set { SetProperty(ref _addContentCommand, value); }
        }

        private DelegateCommand _downloadContentCommand;
        public DelegateCommand DownloadContentCommand
        {
            get { return _downloadContentCommand; }
            set { SetProperty(ref _downloadContentCommand, value); }
        }

        private DelegateCommand _deleteContentCommand;
        public DelegateCommand DeleteContentCommand
        {
            get { return _deleteContentCommand; }
            set { SetProperty(ref _deleteContentCommand, value); }
        }

        private string _encryptionKey;
        public string EncryptionKey
        {
            get { return _encryptionKey; }
            set { SetProperty(ref _encryptionKey, value); }
        }


        public DashboardViewModel()
        {
            dashboardModel = new DashboardModel();
            databaseModel = new DatabaseModel();
            databaseManager = new DatabaseManager();

            AddContentCommand = new DelegateCommand(AddContent);
            DownloadContentCommand = new DelegateCommand(DecryptContent);
            DeleteContentCommand = new DelegateCommand(DeleteContent);

            Dialog = new OpenFolderDialog();

            LoadDatabase();
        }

        private void LoadDatabase()
        {
            databaseModel.Content = databaseManager.Get();
        }

        private void AddContent(object data)
        {
            bool? status = Dialog.ShowDialog();
            Models.Database.IO.FolderModel folder = null;

            if (status == true)
            {
                folder = new Models.Database.IO.FolderModel(Dialog.FolderName);
                folder.Id = Guid.NewGuid();
                folder.Files = Convert(
                    Directory.GetFiles(Dialog.FolderName, "*.*", SearchOption.AllDirectories)
                );

                databaseManager.Add(folder);

                databaseModel.Content = databaseManager.Get();
            }
        }

        private ObservableCollection<Models.Database.IO.FileModel> Convert(string[] files)
        {
            ObservableCollection<Models.Database.IO.FileModel> converted = new ObservableCollection<Models.Database.IO.FileModel>();

            foreach (string file in files)
            {
                converted.Add(new Models.Database.IO.FileModel(file));
            }

            return (converted);
        }

        private void DecryptContent(object data)
        {
            Guid id = (Guid)data;
            Models.Database.IO.FolderModel item = (Models.Database.IO.FolderModel)databaseManager.Retrieve(id);
            Models.IO.FileModel output = new Models.IO.FileModel($"{ISettings.Download.Path}\\cache.tmp", true);

            foreach (Models.Database.IO.FileModel file in item.Files)
            {
                using (FileStream tempFile = new FileStream(output, FileMode.Create))
                {
                    tempFile.Write(Security.Decrypt(file.Data), 0, file.Data.Length);

                    using (FileStream originalFile = new FileStream(filePath, FileMode.Open))
                    {
                        originalFile.Seek(512, SeekOrigin.Begin);
                        originalFile.CopyTo(tempFile);
                    }
                }

                File.Delete(filePath);
                File.Move(output, filePath);
            }
        }

        private void DeleteContent(object data)
        {
            Guid id = (Guid)data;
            databaseManager.Delete(id);

            databaseModel.Content = databaseManager.Get();
        }
    }
}

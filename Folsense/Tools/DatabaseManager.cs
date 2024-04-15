using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database.IO;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Folsense.Tools
{
    public class DatabaseManager
    {
        public static readonly string DatabaseFolder = ISettings.Database.Path;

        private string PathBuilder(Guid? databaseId)
        {
            return ($"{DatabaseFolder}\\{databaseId}.{ISettings.Extension}");
        }

        public void Add(BaseIOClass content)
        {
            ILiteCollection<BaseIOClass>? collection = null;

            using (var db = new LiteDatabase(PathBuilder(content.Id)))
            {
                collection = db.GetCollection<BaseIOClass>("vault");
                collection.Insert(content);
                collection.Update(content);
            }
        }

        public void Delete(Guid id)
        {
            LiteDatabase db = new LiteDatabase(PathBuilder(id));

            db.DropCollection("vault");
            db.Dispose();

            File.Delete(PathBuilder(id));
        }

        public ObservableCollection<BaseIOClass> Get()
        {
            ObservableCollection<BaseIOClass> global = new ObservableCollection<BaseIOClass>();
            ILiteCollection<BaseIOClass>? collection = null;
            LiteDatabase db = null;

            foreach (string database in Directory.GetFiles(DatabaseFolder, $"*.{ISettings.Extension}"))
            {
                if (database.Contains("log") == false)
                {
                    db = new LiteDatabase(database);
                    collection = db.GetCollection<BaseIOClass>("vault");
                    foreach (BaseIOClass baseio in Convert(collection))
                    {
                        global.Add(baseio);
                    }

                    db.Dispose();
                }
            }
            
            return (global);
        }

        public BaseIOClass Retrieve(Guid id)
        {
            BaseIOClass result = new BaseIOClass();
            ILiteCollection<BaseIOClass>? collection = null;

            using (LiteDatabase db = new LiteDatabase(PathBuilder(id)))
            {
                collection = db.GetCollection<BaseIOClass>("vault");
                result = collection.FindOne(x => x.Id == id);
            }

            return (result);
        }

        public ObservableCollection<BaseIOClass> Convert(ILiteCollection<BaseIOClass> collection)
        {
            ObservableCollection<BaseIOClass> data = new ObservableCollection<BaseIOClass>();

            foreach (BaseIOClass item in collection.FindAll())
            {
                data.Add(item);
            }

            return (data);
        }
    }
}

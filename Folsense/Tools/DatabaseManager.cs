using Folsense.Bases;
using Folsense.Models;
using Folsense.Models.Database.IO;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Folsense.Tools
{
    public class DatabaseManager
    {
        public void Add(BaseIOClass content)
        {
            ILiteCollection<BaseIOClass>? collection = null;

            using (var db = new LiteDatabase(ISettings.Database.Path))
            {
                collection = db.GetCollection<BaseIOClass>("vault");
                collection.Insert(content);
                collection.EnsureIndex(x => x.Id);
                collection.Update(content);
            }
        }

        public void Delete(Guid id)
        {
            BaseIOClass item = null;
            ILiteCollection<BaseIOClass>? collection = null;

            using (var db = new LiteDatabase(ISettings.Database.Path))
            {
                collection = db.GetCollection<BaseIOClass>("vault");
                item = collection.FindOne(x => x.Id == id);
                collection.Delete(item.Id);
            }
        }

        public ObservableCollection<BaseIOClass> Get()
        {
            ObservableCollection<BaseIOClass> converted = new ObservableCollection<BaseIOClass>();
            ILiteCollection<BaseIOClass>? collection = null;

            using (var db = new LiteDatabase(ISettings.Database.Path))
            {
                collection = db.GetCollection<BaseIOClass>("vault");
                converted = Convert(collection);
            }
            
            return (converted);
        }

        public BaseIOClass Retrieve(Guid id)
        {
            BaseIOClass result = new BaseIOClass();
            ILiteCollection<BaseIOClass>? collection = null;

            using (var db = new LiteDatabase(ISettings.Database.Path))
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

using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models.Database
{
    public class DatabaseModel : BaseClass
    {
        private Guid? _id;
        public Guid? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private ObservableCollection<BaseIOClass>? _content;
        public ObservableCollection<BaseIOClass>? Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public DatabaseModel()
        {
            Id = Guid.NewGuid();
            Content = new ObservableCollection<BaseIOClass>();
        }
    }
}

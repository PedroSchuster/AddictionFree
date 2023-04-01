using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Entidades;
using AddictionApp.Services;

namespace AddictionApp.Data
{
    public sealed class DataContainer
    {
        private static DataContainer _dataContainer;
        public static DataContainer Instance
        {
            get
            {
                if (_dataContainer == null)
                {
                    _dataContainer = new DataContainer();
                }
                return _dataContainer;
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
        }

        private IList<Note> _notes;
        public IList<Note> Notes
        {
            get { return _notes; }
        }

        private DataContainer() { }

        public void Initialize(Addiction addiction)
        {
            _id = addiction.Id;
            _name = addiction.Name;
            _date = addiction.Date;
            _notes = addiction.Notes;
        }
    }
}

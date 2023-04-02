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

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        private DateTime _lastResetDate;
        public DateTime LastResetDate
        {
            get { return _lastResetDate; }
            set
            {
                _lastResetDate = value;
                Addiction.LastResetDate = LastResetDate;
                UpdateData();
            }
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                Addiction.Notes = Notes;
                UpdateData();

            }
        }

        private Addiction _addiction;
        public Addiction Addiction
        {
            get { return _addiction; }
            set
            {
                _addiction = value;
            }
        }

        private DataContainer() { }

        public void Initialize(Addiction addiction)
        {
            Addiction = addiction;
            _id = addiction.Id;
            _name = addiction.Name;
            _creationDate = addiction.CreationDate;
            _lastResetDate = addiction.LastResetDate;
            _notes = addiction.Notes;
        }

        private async void UpdateData()
        {
            AddictionService a = new AddictionService();
            await a.UpdateAsync(Addiction);
        }
    }
}

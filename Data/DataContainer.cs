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
        }

        public async void UpdateData()
        {
            AddictionService a = new AddictionService();
            await a.UpdateAsync(Addiction);
        }
    }
}

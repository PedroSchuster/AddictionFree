using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Entidades;
using AddictionApp.Services;

namespace AddictionApp.ViewModels
{
    public class HomePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Addiction> _addictions = new ObservableCollection<Addiction>();
        public ObservableCollection<Addiction> Addictions
        {
            get { return _addictions; }
            set
            {
                _addictions = value;
                OnPropertyChanged(nameof(Addictions));
            }
        }

        public HomePageVM() 
        {
            GetData();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetData()
        {
            AddictionService addictionService = new AddictionService();
            Addictions = await addictionService.ToListAsync();
        }
    }
}

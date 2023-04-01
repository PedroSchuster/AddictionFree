using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Data;
using AddictionApp.Entidades;

namespace AddictionApp.ViewModels
{
    public class ProgressPage
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public ProgressPage()
        {
            Name = DataContainer.Instance.Name;
            Date = DataContainer.Instance.Date;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Entidades;

namespace AddictionApp.ViewModels
{
    public class NotesPageVM : INotifyPropertyChanged
    {
        private static Command openCalendar;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ButtonLabel> _dates = new ObservableCollection<ButtonLabel> {
            new ButtonLabel{ Text = "28 \nTer", Command = openCalendar},
            new ButtonLabel{ Text = "29 \n Qua", Command = openCalendar},
            new ButtonLabel{ Text = "30 \n Qui", Command = openCalendar},
            new ButtonLabel{ Text = "31 \n Sex", Command = openCalendar},
            new ButtonLabel{ Text = "01 \n Sab", Command = openCalendar},
            new ButtonLabel{ Text = "02 \n Dom", Command = openCalendar},
            new ButtonLabel{ Text = "03 \n Seg", Command = openCalendar},
            new ButtonLabel{ Text = "04 \n Ter", Command = openCalendar},
            new ButtonLabel{ Text = "05 \n Qua", Command = openCalendar},
            new ButtonLabel{ Text = "06 \n Qui", Command = openCalendar},


        };
        public ObservableCollection<ButtonLabel> Dates
        {
            get { return _dates; }
            set
            {
                _dates = value;
                OnPropertyChanged(nameof(Dates));
            }
        }
        public NotesPageVM()
        {
            openCalendar = new Command(OpenCalendar);
        }



        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenCalendar()
        {

        }
    }


}

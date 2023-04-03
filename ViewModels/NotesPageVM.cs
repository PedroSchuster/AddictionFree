using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddictionApp.Entidades;

namespace AddictionApp.ViewModels
{
    public class NotesPageVM : INotifyPropertyChanged
    {
        public Command OpenCalendarCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Label> _dates = new ObservableCollection<Label>();
        public ObservableCollection<Label> Dates
        {
            get { return _dates; }
            set
            {
                _dates = value;
                OnPropertyChanged(nameof(Dates));
            }
        }

        private Button _selectedItem;
        public Button SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _month;
        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        public NotesPageVM()
        {
            Dates.Add(new Label() { Text= DateTime.UtcNow.AddDays(-1).ToString()});
            UpdateCollectionView(1);

            OpenCalendarCommand = new Command(OpenCalendar); // n funciona https://github.com/dotnet/maui/issues/8895
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenCalendar()
        {
            Application.Current.MainPage.DisplayAlert("Calendario", SelectedItem.Text, "Fechar");
        }

        public void UpdateCollectionView(int mag)
        {
            if (mag == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dates.Add(new Label() { Text = (ConvertToDate(Dates[Dates.Count - 1].Text).Date.AddDays(1).ToString()) } ) ;
                }
            }
            else if (mag == -1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dates.Insert(0, new Label() { Text = (ConvertToDate(Dates[0].Text).Date.AddDays(-1).ToString()) });
                }
            }
        }

        private DateTime ConvertToDate(string date)
        {
            return DateTime.Parse(date);
        }

        public void UpdateMounth(int index)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            Month = culture.DateTimeFormat.GetMonthName(ConvertToDate(Dates[index].Text).Month);

        }
    }


}

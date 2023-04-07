using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddictionApp.Data;
using AddictionApp.Entidades;
using AddictionApp.Services;

namespace AddictionApp.ViewModels
{
    public class NotesPageVM : INotifyPropertyChanged
    {
        private AddicitionNotesService a;
        private AddictionNote? note;

        public ICommand OpenCalendarCommand { get; set; }
        public ICommand UpdateCommand{ get; set; }

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

        private Label _selectedItem;
        public Label SelectedItem
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

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private string _buttonText = "Salvar";
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private bool _isEditEnable = true;
        public bool IsEditEnable
        {
            get { return _isEditEnable; }
            set
            {
                _isEditEnable = value;
                OnPropertyChanged(nameof(IsEditEnable));
            }
        }

        private bool _isButtonVisible = false;
        public bool IsButtonVisible
        {
            get { return _isButtonVisible; }
            set
            {
                _isButtonVisible = value;
                OnPropertyChanged(nameof(IsButtonVisible));
            }
        }


        public NotesPageVM()
        {
            a = new AddicitionNotesService();

            Dates.Add(new Label() { Text= DateTime.UtcNow.AddDays(-1).ToString()});
            UpdateCollectionView(1);
            UpdateMonth(1);
            SelectedItem = Dates[1];
            OpenCalendarCommand = new Command(OpenCalendar); // n funciona https://github.com/dotnet/maui/issues/8895
            UpdateCommand = new Command(UpdateNote);
        }

        public async void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(SelectedItem))
            {


                DateTime selectedDate = ConvertToDate(SelectedItem.Text);

                note = await a.FindByDate(DataContainer.Instance.Addiction.Id, selectedDate);


                if (note != null)
                {
                    Text = note.Note;
                    IsEditEnable = false;
                    ButtonText = "Editar";
                }
                else
                {
                    Text = "";
                    IsEditEnable = true;
                    ButtonText = "Salvar";
                }

            }

            if (propertyName == nameof(Text))
            {
                if (!String.IsNullOrWhiteSpace(Text))
                {
                    IsButtonVisible = true;
                }
                else
                {
                    IsButtonVisible = false;
                }
            }

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

        public void UpdateMonth(int index)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            Month = culture.DateTimeFormat.GetMonthName(ConvertToDate(Dates[index].Text).Month);

        }

        private async void UpdateNote()
        {
            if (IsEditEnable)
            {
                DateTime selectedDate = ConvertToDate(SelectedItem.Text);

                if (note != null)
                {
                    note.Note = Text;
                    await a.UpdateAsync(note);
                }

                else
                    await a.InsertAsync(new AddictionNote() { Note = Text, AddictionId = DataContainer.Instance.Addiction.Id, Date = selectedDate});


                IsEditEnable = false;
                ButtonText = "Editar";
            }
            else
            {
                IsEditEnable = true;
                ButtonText = "Salvar";

            }

        }



    }


}

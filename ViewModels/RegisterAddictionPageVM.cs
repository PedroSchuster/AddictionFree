using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AddictionApp.Entidades;
using AddictionApp.Services;

namespace AddictionApp.ViewModels
{
    public class RegisterAddictionPageVM : INotifyPropertyChanged
    {

        private List<OptionContainer> _options = new List<OptionContainer>();
        public List<OptionContainer> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged(nameof(Options));
            }
        }

        private OptionContainer? _selectedItem;
        public OptionContainer? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

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

        private string _option;
        public string Option
        {
            get { return _option; }
            set
            {
                _option = value;
                OnPropertyChanged(nameof(Option));
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        private bool _isMoneySelected;
        public bool IsMoneySelected
        {
            get { return _isMoneySelected; }
            set
            {
                _isMoneySelected = value;
                OnPropertyChanged(nameof(IsMoneySelected));
            }
        }

        private float _money;
        public float Money
        {
            get { return _money; }
            set
            {
                _money = value;
                OnPropertyChanged(nameof(Money));
            }
        }

        private TimeSpan _time = TimeSpan.Zero;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RegisterCommand { get; set; }

        public RegisterAddictionPageVM()
        {
            RegisterCommand = new Command(RegisterAddiction);
            Options.Add(new OptionContainer { Title = "Dinheiro", Description = "Esse vicio/habito ruim te custa dinheiro." });
            Options.Add(new OptionContainer { Title = "Tempo", Description = "Esse vicio/habito ruim faz você perder tempo." });
            Options.Add(new OptionContainer { Title = "Evento", Description = "Esse vicio/habito ruim não te faz necessariamente perder dinheiro ou tempo, porém o evento em si é ruim." });
            OnPropertyChanged(nameof(Options));

        }

        private async void RegisterAddiction()
        {
            AddictionService a = new AddictionService();

            if (Time != TimeSpan.Zero)
            {
                await a.InsertAsync(new Addiction { Name =  Name, CreationDate = DateTime.Now, 
                    LastResetDate = DateTime.Now, Option = Option, WastedTime = Time, WastedMoney = 0});
            }
            else if (Money > 0)
            {
                await a.InsertAsync(new Addiction
                {
                    Name = Name,
                    CreationDate = DateTime.Now,
                    LastResetDate = DateTime.Now,
                    Option = Option,
                    WastedTime = TimeSpan.Zero,
                    WastedMoney = Money
                });
            }
        }

        private void ChangeOption()
        {
            try
            {
                if (SelectedItem != null)
                {
                    switch (SelectedItem.Value.Title)
                    {
                        case "Dinheiro":
                            IsVisible = true;
                            Option = "Dinheiro";
                            IsMoneySelected = true;
                            Time = TimeSpan.Zero;
                            break;
                        case "Tempo":
                            IsVisible = true;
                            Option = "Tempo";
                            IsMoneySelected = false;
                            break;
                        case "Evento":
                            IsVisible = false;
                            Option = "Evento";
                            IsMoneySelected = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
      
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
            if(propertyName == nameof(SelectedItem))
                ChangeOption();

        }

        public struct OptionContainer
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
                                                                                                
}

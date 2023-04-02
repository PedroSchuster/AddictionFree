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

namespace AddictionApp.ViewModels
{
    public class ProgressPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Thread UpdateData;
        public CancellationTokenSource Source = new CancellationTokenSource();

        public ICommand ResetCommand { get; set; }

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

        private string _goal = "3 DIAS";
        public string Goal
        {
            get { return _goal; }
            set
            {
                _goal = value;
                OnPropertyChanged(nameof(Goal));
            }
        }

        private string _timeSpanText;
        public string TimeSpanText
        {
            get { return _timeSpanText; }
            set
            {
                _timeSpanText = value;
                OnPropertyChanged(nameof(TimeSpanText));
            }
        }



        public ProgressPageVM()
        {

            ResetCommand = new Command(() =>
            {
                DataContainer.Instance.LastResetDate = DateTime.Now;
                RefreshData();
            });

            UpdateData = new Thread(() =>
            {
                UpdateData.IsBackground = true;
                CalculateData(Source.Token);
            });

        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RefreshData()
        {
            Name = DataContainer.Instance.Name;
            Date = DataContainer.Instance.LastResetDate;
        }

        private  void CalculateData(CancellationToken token)
        {

            RefreshData();
            while (!token.IsCancellationRequested)
            {
                TimeSpan ts = DateTime.Now - Date;
                TimeSpanText = String.Format("{0}D {1}H {2}min {3}s", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                Thread.Sleep(1000);
            }
        }
    }
}

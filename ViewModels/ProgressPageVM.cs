﻿using System;
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
using Syncfusion.Maui.Calendar;

namespace AddictionApp.ViewModels
{
    public class ProgressPageVM : INotifyPropertyChanged
    {
        private ResetDateService s = new ResetDateService();
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Thread UpdateData;
       
        public CancellationTokenSource Source = new CancellationTokenSource();
       
        public Func<DateTime, CalendarIconDetails> SpecialDayPredicate { get; set; }
       
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

        private string _typeText;
        public string TypeText
        {
            get { return _typeText; }
            set
            {
                _typeText = value;
                OnPropertyChanged(nameof(TypeText));
            }
        }

        private string _wastedText;
        public string WastedText
        {
            get { return _wastedText; }
            set
            {
                _wastedText = value;
                OnPropertyChanged(nameof(WastedText));
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
            IsVisible = DataContainer.Instance.Addiction.Option != "Evento";

            if (IsVisible)
            {
                double? minutesWasted = DataContainer.Instance.Addiction.WastedTime.HasValue ?
                    DataContainer.Instance.Addiction.WastedTime.Value.TotalMinutes *
                    (DateTime.Today - DataContainer.Instance.Addiction.CreationDate).Days : null;

                double? moneyWasted = DataContainer.Instance.Addiction.WastedMoney > 0 ?
                    DataContainer.Instance.Addiction.WastedMoney *
                    (DateTime.Today - DataContainer.Instance.Addiction.CreationDate).Days : 0;

                TypeText = $"Total {DataContainer.Instance.Addiction.Option} gasto: ";
                WastedText = minutesWasted.HasValue ? string.Format("{0}H {1}min", minutesWasted / 60, minutesWasted % 60) 
                    : $"R${moneyWasted}" ;
            }



            ResetCommand = new Command( async () =>
            {
                DateTime resetTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                DataContainer.Instance.Addiction.LastResetDate = DateTime.Now;
                DataContainer.Instance.UpdateData();
                RefreshData();
                await s.InsertAsync(new ResetDate { AddictionId = DataContainer.Instance.Addiction.Id, Date = resetTime });
                
            });

            UpdateData = new Thread(() =>
            {
                UpdateData.IsBackground = true;
                CalculateData(Source.Token);
            });

            Task.Run(()=> SetDatesIcons()).Wait();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RefreshData()
        {
            Name = DataContainer.Instance.Addiction.Name;
            Date = DataContainer.Instance.Addiction.LastResetDate;
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

        private async Task SetDatesIcons()
        {
             List<ResetDate> listResetDates = await s.ToListAsync(DataContainer.Instance.Addiction.Id);

            SpecialDayPredicate = (date) => {
                
                CalendarIconDetails iconDetails = new CalendarIconDetails();
                var c = listResetDates.Where(x => x.Date == date).Count();

                if (date >= DataContainer.Instance.Addiction.CreationDate && date <= DateTime.Today && c == 0)
                {
                    iconDetails.Icon = CalendarIcon.Heart;
                    iconDetails.Fill = Colors.Red;
                    return iconDetails;
                }
                if (c > 0)
                {
                    iconDetails.Icon = CalendarIcon.Dot;
                    iconDetails.Fill = Colors.Blue;
                    return iconDetails;
                }
                return null;
            };
        }
    }
}

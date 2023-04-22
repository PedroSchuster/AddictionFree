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
    public class AchievementsPageVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Achievement> _achievements = new ObservableCollection<Achievement>();
        public ObservableCollection<Achievement> Achievements
        {
            get
            {
                return _achievements;
            }
            set
            {
                _achievements = value;
                OnPropertyChanged(nameof(Achievements));
            }
        }


        public AchievementsPageVM()
        {
            Achievements = new ObservableCollection<Achievement>(DataContainer.Instance.Achievements);
            SelectAchievement();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectAchievement()
        {
            ObservableCollection<Achievement> addictionAchievements = new ObservableCollection<Achievement>(DataContainer.Instance.AddictionAchievements);
            for (int i = 0; i < addictionAchievements.Count; i++)
            {
                switch (addictionAchievements[i].Text)
                {
                    case "3 Dias":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor);
                        Achievements[i] = valor;
                        break;
                    case "1 Semana":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor2 = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor2);
                        Achievements[i] = valor2;
                        break;
                    case "1 Mês":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor3 = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor3);
                        Achievements[i] = valor3;
                        break;
                    case "3 Meses":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor4 = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor4);
                        Achievements[i] = valor4;
                        break;
                    case "6 Meses":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor5 = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor5);
                        Achievements[i] = valor5;
                        break;
                    case "1 Ano":
                        Achievements[i].BackgroundColor = "Transparent";
                        Achievement valor6 = addictionAchievements[i];
                        ChangeAchievementStatus(ref valor6);
                        Achievements[i] = valor6;
                        break;
                }
                OnPropertyChanged(nameof(Achievements));

            }
        }

        private void ChangeAchievementStatus(ref Achievement achievement)
        {
            TimeSpan ts = DateTime.Now - DataContainer.Instance.Addiction.LastResetDate;
            double ratio = (ts.TotalHours / achievement.Duration.TotalHours) * 100;
            achievement.Percent = ratio >= 100 ? "100%" : ratio.ToString("F0") + "%";
        }

    }
}

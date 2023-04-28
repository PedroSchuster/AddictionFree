using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Entidades;
using AddictionApp.Services;
using SQLite;

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

        public List<Achievement> Achievements = new List<Achievement>();
        public ObservableCollection<Achievement> AddictionAchievements = new ObservableCollection<Achievement>();


        private DataContainer() { }

        public async Task Initialize(Addiction addiction)
        {
            AchievementService a = new AchievementService();

            Addiction = addiction;
            
            if (Achievements.Count <= 0)
            {
                //await a.DeleteAllAsync();
                Achievements.Add(new Achievement { Duration = new TimeSpan(3, 0, 0, 0), Text = "3 Dias", 
                    Image = "block.png", BackgroundColor = "Black", Percent = "0%", GradientStart = "#65F3DA", GradientEnd = "#3965FF" });

                Achievements.Add(new Achievement { Duration = new TimeSpan(7, 0, 0, 0), Text = "1 Semana", 
                    Image = "block.png", BackgroundColor = "#5ED3D3D3", Percent = "0%", GradientStart = "#3965FF", GradientEnd = "#FB38FF" });

                Achievements.Add(new Achievement { Duration = new TimeSpan(30, 0, 0, 0), Text = "1 Mês", 
                    Image = "block.png", BackgroundColor = "#5ED3D3D3", Percent = "0%", GradientStart = "#FA1AFF", GradientEnd = "#FF0000" });

                Achievements.Add(new Achievement { Duration = new TimeSpan(90, 0, 0, 0), Text = "3 Meses", 
                    Image = "block.png", BackgroundColor = "#5ED3D3D3", Percent = "0%", GradientStart = "#FF7A00", GradientEnd = "#FFE600" });

                Achievements.Add(new Achievement { Duration = new TimeSpan(180, 0, 0, 0), Text = "6 Meses",
                    Image = "block.png", BackgroundColor = "#5ED3D3D3", Percent = "0%", GradientStart = "#65F3DA", GradientEnd = "#3965FF" });

                Achievements.Add(new Achievement { Duration = new TimeSpan(365, 0, 0, 0), Text = "1 Ano", 
                    Image = "block.png", BackgroundColor = "#5ED3D3D3", Percent = "0%", GradientStart = "#FFE600", GradientEnd = "#42FF00" });

            }

            for (int i = 0; i < Achievements.Count; i++)
            {
                Achievements[i].AddictionId = addiction.Id;
            }

            AddictionAchievements = new ObservableCollection<Achievement>(await a.ToListAsync(Addiction.Id));
            if (AddictionAchievements.Count <= 0)
            {
                await a.InsertAsync(Achievements[0]);
            }

            await UpdateAchievement();
        }

        public async void UpdateData()
        {
            AddictionService a = new AddictionService();
            await a.UpdateAsync(Addiction);
        }

        private async Task UpdateAchievement()
        {
            AchievementService a = new AchievementService();
            TimeSpan ts = DateTime.Now - Addiction.LastResetDate;
            while (true)
            {
                AddictionAchievements = new ObservableCollection<Achievement>(await a.ToListAsync(Addiction.Id));
                Achievement lastAchievement = AddictionAchievements.Last();
                if (ts.TotalHours > lastAchievement.Duration.TotalHours)
                {
                    lastAchievement.IsCompleted = true;
                    await a.InsertAsync(Achievements[AddictionAchievements.Count]);
                    await a.UpdateAsync(lastAchievement);
                }
                else
                {
                    break;
                }
            }


        }
    }
}

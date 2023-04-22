using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Entidades;
using SQLite;

namespace AddictionApp.Services
{
    public class AchievementService
    {
        private SQLiteAsyncConnection database;

        public AchievementService()
        {
            try
            {
                database = App.DataBase;

            }
            catch
            {
                //popup?
            }
        }

        public async Task<int> InsertAsync(Achievement achievement)
        {
            try
            {
                return await database.InsertAsync(achievement);
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }

        public async Task<int> UpdateAsync(Achievement achievement)
        {
            try
            {
                return await database.UpdateAsync(achievement);
            }
            catch
            {
                //popup
                return -1;
            }
        }

        public async Task<List<Achievement>> ToListAsync(int addictionId)
        {
            ObservableCollection<Achievement> achievements;

            try
            {
                achievements = new ObservableCollection<Achievement>(await database.Table<Achievement>().ToListAsync());
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return new List<Achievement>();
            }

            return achievements.Where(x => x.AddictionId == addictionId).ToList();
        }

        public async Task<int> DeleteAllAsync()
        {
            try
            {
                return await database.DeleteAllAsync<Achievement>();
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }
    }
}

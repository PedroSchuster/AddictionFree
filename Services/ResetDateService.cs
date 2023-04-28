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
    class ResetDateService
    {
        private SQLiteAsyncConnection database;

        public ResetDateService()
        {
            try
            {
                database = App.DataBase;

            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

            }
        }

        public async Task<List<ResetDate>> ToListAsync(int id)
        {
            ObservableCollection<ResetDate> dates;

            try
            {
                dates = new ObservableCollection<ResetDate>(await database.Table<ResetDate>().ToListAsync());
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return new List<ResetDate>();
            }

            return dates.Where(x => x.AddictionId == id).ToList();
        }


        public async Task<int> DeleteAllAsync()
        {
            try
            {
                return await database.DeleteAllAsync<ResetDate>();
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }


        public async Task<int> InsertAsync(ResetDate resetDate)
        {
            try
            {
                return await database.InsertAsync(resetDate);
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }

    }
}

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
    public class AddictionService
    {
        private SQLiteAsyncConnection database;

        public AddictionService()
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

        public async Task<Addiction> FindById(int id)
        {
            Addiction addiction;
            try
            {
                addiction = await database.Table<Addiction>().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                //popup
                return null;
            }

            return addiction;
        }

        public async Task<ObservableCollection<Addiction>> ToListAsync()
        {
            ObservableCollection<Addiction> addiction;

            try
            {
                var b = database.Table<Addiction>();

                var a = await database.Table<Addiction>().ToListAsync();
                addiction = new ObservableCollection<Addiction>(await database.Table<Addiction>().ToListAsync());
            }
            catch(Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return null;
            }

            return addiction;
        }

        public async Task<int> DeleteAsync(Addiction addiction)
        {
            try
            {
                return await database.DeleteAsync(addiction);
            }
            catch(Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }

        public async Task<int> InsertAsync(Addiction addiction)
        {
            try
            {
                var b = database.Table<Addiction>();

                return await database.InsertAsync(addiction);
            }
            catch(Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }

        public async Task<int> UpdateAsync(Addiction addiction)
        {
            try
            {
                return await database.UpdateAsync(addiction);
            }
            catch
            {
                //popup
                return -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.Navigation;
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
                addiction = new ObservableCollection<Addiction>(await database.Table<Addiction>().ToListAsync());
            }
            catch
            {
                //popup
                return null;
            }

            return addiction;
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                return await database.Table<Addiction>().DeleteAsync(x => x.Id == id);
            }
            catch
            {
                //popup
                return -1;
            }
        }
    }
}

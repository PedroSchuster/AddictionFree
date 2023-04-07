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
    public class AddicitionNotesService
    {

        private SQLiteAsyncConnection database;

        public AddicitionNotesService()
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

        public async Task<AddictionNote> FindById(int id)
        {
            AddictionNote addictionNote;
            try
            {
                addictionNote = await database.Table<AddictionNote>().Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                //popup
                return null;
            }

            return addictionNote;
        }

        public async Task<AddictionNote> FindByDate(int id, DateTime date)
        {
            AddictionNote addictionNote;
            try
            {
                var a = ToListAsync();

                addictionNote = await database.Table<AddictionNote>().Where(x =>x.AddictionId == id && x.Date == date).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return null;
            }

            return addictionNote;
        }

        public async Task<ObservableCollection<AddictionNote>> ToListAsync()
        {
            ObservableCollection<AddictionNote> addictionNotes;

            try
            {

                addictionNotes = new ObservableCollection<AddictionNote>(await database.Table<AddictionNote>().ToListAsync());
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return null;
            }

            return addictionNotes;
        }


        public async Task<int> DeleteAllAsync()
        {
            try
            {
                return await database.DeleteAllAsync<AddictionNote>();
            }
            catch (Exception e)
            {
                //popup
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }


        public async Task<int> InsertAsync(AddictionNote addictionNote)
        {
            try
            {
                return await database.InsertAsync(addictionNote);
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", e.Message, "Fechar");

                return -1;
            }
        }

        public async Task<int> UpdateAsync(AddictionNote addictionNote)
        {
            try
            {
                return await database.UpdateAsync(addictionNote);
            }
            catch
            {
                //popup
                return -1;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AddictionApp.Entidades
{
    public class Addiction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public IList<Note> Diary { get; set; }
    }

    public struct Note
    {
        public string text;

        public DateTime date;
    }

}

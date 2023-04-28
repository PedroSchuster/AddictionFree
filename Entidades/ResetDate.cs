using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AddictionApp.Entidades
{
    public class ResetDate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AddictionId { get; set; }

        public DateTime Date { get; set; }
    }
}

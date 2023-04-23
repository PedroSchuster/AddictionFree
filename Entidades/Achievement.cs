using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AddictionApp.Entidades
{
    public class Achievement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AddictionId { get; set; }

        public string BackgroundColor { get; set; }

        public string GradientStart { get; set; }

        public string GradientEnd { get; set; }

        public string Text { get; set; }

        public string Percent { get; set; }

        public string Image { get; set; }

        public bool IsCompleted { get; set; }

        public TimeSpan Duration { get; set; }
    }
}

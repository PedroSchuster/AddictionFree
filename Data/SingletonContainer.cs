using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddictionApp.Views;

namespace AddictionApp
{
    public static class SingletonContainer
    {
        private static ProgressPage _progressPage;
        public static ProgressPage ProgressPage { get { return _progressPage; } }

        private static NotesPage _notesPage;
        public static NotesPage NotesPage { get { return _notesPage; } }

        public static void Init()
        {
            _progressPage = new ProgressPage();
        }
    }
}

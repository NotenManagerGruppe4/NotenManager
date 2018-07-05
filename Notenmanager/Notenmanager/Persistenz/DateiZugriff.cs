using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Persistenz
{
    public static class DateiZugriff
    {
        public static string[] LeseDatei(string pfad)
        {
            return File.ReadAllLines(pfad);
        }
<<<<<<< HEAD

        public static void ImportSchueler(string pfad)
        {

        }
=======
>>>>>>> 32685b1b05967f5122700ddb237feb79e85f0962
    }
}

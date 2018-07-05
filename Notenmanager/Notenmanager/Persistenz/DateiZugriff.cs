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
        public static void ImportSchueler(string pfad)
        {

        }
    }
}

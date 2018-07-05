using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_EM;

namespace Notenmanager.Persistenz
{
    public static class DateiZugriff
    {
        /// <summary>
        /// Liest eine Datei falls diese vorhanden ist und gibt deren Inhalt als Zeilen zurück.
        /// </summary>
        /// <param name="pfad">Pfad der Datei, die gelesen werden soll.</param>
        /// <returns>string[] mit den gelesenen Zeilen falls die Datei existiert. Jeder Index entspricht einer Zeile.
        ///         Existiert keine Datei wird eine Exception geworfen.</returns>
        public static string[] LeseDatei(string pfad)
        {
            if (File.Exists(pfad))
                return File.ReadAllLines(pfad);
            else
                throw new FileNotFoundException("Die angegebene Datei konnet nicht gefunden werden!");
        }

        public static void ImportSchueler(string pfad)
        {
            string[] zeilen = LeseDatei(pfad);

            foreach(string s in zeilen)
            {
                var schueler = new Schueler();

            }
        }
    }
}

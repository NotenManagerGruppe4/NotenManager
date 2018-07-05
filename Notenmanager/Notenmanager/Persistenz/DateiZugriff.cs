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
        private static string[] LeseDatei(string pfad)
        {
            if (File.Exists(pfad))
                return File.ReadAllLines(pfad);
            else
                throw new FileNotFoundException("Die angegebene Datei konnet nicht gefunden werden!");
        }

        /// <summary>
        /// Importiert eine CSV-Datei mit Schülern und speichert diese in die Datenbank
        /// </summary>
        /// <param name="pfad">Pfad der Schülerdatei</param>
        public static void ImportSchueler(string pfad)
        {
            string[] zeilen = LeseDatei(pfad);
            string[] schuel;

            foreach(string s in zeilen)
            {
                var schueler = new Schueler();
                schuel = s.Split(',');
                schueler.Nachname = schuel[1];
                schueler.Vorname = schuel[2];
                schueler.Geburtsdatum = Convert.ToDateTime(schuel[3]);
                //Konfession?
                //Geschlecht?

            //schueler.speichern();
            }
        }
        
        /// <summary>
        /// Importiert die Klassen aus einer CSV-Datei --> erstellt daraus die Klassenobjekte--> speichert diese in der Datenbank
        /// </summary>
        /// <param name="pfad">Pfad der Klassendatei</param>
        public static void ImportKlassen(string pfad)
        {
            string[] klassen = LeseDatei(pfad);
            string[] kl;


            foreach(string klasse in klassen)
            {
                string[] sjahr;
                Klasse k = new Klasse();
                kl = klasse.Split(',');
                sjahr = kl[2].Split('/');
                k.Bez = kl[1];
                k.SJ = Convert.ToInt32(sjahr[0]);

                //k.speichern();
            }
        }


        /// <summary>
        /// Importiert die Lehrer aus einer CSV-Datei --> erstellt daraus die Lehrerobjekte--> speichert diese in der Datenbank
        /// </summary>
        /// <param name="pfad">Pfad der Lehrerdatei</param>
        public static void ImportLehrer(string pfad)
        {

            string[] lehrer = LeseDatei(pfad);
            string[] le;

            foreach(string lehr in lehrer)
            {
                Lehrer l = new Lehrer();
                le = lehr.Split(',');
                l.Kürzel = le[3];
                l.Nachname = le[1];
                l.Vorname = le[2];

                //l.speichern();
            }
        }
    }
}

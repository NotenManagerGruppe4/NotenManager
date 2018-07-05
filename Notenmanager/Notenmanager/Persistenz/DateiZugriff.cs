﻿using System;
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

        public static void ImportKlassen(string pfad)
        {
            string[] klassen = LeseDatei(pfad);
            string[] kl;


            foreach(string klasse in klassen)
            {
                Klasse k = new Klasse();
                kl = klasse.Split(';');
                //k.Bez = kl[0];
                //k.SJ = Convert.ToInt32(kl[1]);
                //k.IdSchule = Convert.ToInt32(kl[2]);
                //k.IdKlassenleiter = Convert.ToInt32(kl[3]);
                //k.IdStvKlassenleiter = Convert.ToInt32(kl[4]);

                //k.speichern();

            }

        }
    }
}

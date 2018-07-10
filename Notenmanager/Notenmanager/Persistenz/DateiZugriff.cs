using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notenmanager.Model;

namespace Notenmanager.Persistenz
{
   public static class DateiZugriff
   {
      ///// <summary>
      ///// Liest eine Datei falls diese vorhanden ist und gibt deren Inhalt als Zeilen zurück.
      ///// </summary>
      ///// <param name="pfad">Pfad der Datei, die gelesen werden soll.</param>
      ///// <returns>string[] mit den gelesenen Zeilen falls die Datei existiert. Jeder Index entspricht einer Zeile.
      /////         Existiert keine Datei wird eine Exception geworfen.</returns>
      //private static string[] LeseDatei(string pfad)
      //{
      //   if (File.Exists(pfad))
      //      return File.ReadAllLines(pfad);
      //   else
      //      throw new FileNotFoundException("Die angegebene Datei konnte nicht gefunden werden!");
      //}

      /// <summary>
      /// Importiert eine CSV-Datei mit Schülern und speichert diese in die Datenbank
      /// </summary>
      /// <param name="pfad">Pfad der Schülerdatei</param>
      public static void ImportSchueler(string pfad)
      {
         string[] tmp;

         foreach (string s in File.ReadAllLines(pfad))
         {
            tmp = s.Split(',');

            string readedkonfession = tmp[8].Trim().ToLower();
            Konfession k;

            if (readedkonfession == "bl")
               k = Konfession.BL;
            if (readedkonfession == "ev")
               k = Konfession.EV;
            else if (readedkonfession == "rk")
               k = Konfession.RK;
            else
               k = Konfession.SONST;

            Schueler schueler = new Schueler()
            {
               Nachname = tmp[1],
               Vorname = tmp[2],
               Geburtsdatum = Convert.ToDateTime(tmp[3]),
               Geschlecht = tmp[7] == "m" ? Geschlecht.M : Geschlecht.W,
               Konfession = k,
            };

            DBZugriff.Current.Speichern(schueler, false);
         }

         DBZugriff.Current.Save();
      }

      /// <summary>
      /// Importiert die Klassen aus einer CSV-Datei --> erstellt daraus die Klassenobjekte--> speichert diese in die Datenbank
      /// </summary>
      /// <param name="pfad">Pfad der Klassendatei</param>
      /// <param name="schule">Zugehörige Schule</param>
      public static void ImportKlassen(string pfad, Schule schule)
      {
         string[] tmp;

         foreach (string s in File.ReadAllLines(pfad))
         {
            tmp = s.Split(',');

            Klasse klasse = new Klasse()
            {
               Bez = tmp[1],
               SJ = Convert.ToInt32(tmp[2].Split('/')[0]),
               Schule = schule,
            };

            DBZugriff.Current.Speichern(klasse, false);
         }
         DBZugriff.Current.Save();
      }


      /// <summary>
      /// Importiert die Lehrer aus einer CSV-Datei --> erstellt daraus die Lehrerobjekte--> speichert diese in die Datenbank
      /// </summary>
      /// <param name="pfad">Pfad der Lehrerdatei</param>
      public static void ImportLehrer(string pfad)
      {
         string[] tmp;

         foreach (string s in File.ReadAllLines(pfad))
         {

            tmp = s.Split(',');

            Lehrer lehrer = new Lehrer()
            {
               Kürzel = tmp[3],
               Nachname = tmp[1],
               Vorname = tmp[2],
            };

            DBZugriff.Current.Speichern(lehrer, false);
         }

         DBZugriff.Current.Save();
      }


   }
}

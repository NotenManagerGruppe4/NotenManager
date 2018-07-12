using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notenmanager.Model;
using System.Diagnostics;

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
      public static void ImportSchueler(string pfad, bool autoGenerateSchuelerKlasse = true)
      {
         string[] tmp;

         foreach (string s in ReadAllLines(pfad))
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

            int sid = Convert.ToInt32(tmp[0]);

            Schueler schueler = DBZugriff.Current.SelectFirstOrDefault<Schueler>(x => x.SID == sid);
            bool neu = (schueler == null);
            if (neu)
               schueler = new Schueler();

            schueler.SID = sid;
            schueler.Nachname = tmp[1];
            schueler.Vorname = tmp[2];
            schueler.Geburtsdatum = Convert.ToDateTime(tmp[3]);
            schueler.Geschlecht = tmp[7] == "m" ? Geschlecht.M : Geschlecht.W;
            schueler.Konfession = k;

            DBZugriff.Current.Speichern(schueler, false);

            if (neu && autoGenerateSchuelerKlasse)
               GeneriereSchuelerKlasse(schueler, tmp);
         }

         DBZugriff.Current.Save();
      }

      //Wünschenswert: Rückgabewerte für AnzSchuelerKlasse OK & Err
      private static void GeneriereSchuelerKlasse(Schueler schueler, string[] tmp)
      {

         string kbez = tmp[4];
         int ksj = 0;
         if (!Int32.TryParse(tmp[5].Split('/')[0], out ksj))
            return; //Parse Err

         Klasse k = DBZugriff.Current.SelectFirstOrDefault<Klasse>(x => x.Bez == kbez && x.SJ == ksj);
         //404
         if (k == null)
            return;


         SchuelerKlasse sk = new SchuelerKlasse()
         {
            Schueler = schueler,
            Klasse = k,
         };
         DBZugriff.Current.Speichern(sk, false);
      }

      /// <summary>
      /// Importiert die Klassen aus einer CSV-Datei --> erstellt daraus die Klassenobjekte--> speichert diese in die Datenbank
      /// </summary>
      /// <param name="pfad">Pfad der Klassendatei</param>
      /// <param name="schule">Zugehörige Schule</param>
      public static void ImportKlassen(string pfad, Schule schule)
      {
         string[] tmp;

         Lehrer dummy = new Lehrer()
         {
            Vorname = "-",
            Nachname = "-",
            Kürzel = "-",
            Dienstbezeichnung = "DUMMY",
         };
         dummy = DBZugriff.Current.SelectFirstOrDefault<Lehrer>(x => 
               x.Vorname == dummy.Vorname
               && x.Nachname == dummy.Nachname
               && x.Kürzel == dummy.Kürzel
               && x.Dienstbezeichnung == dummy.Dienstbezeichnung
               ) ?? dummy;
         dummy.Speichern();


         foreach (string s in ReadAllLines(pfad))
         {
            tmp = s.Split(',');

            int sid = Convert.ToInt32(tmp[0]);
            Klasse klasse = DBZugriff.Current.SelectFirstOrDefault<Klasse>(x => x.SID == sid);
            if (klasse == null)
               klasse = new Klasse();


            klasse.SID = sid;
            klasse.Bez = tmp[1];
            klasse.SJ = Convert.ToInt32(tmp[2].Split('/')[0]);
            klasse.Schule = schule;
            klasse.Klassenleiter = dummy;
            klasse.StellvertretenderKlassenleiter = dummy;


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

         foreach (string s in ReadAllLines(pfad))
         {
            tmp = s.Split(',');

            int sid = Convert.ToInt32(tmp[0]);
            Lehrer lehrer = DBZugriff.Current.SelectFirstOrDefault<Lehrer>(x => x.SID == sid);
            if (lehrer == null)
               lehrer = new Lehrer();

            lehrer.SID = Convert.ToInt32(tmp[0]);
            lehrer.Nachname = tmp[1];
            lehrer.Vorname = tmp[2];
            lehrer.Kürzel = tmp[3];


            DBZugriff.Current.Speichern(lehrer, false);
         }

         DBZugriff.Current.Save();
      }

      /// <summary>
      /// Liest alle Zeilen aus einer Datei
      /// </summary>
      /// <param name="filepath">Dateipfad</param>
      private static string[] ReadAllLines(string filepath)
      {
         return File.ReadAllLines(filepath, GetEncoding(filepath));
      }

      /// <summary>
      /// Holt die Encodierung (z.B. UTF8, ANSI, ...) der angegebenen Datei
      /// </summary>
      private static Encoding GetEncoding(string filepath)
      {
         // Read the BOM
         var bom = new byte[4];
         using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
         {
            file.Read(bom, 0, 4);
         }

         // Analyze the BOM
         if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
            return Encoding.UTF7;
         else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            return Encoding.UTF8;
         else if (bom[0] == 0xff && bom[1] == 0xfe)
            return Encoding.Unicode; //UTF-16LE
         else if (bom[0] == 0xfe && bom[1] == 0xff)
            return Encoding.BigEndianUnicode; //UTF-16BE
         else if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            return Encoding.UTF32;

         //Kein BOM oder etwas anderes
         return Encoding.Default;
      }
   }
}

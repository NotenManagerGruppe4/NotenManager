using MySql.Data.Entity;
using NOMACreator.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMACreator
{
   class Program
   {
      static void Main(string[] args)
      {
         AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
         {
            DBZugriff.CloseDB();
         };


         DBZugriff.InitDB();


         RunTest();

         DBZugriff.CloseDB();

         Console.ReadKey();


      }

      private static void RunTest()
      {
         try
         {
            Schule schule = new Schule()
            {
               Bez = "TestSchule",
            };
            schule.Speichern();

            Lehrer leh = new Lehrer()
            {
               Vorname = "VorTest",
               Nachname = "NachTest",
               Kürzel = "TL",
            };
            leh.Speichern();

            Klasse kl = new Klasse()
            {
               Bez = "TKL1A",
               SJ = 2017,
               Schule = schule,
               Klassenleiter = leh,
               StellvertretenderKlassenleiter = leh,
            };
            kl.Speichern();

            Zeugnisfach zf = new Zeugnisfach()
            {
               Bez = "TestAbschlussFach",
               Fachart = "TestFachArt",
               Pos = 1,
               Vorrueckungsfach = true,
               AbschliessendesFach = true,
               Klasse = kl,
            };
            zf.Speichern();

            Unterrichtsfach uf = new Unterrichtsfach()
            {
               Bez = "TestUnterichtsFach",
               Pos = 1,
               Stunden = 6,
               Zeugnisfach = zf,
            };
            uf.Speichern();

            UFachLehrer ufl = new UFachLehrer()
            {
               Lehrer = leh,
               Unterrichtsfach = uf,
               Stunden = uf.Stunden
            };
            ufl.Speichern();


            Schueler s = new Schueler()
            {
               Vorname = "VorTestS",
               Nachname = "NachTestS",
               Geburtsdatum = new DateTime(2000, 1, 1),
               Geschlecht = "M",
               Konfession = "K",
            };
            s.Speichern();

            SchuelerKlasse sk = new SchuelerKlasse()
            {
               Klasse = kl,
               Schueler = s,
            };
            sk.Speichern();

            Leistungsart la = new Leistungsart()
            {
               Bez = "Schulaufgabe",
               Gewichtung = 2,
               Gruppe = "S",

            };
            la.Speichern();

            Leistungsgruppe lg = new Leistungsgruppe()
            {
               Bez = "Schriftlich",
               Gewicht = "2",
               Leistungsart = la
            };
            lg.Speichern();

            Leistung lei = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 1,
               Tendenz = "+",
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la,
               SchuelerKlasse = sk,
            };
            lei.Speichern();
         }
         catch(Exception e)
         {
            Console.WriteLine(e);
         }


      }
   }
}

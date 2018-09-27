﻿using Notenmanager.Model;
using Notenmanager.View.UIComp;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Notenmanager
{
   /// <summary>
   /// Interaktionslogik für "App.xaml"
   /// </summary>
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         AppDomain.CurrentDomain.UnhandledException += (sender, e2) =>
         {
            try
            {
               DBZugriff.CloseDB();

               Exception ex = e2.ExceptionObject as Exception;
               if (ex != null)
               {
                  this.Dispatcher.Invoke(() =>
                  {
                     CrashMessage.ShowErrorMessage(ex);
                  });
               }
               else
                  throw new Exception("Ungültige Exception von Sender: " + sender);
            }
            catch (Exception exx)
            {
               MessageBox.Show(exx.ToString(), "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
               Environment.Exit(-1);
            }
         };

         //Start EF
         DBZugriff.InitDB();

#if DEBUG
         //Testaufruf
         //Aufruf durch Projekt>Eigenschaften>Debugging>Parameter: -test (Danach aber wieder entfernen;)
         if (Environment.GetCommandLineArgs().Contains("-test"))
            Test();
#endif
      }

      private void Application_Exit(object sender, ExitEventArgs e)
      {
         DBZugriff.CloseDB();
      }

      private static void Test()
      {
         try
         {
            Trace.WriteLine("Test: Warte auf DB Verbindung...");
            
            int timeout = 20; //20 Zyklen Timeout
            //Warte bis DB-ready
            for (; timeout >= 0 && DBZugriff.InitRuns; timeout--)
               Thread.Sleep(500);

            if (timeout < 0)
               throw new TimeoutException("Test: Datenbank-TIMEOUT!");

            Trace.WriteLine("Test: Verbindung hergestellt! Starte Test!");


            //Test hier ausführen

            Tests t = new Tests();
            //t.InsertTest(); //<-- Test für Beispieldaten
         }
         catch (Exception e)
         {
            Trace.WriteLine(e.ToString());
         }
         Trace.WriteLine("Test: Tests beendet!");

         Thread th = new Thread(() =>
         {
            Thread.Sleep(2000); //Warte bis Fenster hoffentlich fertig geladen

            Navigator.Instance.StartUpDone();
            Trace.WriteLine("Test: Versuche GUI zu aktivieren...");
         });
         th.IsBackground = true;
         th.Name = "Test:AktiviereGUI-Thread";
         th.Start();
      }

      

      //[TestClass()]
      internal class Tests
      {

         internal void InsertTest()
         {
            Schule schule = new Schule()
            {
               Bez = "EDV-Schule",//"TestSchule"+DateTime.Now.Millisecond,
            };
            schule.Speichern();

            Lehrer leh = new Lehrer()
            {
               Vorname = "Klaus",
               Nachname = "Reuther",
               Kuerzel = "BAR",
            };
            leh.Speichern();

            Klasse kl = new Klasse()
            {
               Bez = "BFI12A",//"TKL1A"+ DateTime.Now.Millisecond,
               SJ = ViewModel.Tool.CURRENTSJ,
               Schule = schule,
               Klassenleiter = leh,
               StellvertretenderKlassenleiter = leh,
            };
            kl.Speichern();

            Zeugnisfach zf = new Zeugnisfach()
            {
               Bez = "TFBAN",
               Fachart = Fachart.Pflichtfach,
               Pos = 1,
               Vorrueckungsfach = true,
               AbschliessendesFach = true,
               Klasse = kl,
            };
            zf.Speichern();
            Zeugnisfach zf2 = new Zeugnisfach()
            {
               Bez = "Anwendungsentwicklung",
               Fachart = Fachart.Wahlpflichtfach,
               Pos = 2,
               Vorrueckungsfach = true,
               AbschliessendesFach = true,
               Klasse = kl,
            };
            zf2.Speichern();

            Unterrichtsfach uf = new Unterrichtsfach()
            {
               Bez = "TFBAN1",
               Pos = 1,
               Stunden = 6,
               Zeugnisfach = zf,
            };
            uf.Speichern();
            Unterrichtsfach uf2 = new Unterrichtsfach()
            {
               Bez = "TFBAN2",
               Pos = 1,
               Stunden = 4,
               Zeugnisfach = zf,
            };
            uf2.Speichern();
            Unterrichtsfach uf2B = new Unterrichtsfach()
            {
               Bez = "C#",
               Pos = 1,
               Stunden = 4,
               Zeugnisfach = zf2,
            };
            uf2B.Speichern();

            UFachLehrer ufl = new UFachLehrer()
            {
               Lehrer = leh,
               Unterrichtsfach = uf,
               Stunden = uf.Stunden
            };
            ufl.Speichern();
            UFachLehrer ufl2 = new UFachLehrer()
            {
               Lehrer = leh,
               Unterrichtsfach = uf2,
               Stunden = uf.Stunden
            };
            ufl2.Speichern();
            UFachLehrer ufl2B = new UFachLehrer()
            {
               Lehrer = leh,
               Unterrichtsfach = uf2B,
               Stunden = uf.Stunden
            };
            ufl2B.Speichern();


            Schueler s = new Schueler()
            {
               Vorname = "Hans",//"VornameSchüler"+DateTime.Now.Millisecond,
               Nachname = "Bauer",
               Geburtsdatum = new DateTime(2000, 1, 1),
               Geschlecht = Geschlecht.M,
               Konfession = Konfession.BL,
            };
            s.Speichern();

            SchuelerKlasse sk = new SchuelerKlasse()
            {
               Klasse = kl,
               Schueler = s,
            };
            sk.Speichern();

            Schueler s2 = new Schueler()
            {
               Vorname = "Harry",//"VornameSchüler" + (DateTime.Now.Millisecond+10),
               Nachname = "Müller",
               Geburtsdatum = new DateTime(2001, 1, 1),
               Geschlecht = Geschlecht.M,
               Konfession = Konfession.BL,
            };
            s2.Speichern();
            SchuelerKlasse sk2 = new SchuelerKlasse()
            {
               Klasse = kl,
               Schueler = s2,
            };
            sk2.Speichern();


            Leistungsgruppe lg = new Leistungsgruppe()
            {
               Bez = "Schriftlich",
               Gewicht = 2,
            };
            lg.Speichern();
            Leistungsgruppe lg2 = new Leistungsgruppe()
            {
               Bez = "Mündlich",
               Gewicht = 1,
            };
            lg2.Speichern();

            Leistungsart la = new Leistungsart()
            {
               Bez = "Schulaufgabe",
               Gewichtung = 2,
               Gruppe = lg,

            };
            la.Speichern();
            Leistungsart la2 = new Leistungsart()
            {
               Bez = "Stegreifaufgabe",
               Gewichtung = 1,
               Gruppe = lg,

            };
            la2.Speichern();
            Leistungsart la3 = new Leistungsart()
            {
               Bez = "Abfrage",
               Gewichtung = 1,
               Gruppe = lg2,

            };
            la3.Speichern();

            Leistung lei = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 1,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la,
               SchuelerKlasse = sk,
               UFachLehrer = ufl
            };
            lei.Speichern();
            Leistung lei2 = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 3,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la2,
               SchuelerKlasse = sk,
               UFachLehrer = ufl
            };
            lei2.Speichern();
            Leistung lei3 = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 2,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la2,
               SchuelerKlasse = sk,
               UFachLehrer = ufl
            };
            lei3.Speichern();
            Leistung lei4 = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 3,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la3,
               SchuelerKlasse = sk,
               UFachLehrer = ufl
            };
            lei4.Speichern();

            Leistung leiB = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 3,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la,
               SchuelerKlasse = sk,
               UFachLehrer = ufl2
            };
            leiB.Speichern();
            Leistung lei2B = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 1,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la,
               SchuelerKlasse = sk,
               UFachLehrer = ufl2B
            };
            lei2B.Speichern();
            Leistung lei2C = new Leistung()
            {
               Erhebungsdatum = DateTime.Now,
               Notenstufe = 3,
               Tendenz = Tendenz.UP,
               LetzteÄnderung = DateTime.Now,
               Leistungsart = la,
               SchuelerKlasse = sk,
               UFachLehrer = ufl2B
            };
            lei2C.Speichern();


         }

         internal void ValidierungsTest()
         {
            //Lehrer leh = new Lehrer()
            //{
            //   Vorname = "KAPUTT"
            //};
            //leh.Speichern();

            //Lehrer leh2 = new Lehrer()
            //{
            //   Vorname = "TEST",
            //   Nachname = "Test",
            //   Kuerzel="TTTT",
            //};
            //leh2.Speichern();


            //foreach (Lehrer l in DBZugriff.Current.Context.LehrerSet)
            //   Trace.WriteLine(l.Vorname);
         }
      }
   }
}

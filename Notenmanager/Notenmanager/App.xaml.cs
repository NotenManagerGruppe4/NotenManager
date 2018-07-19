using Notenmanager.Model;
using Notenmanager.View.UIComp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
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

         DBZugriff.InitDB();

         AppDomain.CurrentDomain.UnhandledException += (sender, e2) =>
         {
            try
            {
               DBZugriff.CloseDB();

               Exception ex = e2.ExceptionObject as Exception;
               if (ex != null)
                  CrashMessage.ShowErrorMessage(ex);
            }
            catch (Exception exx)
            {
               MessageBox.Show(exx.ToString(), "Fataler Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         };


#if DEBUG
         Tests s = new Tests();
         if (Environment.GetCommandLineArgs().Contains("-test"))
            s.Test2();
         if (Environment.GetCommandLineArgs().Contains("-valtest"))
            s.Test();

         foreach (Lehrer l in DBZugriff.Current.Context.LehrerSet)
            Trace.WriteLine(l.Vorname);
#endif
      }

      private void Application_Exit(object sender, ExitEventArgs e)
      {
         DBZugriff.CloseDB();
      }

      //[TestClass()]
      public class Tests
      {

         public void Test()
         {
            Lehrer leh = new Lehrer()
            {
               Vorname = "KAPUTT"
            };
            leh.Speichern();

            Lehrer leh2 = new Lehrer()
            {
               Vorname = "TEST",
               Nachname = "Test",
               Kuerzel="TTTT",
            };
            leh2.Speichern();

         }
         //[TestMethod()]
         public void Test2()
         {
            try
            {
               //DBZugriff.InitDB();

               InsertTest();
            }
            catch (Exception e)
            {
               Trace.WriteLine(e.ToString());
               //Assert.Fail();
            }
            //DBZugriff.CloseDB();
         }


         private static void InsertTest()
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
               Kuerzel = "TL",
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
               Fachart = Fachart.Pflichtfach,
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


            Leistungsgruppe lg = new Leistungsgruppe()
            {
               Bez = "Schriftlich",
               Gewicht = 2,
            };
            lg.Speichern();

            Leistungsart la = new Leistungsart()
            {
               Bez = "Schulaufgabe",
               Gewichtung = 2,
               Gruppe = lg,

            };
            la.Speichern();

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

         }
      }
   }
}

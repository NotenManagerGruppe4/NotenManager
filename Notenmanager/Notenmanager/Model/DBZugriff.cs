﻿using MySql.Data.Entity;
using Notenmanager.ViewModel.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Notenmanager.Model
{
   //Hauptklasse für den Datenbankzugriff
   //
   // !- Falls etwas hier nicht beschrieben sein sollte, einfach im Projekt danach suchen ;)
   // Vorgehensweise bei Erstellen/Ändern des DB-Modells:
   // 0. falls das Projekt das erste Mal EF nutzt: 
   //     * NuGet-Manager folgende Pakete installieren:
   //       - EntityFramewrok
   //       - MySql.Data.Entity (enthält/benötigt die GLEICHE!!! Version vom normalen MySql-Konnektor)
   //     * App.config konfigurieren
   //       - <connectionStrings> hinzufügen
   //     * Context.cs (DB-Darstellung)
   //       - [DbConfigurationType(typeof(MySqlEFConfiguration))] über die Klasse schreiben
   //       - : base("name=<NameEinesConnectionStrings>") am Standardkonstruktor anhängen
   //     * Main-Methode
   //       -  DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
   //     * Paket-Manager-Konsole> Enable-Migrations (falls bereits vorhanden: Enable-Migrations -Force)
   // 1. falls vorhanden, Datenbank zurücksetzen (alle bestehenden Tabellen löschen)
   // 2. falls vorhanden, alle Migrationsberichte im Ordner "Migrations" löschen
   // 3. Neue Migration via 
   //       Paket-Manager-Konsole> Add-Migration <IrgendeinPassenderName>
   //    erstellen und ggf auf Fehler prüfen
   // 4. Datenbank Updaten via
   //       Paket-Manager-Konsole> Update-Database -Verbose

   public class DBZugriff : IDisposable
   {
      public const int STRING_MAXLENGTH = 45;

      //Stellt die jetzige Session dar
      public static DBZugriff Current { get; private set; }

      //Stellt die DB dar
      protected Context Context { get; private set; }

      //wenn initizialisiert wird
      private static bool InitRuns { get; set; } = true;
      private Thread Initer = new Thread(() =>
      {
         try
         {
            Trace.WriteLine("[DB] Loading Context...");
            DateTime start = DateTime.Now;

            //Manuell
            DBZugriff.Current.Context.GetDbSet<Klasse>().SingleOrDefault();

            Navigator.Instance.StartUpDone();

            Trace.WriteLine($"[DB] Loaded Context ({(DateTime.Now - start).TotalSeconds.ToString("#0.00")}s)");
         }
         catch (Exception e)
         {
            Trace.WriteLine("ERROR: LOADING CONTEXT: " + e.ToString());

            string exmsgre = "";
            Exception ce = e;
            while (ce.InnerException != null)
            {
               exmsgre += "\t->" + ce.InnerException.Message + "\r\n";
               ce = ce.InnerException;
            }

            MessageBox.Show("Fehler beim Starten von EF:\r\n" + exmsgre, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            throw e;
         }
         finally
         {
            InitRuns = false;
         }
      });
      private object _key = new object();

      public DBZugriff()
      {
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

         Init();
         Trace.WriteLine("[DB] Inited Context");

         PreLoadContext();
      }
      private void Init()
      {
         Context = new Context();
      }

      //Lädt Context im Vorrsaus
      private void PreLoadContext()
      {
         Initer.Start();
         Initer = null;
      }

      private void CheckInit()
      {
         lock (_key)
         {

            if (InitRuns)
            {
               Initer?.Join();

               Trace.WriteLine("[DB] WARN: Zugriff auf Context obwohl er NICHT GELADEN ist! Stack:\r\n" + Environment.StackTrace.ToString());
            }
            //falls checkinit aufgerufen bevor, der Thread gestartet wird...
            while (InitRuns)
            {
               Thread.Sleep(500);
               Initer?.Join();
            }
         }
      }

      /// <summary>
      /// Speichert (Hinzufügen oder Ändern) ein Objekt in die Datenbank (auch refernezierte Objekte)
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das Objekt</param>
      /// <param name="autoSyncDb">Stellt dar, ob dieDatenbank nach dem Speichern sofort aktualisiert werden soll</param>
      /// <exception cref="Exception"></exception>
      public void Speichern<T>(T obj, bool autoSyncDb = true) where T : class, IDBable
      {
         try
         {
            DbSet<T> dbset = GetDbSetFromContext<T>();

            T inside = dbset.SingleOrDefault(o => o.Id == obj.Id);
            //Update
            if (inside != null)
               inside = obj;
            //Add
            else
               dbset.Add(obj);

            if (autoSyncDb)
               Save();
         }
         catch (Exception e)
         {
            Trace.WriteLine($"[DBZ] Fehler beim Speichern von '{obj}'\r\n" + e.ToString());
            throw e;
         }

      }

      /// <summary>
      /// Löscht ein Objekt (logisch) aus der Datenbank 
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das Objekt</param>
      /// <param name="autoSyncDb">Stellt dar, ob dieDatenbank nach dem Speichern sofort aktualisiert werden soll</param>
      /// <exception cref="Exception"></exception>
      public void Loeschen<T>(T obj, bool autoSyncDb = true) where T : class, IDBable
      {
         CheckInit();

         try
         {
            //DbSet<T> dbset = GetDbSetFromContext<T>();

            ////Delete
            //dbset.Remove(obj);

            //Nur logisch löschen
            obj.Active = false;

            if (autoSyncDb)
               Save();

         }
         catch (Exception e)
         {
            Trace.WriteLine($"[DBZ] Fehler bei Löschen von '{obj}'\r\n" + e.ToString());
            throw e;
         }
      }

      /// <summary>
      /// Lädt das Objekt neu aus der Datenbank
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="obj">Das Objekt</param>
      public void Reload<T>(T obj) where T : class, IDBable
      {
         CheckInit();

         Context.Entry<T>(obj)?.Reload();
      }

      /// <summary>
      /// Selektiert ein Element aus der Datenbank; wirft eine Exception falls Mehrere gefunden
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="pred">Die Vorlage zum Selektieren z.B. (x => x.Id == 1)</param>
      /// <returns>Objekt vom Typ T, default(T) (meistens null) falls nichts gefunden</returns>
      /// <exception cref="InvalidOperationException">Mehrer Elemente gefunden</exception>
      public T SelectSingleOrDefault<T>(Func<T, bool> pred) where T : class, IDBable
      {
         return GetDbSetFromContext<T>().SingleOrDefault(pred);
      }

      /// <summary>
      /// Selektiert ein Element aus der Datenbank; erste Element falls Mehrere gefunden
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="pred">Die Vorlage zum Selektieren z.B. (x => x.Id == 1)</param>
      /// <returns>Objekt vom Typ T, default(T) (meistens null) falls nichts gefunden</returns>
      public T SelectFirstOrDefault<T>(Func<T, bool> pred = null) where T : class, IDBable
      {
         DbSet<T> tmp = GetDbSetFromContext<T>();

         if (pred == null)
            return tmp.FirstOrDefault();
         else
            return tmp.FirstOrDefault(pred);
      }


      /// <summary>
      /// Selektiert ALLES aus der Tabelle von T
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// /// <param name="showDeActive">Zeigt die deaktivierten Einträge an</param>
      /// <returns>Liste mit Elementen</returns>
      public List<T> Select<T>(bool showDeActive = false) where T : class, IDBable
      {
         if (showDeActive)
            return GetDbSetFromContext<T>().ToList();
         else
            return GetDbSetFromContext<T>().Where(x => x.Active == true).ToList();
      }

      /// <summary>
      /// Selektiert nach Kriterium aus der Tabelle von T
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="pred">Suchkriterien z.B. x => x.Name == "Test"</param>
      /// <param name="showDeActive">Zeigt die deaktivierten Einträge an</param>
      /// <returns>Liste mit Elementen</returns>
      public List<T> Select<T>(Func<T, bool> pred, bool showDeActive = false) where T : class, IDBable
      {

         if (showDeActive)
            return GetDbSetFromContext<T>().Where(pred).ToList();
         else
            return Select<T>(false).Where(pred).ToList();
      }


      public DbSet<T> GetDbSetFromContext<T>() where T : class, IDBable
      {
         CheckInit();

         return Context.GetDbSet<T>();
      }

      /// <summary>
      /// Beginnt eine Transaktion; Benutzung am Besten in Kombination mit using...
      /// Abbau mit: Save + Commit || Rollback
      /// </summary>
      /// <returns>Transaktions-Objekt</returns>
      public DbContextTransaction BeginTransaction()
      {
         CheckInit();

         return Context.Database.BeginTransaction();
      }


      //TEMP: async = false : Bessere Fehlernachvollziehbarkeit
      public void Save(bool async = false)
      {
         CheckInit();
         try
         {
            if (async)
               Context.SaveChangesAsync();
            else
               Context.SaveChanges();
         }
         catch (DbEntityValidationException ex)
         {
            foreach (var eve in ex.EntityValidationErrors.ToList())
            {
               Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
               foreach (var ve in eve.ValidationErrors)
                  Trace.WriteLine($"- Property: \"{ ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");

            }

            Trace.WriteLine("[DBZ] Reloading COMPLETE Context from DB!");

            //Context neu laden
            Dispose();
            Init();

            throw ex;
         }
      }

      public void Dispose()
      {
         Context.Dispose();
      }


      public static void InitDB()
      {
         if (Current != null)
            throw new Exception("Already opened a session!");
         Current = new DBZugriff();

      }



      public static void CloseDB()
      {
         try
         {
            if (Current == null)
               return;

            Current.Dispose();
            Current = null;
         }
         catch (Exception e)
         {
            Trace.WriteLine(e.ToString());
         }
         finally
         {
            Trace.WriteLine("[DB] Closed DB!");
         }
      }

   }
}

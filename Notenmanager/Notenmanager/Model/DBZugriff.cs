using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Model
{
   //Hauptklasse für den Datenbankzugriff
   //Vorgehensweise bei Erstellen/Ändern des DB-Modells:
   // 1. falls vorhanden Datenbank zurücksetzen (alle bestehenden Tabellen löschen)
   // 2. falls vorhanden alle Migrationsberichte im Ordner "Migrations" löschen
   // 3. falls das Projekt das erste Mal EF nutzt: 
   //     *NuGet-Manager
   //     *Paket-Manager-Konsole>Enable-Migrations
   // 4. 

   public class DBZugriff : IDisposable
   {
      public const int STRING_MAXLENGTH = 45;

      //Stellt die jetzige Session dar
      public static DBZugriff Current { get; private set; }

      //Stellt die DB dar
      public Context Context { get; private set; }


      public DBZugriff()
      {
         Init();
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
      }
      private void Init()
      {
         Context = new Context();
      }

      /// <summary>
      /// Speichert (Hinzufügen oder Ändern) ein Objekt in die Datenbank (auch refernezierte Objekte)
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das Objekt</param>
      /// <returns>true = OK, false = Fehler</returns>
      public bool Speichern<T>(T obj, bool autoSyncDb = true) where T : class, IDBable
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

            return true;
         }
         catch (Exception e)
         {
            Trace.WriteLine("Error saving " + obj + "\r\n" + e.ToString());
         }
         return false;

      }

      /// <summary>
      /// Löscht ein Objekt (logisch) aus der Datenbank 
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das Objekt</param>
      /// <returns>true = OK, false = Fehler</returns>
      public bool Loeschen<T>(T obj) where T : class, IDBable
      {
         try
         {
            //DbSet<T> dbset = GetDbSetFromContext<T>();

            ////Delete
            //dbset.Remove(obj);

            obj.Active = false;

            Save();

            return true;
         }
         catch (Exception e)
         {
            Trace.WriteLine("Error deleting " + obj + "\r\n" + e.ToString());
         }
         return false;
      }

      /// <summary>
      /// Lädt das Objekt neu aus der Datenbank
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="obj">Das Objekt</param>
      public void Reload<T>(T obj) where T : class, IDBable
      {
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
      /// <exception cref=""></exception>
      public T SelectFirstOrDefault<T>(Func<T, bool> pred) where T : class, IDBable
      {
         return GetDbSetFromContext<T>().SingleOrDefault(pred);
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

      ///// <summary>
      ///// Lädt die gesamte Tabelle neu
      ///// </summary>
      //public void ReloadSetTable<T>() where T : class, IDBable
      //{
      //   Context.Entry(GetDbSetFromContext<T>()).Reload();
      //}


      public DbSet<T> GetDbSetFromContext<T>() where T : class, IDBable
      {
         return Context.GetDbSet<T>();
      }

      /// <summary>
      /// Beginnt eine Transaktion; Benutzung am Besten in Kombination mit using...
      /// Abbau mit: Save + Commit || Rollback
      /// </summary>
      /// <returns>Transaktions-Objekt</returns>
      public DbContextTransaction BeginTransaction()
      {
         return Context.Database.BeginTransaction();
      }


      //TEMP: async = false : Bessere Fehlernachvollziehbarkeit
      public void Save(bool async = false)
      {
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

               //Trace.WriteLine("Reloading entity!");

               //try
               //{
               //   if (eve.Entry.State == EntityState.Added)
               //   {
               //      if (!(eve.Entry.Entity is IDBable))
               //         throw new Exception("Nicht löschbar: Unbekannter Objekttyp für Datenbank!");

               //      eve.Entry.State = EntityState.Deleted;
               //   }
               //   else if (eve.Entry.State == EntityState.Modified)
               //      eve.Entry.Reload();
               //   else
               //      throw new Exception("Unbehandelbarer Status");

                  
               //}
               //catch (Exception ex2)
               //{
               //   Trace.WriteLine("Fatal: " + ex2.ToString());

               //   Trace.WriteLine("Lade Context KOMPLETT neu!");

                  
               //}
               //Trace.WriteLine("Repariert!");

            }

            Trace.WriteLine("Reloading from DB!");
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

         Trace.WriteLine("Inited DB!");
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
            Trace.WriteLine("Closed DB!");
         }
      }

   }
}

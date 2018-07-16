using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notenmanager.Model
{

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
      }
      private void Init()
      {
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

         Context = new Context();
         Context.Configuration.LazyLoadingEnabled = true;
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
         if (async)
            Context.SaveChangesAsync();
         else
            Context.SaveChanges();
      }

      public void Dispose()
      {
         //Save();
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

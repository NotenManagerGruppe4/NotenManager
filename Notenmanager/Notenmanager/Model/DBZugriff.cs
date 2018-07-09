using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMACreator.Model
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
      /// <param name="obj">Das neue Objekt</param>
      /// <returns>true = OK, false = Fehler</returns>
      public bool Speichern<T>(T obj) where T : class, IDBable
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
            Trace.WriteLine(e.ToString());
         }
         return false;

      }

      /// <summary>
      /// Löscht ein Objekt aus der Datenbank 
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das neue Objekt</param>
      /// <returns>true = OK, false = Fehler</returns>
      public bool Loeschen<T>(T obj) where T : class, IDBable
      {
         try
         {
            DbSet<T> dbset = GetDbSetFromContext<T>();

            //Delete
            dbset.Remove(obj);

            Save();

            return true;
         }
         catch (Exception e)
         {
            Trace.WriteLine(e.ToString());
         }
         return false;
      }

      /// <summary>
      /// Selektiert ein Element aus der Datenbank, wirf eine Exception falls mehrere gefunden
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="pred">Die Vorlage zum Selektieren z.B. (x => x.Id == 1)</param>
      /// <returns>Objekt vom Typ T, default(T) (meistens null) falls nichts gefunden</returns>
      /// <exception cref="InvalidOperationException">Mehrer Elemente gefunden</exception>
      public T SelectSingleOrDefault<T>(Func<T,bool> pred) where T : class, IDBable
      {
         return GetDbSetFromContext<T>().SingleOrDefault(pred);
      }

      /// <summary>
      /// Selektiert ein Element aus der Datenbank, erste Element falls mehrere gefunden
      /// </summary>
      /// <typeparam name="T">Der Typ der genutzt werden soll</typeparam>
      /// <param name="pred">Die Vorlage zum Selektieren z.B. (x => x.Id == 1)</param>
      /// <returns>Objekt vom Typ T, default(T) (meistens null) falls nichts gefunden</returns>
      /// <exception cref=""></exception>
      public T SelectFirstOrDefault<T>(Func<T, bool> pred) where T : class, IDBable
      {
         return GetDbSetFromContext<T>().SingleOrDefault(pred);
      }

      public DbSet<T> GetDbSetFromContext<T>() where T : class, IDBable
      {
         return Context.GetDbSet<T>();
      }


      private void Save(bool async = true)
      {
         if (async)
            Context.SaveChangesAsync();
         else
            Context.SaveChanges();
      }

      public void Dispose()
      {
         Save();
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
         catch(Exception e)
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

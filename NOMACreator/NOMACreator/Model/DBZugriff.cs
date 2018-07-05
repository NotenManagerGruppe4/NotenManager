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
      public static DBZugriff Current { get; private set; }

      public Context Context { get; private set; }


      public DBZugriff()
      {
         Init();
      }
      private void Init()
      {
         DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

         Context = new Context();
      }

      /// <summary>
      /// Speichert (Hinzufügen oder Ändern) ein Objekt in die Datenbank
      /// </summary>
      /// <typeparam name="T">Der Typ des Objekt</typeparam>
      /// <param name="obj">Das neue Objekt</param>
      /// <returns>true = OK, false = Fehler</returns>
      public bool Speichern<T>(T obj) where T : class, IDBable
      {
         try
         {
            DbSet<T> dbset = Context.GetDbSet<T>();

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
            DbSet<T> dbset = Context.GetDbSet<T>();

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

      
      private void Save()
      {
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
      }

      public static void CloseDB()
      {
         try
         {
            Current.Dispose();
            Current = null;
         }
         catch(Exception e)
         {
            Trace.WriteLine(e.ToString());
         }
      }

   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("ufachlehrer")]
   public class UFachLehrer : IDBable
   {
      public UFachLehrer()
      {
      }

      public int Id { get; private set; } 
      public int Stunden { get; set; }
      public DateTime? EDatum { get; set; } = null;
      public DateTime? ADatum { get; set; } = null;

      //public int IdUnterrichtsfach { get; set; }
      //public int IdLehrer { get; set; }

      public Unterrichtsfach Unterrichtsfach { get; set; }
      public Lehrer Lehrer { get; set; }

      public List<Leistung> Leistung { get; set; } = new List<Model.Leistung>();

      public bool Speichern()
      {
         return DBZugriff.Current.Speichern(this);
      }

      public bool Loeschen()
      {
         return DBZugriff.Current.Loeschen(this);
      }

   }
}

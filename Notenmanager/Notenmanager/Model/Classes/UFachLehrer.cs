using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("ufachlehrer")]
   public class UFachLehrer : IDBable
   {
      public UFachLehrer()
      {
      }

      public int Id { get; private set; }

      [Required]
      public int Stunden { get; set; }

      public DateTime? EDatum { get; set; } = null;

      public DateTime? ADatum { get; set; } = null;

      //public int IdUnterrichtsfach { get; set; }
      //public int IdLehrer { get; set; }

      [Required]
      public virtual Unterrichtsfach Unterrichtsfach { get; set; }

      [Required]
      public virtual Lehrer Lehrer { get; set; }

      public virtual List<Leistung> Leistungen { get; set; } = new List<Model.Leistung>();

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

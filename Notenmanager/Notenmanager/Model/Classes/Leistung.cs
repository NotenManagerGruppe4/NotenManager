using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   public enum Tendenz { UP, DOWN }

   [Table("leistung")]
   public class Leistung : IDBable
   {
      public Leistung()
      {  }

      public int Id { get; private set; }

      [Required]
      public DateTime Erhebungsdatum { get; set; }

      [Required]
      public int Notenstufe { get; set; }

      public Tendenz? Tendenz { get; set; } = null;

      public DateTime? LetzteÄnderung { get; set; } = null;

      //public int IdArt { get; set; }
      //public int IdFachLehrer { get; set; }
      //public int IdSchülerKlasse { get; set; }

      [Required]
      public virtual UFachLehrer UFachLehrer { get; set; }
      [Required]
      public virtual SchuelerKlasse SchuelerKlasse { get; set; }
      [Required]
      public virtual Leistungsart Leistungsart { get; set; }

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

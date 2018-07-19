using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   public enum Tendenz { UP, DOWN }

   [Table("leistung")]
   public class Leistung : IDBable
   {
      public Leistung()
      { }

      public int Id { get; private set; }
      public bool Active { get; set; } = true;

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

      public void Speichern()
      {
         DBZugriff.Current.Speichern(this);
      }

      public void Loeschen()
      {
         DBZugriff.Current.Loeschen(this);
      }

      public void Reload()
      {
         DBZugriff.Current.Reload(this);
      }
   }
}

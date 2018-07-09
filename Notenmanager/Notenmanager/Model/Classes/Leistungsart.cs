using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("leistungsart")]
   public class Leistungsart : IDBable
   {
      public Leistungsart()
      {
      }

      public int Id { get; private set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      [Required]
      public double Gewichtung { get; set; }

      [Required]
      public virtual Leistungsgruppe Gruppe { get; set; }

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

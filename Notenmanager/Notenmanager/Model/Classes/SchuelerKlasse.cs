using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("schuelerklasse")]
   public class SchuelerKlasse : IDBable
   {
      public SchuelerKlasse()
      {
      }

      public int Id { get; private set; }
      //public int IdSchueler { get; set; }
      //public int IdKlasse { get; set; }

      [Required]
      public virtual Schueler Schueler { get; set; }

      [Required]
      public virtual Klasse Klasse { get; set; }

      public virtual List<Leistung> Leistung { get; set; } = new List<Model.Leistung>();

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("schuelerklasse")]
   public class SchuelerKlasse : IDBable
   {
      public SchuelerKlasse()
      {
      }

      public int Id { get; set; }
      //public int IdSchueler { get; set; }
      //public int IdKlasse { get; set; }

      public Schueler Schueler { get; set; }
      public Klasse Klasse { get; set; }
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

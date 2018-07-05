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
      public string Bez { get; set; }
      public double Gewichtung { get; set; }
      [MaxLength(1)]
      public string Gruppe { get; set; }

      public List<Leistungsgruppe> Leistungsgruppe { get; set; } = new List<Model.Leistungsgruppe>();
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

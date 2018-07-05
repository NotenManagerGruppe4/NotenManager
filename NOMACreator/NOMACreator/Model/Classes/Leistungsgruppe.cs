using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("leistungsgruppe")]
   public class Leistungsgruppe : IDBable
   {
      public int Id { get; private set; } 
      public string Bez { get; set; }
      [Obsolete("STRING?!?")]
      public string Gewicht { get; set; }
      //public int IdLeistungsart { get; set; }

      public Leistungsart Leistungsart { get; set; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("leistungsgruppe")]
   public class Leistungsgruppe : IDBable
   {
      public int Id { get; private set; }
      public bool Active { get; set; } = true;

      [Required]
      public string Bez { get; set; }

      [Required]
      public double Gewicht { get; set; }
      //public int IdLeistungsart { get; set; }

      public virtual List<Leistungsart> Leistungsarten { get; set; } = new List<Leistungsart>();

      public bool Speichern()
      {
         return DBZugriff.Current.Speichern(this);
      }

      public bool Loeschen()
      {
         return DBZugriff.Current.Loeschen(this);
      }

      public void Reload()
      {
         DBZugriff.Current.Reload(this);
      }
   }
}

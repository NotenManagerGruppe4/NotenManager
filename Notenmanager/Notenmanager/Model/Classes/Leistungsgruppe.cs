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

      [Required,MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      [Required]
      public double Gewicht { get; set; }
      //public int IdLeistungsart { get; set; }

      public virtual List<Leistungsart> Leistungsarten { get; set; } = new List<Leistungsart>();

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

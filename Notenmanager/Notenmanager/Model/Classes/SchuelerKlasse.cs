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
      public bool Active { get; set; } = true;
      //public int IdSchueler { get; set; }
      //public int IdKlasse { get; set; }

      [Required]
      public virtual Schueler Schueler { get; set; }

      [Required]
      public virtual Klasse Klasse { get; set; }

      public virtual List<Leistung> Leistungen { get; set; } = new List<Model.Leistung>();

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("zeugnisfach")]
   public class Zeugnisfach : IDBable
   {
      public Zeugnisfach()
      {
      }

      public int Id { get; private set; }
      public bool Active { get; set; } = true;

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      [Required]
      public int Pos { get; set; }

      [Required]
      public bool AbschliessendesFach { get; set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Fachart { get; set; }

      [Required]
      public bool Vorrueckungsfach { get; set; }
      //public int IdKlasse { get; set; }

      [Required]
      public virtual Klasse Klasse { get; set; }

      public virtual List<Unterrichtsfach> Unterrichtsfaecher { get; set; } = new List<Model.Unterrichtsfach>();

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

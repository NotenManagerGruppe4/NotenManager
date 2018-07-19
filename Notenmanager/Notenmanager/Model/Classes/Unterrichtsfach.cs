using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   [Table("unterrichtsfach")]
   public class Unterrichtsfach : IDBable
   {
      public Unterrichtsfach()
      { }

      public int Id { get; private set; }
      public bool Active { get; set; } = true;

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      [Required]
      public int Stunden { get; set; }

      [Required]
      public int Pos { get; set; }
      //public int IdZeugnisfach { get; set; }

      [Required]
      public virtual Zeugnisfach Zeugnisfach { get; set; }

      public virtual List<UFachLehrer> UFaecherLehrer { get; set; } = new List<Model.UFachLehrer>();

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

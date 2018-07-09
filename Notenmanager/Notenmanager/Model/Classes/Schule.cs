using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("schule")]
   public class Schule : IDBable
   {
      public Schule()
      {
      }

      public int Id { get; private set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      public virtual List<Klasse> Klassen { get; set; } = new List<Model.Klasse>();

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

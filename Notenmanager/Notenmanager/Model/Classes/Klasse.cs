using NOMACreator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("klasse")]
   public class Klasse : IDBable
   {
      public Klasse()
      { }

      public int Id { get; private set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Bez { get; set; }

      [Required]
      public int SJ { get; set; }



      public int IdKlassenleiter { get; set; }
      public int IdStvKlassenleiter { get; set; }

      [Required]
      public virtual Schule Schule { get; set; }
      public virtual Lehrer Klassenleiter { get; set; }
      public virtual Lehrer StellvertretenderKlassenleiter { get; set; }

      public virtual List<Zeugnisfach> Faecher { get; set; } = new List<Zeugnisfach>();
      public virtual List<SchuelerKlasse> SchuelerKlassen { get; set; } = new List<SchuelerKlasse>();

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

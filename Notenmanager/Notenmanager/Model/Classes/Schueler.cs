using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   public enum Geschlecht { M, W };

   public enum Konfession { NONE, RHK, EV, ISL, SONST };

   [Table("schueler")]
   public class Schueler : IDBable
   {
      public Schueler()
      { }

      public int Id { get; private set; }

      [Required,MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Nachname { get; set; }

      [Required, MaxLength(DBZugriff.STRING_MAXLENGTH)]
      public string Vorname { get; set; }

      [Required]
      public DateTime Geburtsdatum { get; set; }

      [Required]
      public Geschlecht Geschlecht { get; set; }
      
      [Required]
      public Konfession Konfession { get; set; }

      public virtual List<SchuelerKlasse> SchuelerKlassen { get; set; } = new List<Model.SchuelerKlasse>();

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

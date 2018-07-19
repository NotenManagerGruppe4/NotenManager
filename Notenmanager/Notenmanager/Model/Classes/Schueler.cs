using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notenmanager.Model
{
   public enum Geschlecht { M, W };

   public enum Konfession { BL, RK, EV, ISL, SONST };

   [Table("schueler")]
   public class Schueler : IDBable
   {
      public Schueler()
      { }

      public int Id { get; private set; }
      public int SID { get; set; } = 0;

      public bool Active { get; set; } = true;

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

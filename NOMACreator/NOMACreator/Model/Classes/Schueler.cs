using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("schueler")]
   public class Schueler : IDBable
   {
      public Schueler()
      {
      }

      public int Id { get; private set; } 
      public string Nachname { get; set; }
      public string Vorname { get; set; }
      public DateTime Geburtsdatum { get; set; }
      [MaxLength(1)]
      public string Geschlecht { get; set; }
      [MaxLength(1)]
      public string Konfession { get; set; }

      public List<SchuelerKlasse> SchuelerKlasse { get; set; } = new List<Model.SchuelerKlasse>();

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

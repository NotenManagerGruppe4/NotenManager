using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("schueler")]
   public class Schueler : IDBable
   {
      public Schueler()
      {
      }

      public int Id { get; set; }
      public string Nachname { get; set; }
      public string Vorname { get; set; }
      public DateTime Geburtsdatum { get; set; }
      public string Geschlecht { get; set; }
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

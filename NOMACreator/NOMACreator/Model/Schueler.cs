using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Schueler
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
   }
}

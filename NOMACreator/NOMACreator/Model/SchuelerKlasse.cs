using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class SchuelerKlasse
   {
      public SchuelerKlasse()
      {
      }

      public int Id { get; set; }
      //public int IdSchueler { get; set; }
      //public int IdKlasse { get; set; }

      public Schueler Schueler { get; set; }
      public Klasse Klasse { get; set; }
      public List<Leistung> Leistung { get; set; } = new List<Model.Leistung>();
   }
}

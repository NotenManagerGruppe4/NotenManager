using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Leistung
   {
      public Leistung()
      {
      }

      public int Id { get; set; }
      public DateTime Erhebungsdatum { get; set; }
      public int Notenstufe { get; set; }
      public string Tendenz { get; set; } = null;
      public DateTime? LetzteÄnderung { get; set; } = null;

      //public int IdArt { get; set; }
      //public int IdFachLehrer { get; set; }
      //public int IdSchülerKlasse { get; set; }

      public UFachLehrer UFachLehrer { get; set; }
      public SchuelerKlasse SchuelerKlasse { get; set; }
      public Leistungsart Leistungsart { get; set; }
   }
}

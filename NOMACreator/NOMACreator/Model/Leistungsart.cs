using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Leistungsart
   {
      public Leistungsart()
      {
      }

      public int Id { get; set; }
      public string Bez { get; set; }
      public double Gewichtung { get; set; }
      public string Gruppe { get; set; }

      public List<Leistungsgruppe> Leistungsgruppe { get; set; } = new List<Model.Leistungsgruppe>();
      public List<Leistung> Leistung { get; set; } = new List<Model.Leistung>();
   }
}

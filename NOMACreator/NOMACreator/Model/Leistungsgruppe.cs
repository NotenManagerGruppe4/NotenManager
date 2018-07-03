using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Leistungsgruppe
   {
      public int Id { get; set; }
      public string Bez { get; set; }
      public string Gewicht { get; set; }
      //public int IdLeistungsart { get; set; }

      public Leistungsart Leistungsart { get; set; }
   }
}

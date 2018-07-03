using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{

   public class UFachLehrer
   {
      public UFachLehrer()
      {
      }

      public int Id { get; set; }
      public int Stunden { get; set; }
      public DateTime? EDatum { get; set; } = null;
      public DateTime? ADatum { get; set; } = null;

      //public int IdUnterrichtsfach { get; set; }
      //public int IdLehrer { get; set; }

      public Unterrichtsfach Unterrichtsfach { get; set; }
      public Lehrer Lehrer { get; set; }

      public List<Leistung> Leistung { get; set; } = new List<Model.Leistung>();
   }
}

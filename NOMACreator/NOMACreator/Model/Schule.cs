using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Schule
   {
      public Schule()
      {
      }

      public int Id { get; set; }
      public string Bez { get; set; }

      public List<Klasse> Klasse { get; set; } = new List<Model.Klasse>();
   }
}

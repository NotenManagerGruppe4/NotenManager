using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{

   public class Unterrichtsfach
   {

      public Unterrichtsfach()
      {
      }

      public int Id { get; set; }
      public string Bez { get; set; }
      public int Stunden { get; set; }
      public int Pos { get; set; }
      //public int IdZeugnisfach { get; set; }

      public Zeugnisfach Zeugnisfach { get; set; }

      public List<UFachLehrer> UFachLehrer { get; set; } = new List<Model.UFachLehrer>();
   }
}

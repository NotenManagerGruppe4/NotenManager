using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("unterrichtsfach")]
   public class Unterrichtsfach : IDBable
   {

      public Unterrichtsfach()
      {
      }

      public int Id { get; private set; } 
      public string Bez { get; set; }
      public int Stunden { get; set; }
      public int Pos { get; set; }
      //public int IdZeugnisfach { get; set; }

      public Zeugnisfach Zeugnisfach { get; set; }

      public List<UFachLehrer> UFachLehrer { get; set; } = new List<Model.UFachLehrer>();

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

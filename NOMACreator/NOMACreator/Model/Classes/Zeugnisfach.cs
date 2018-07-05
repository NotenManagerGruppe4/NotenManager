using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOMACreator.Model
{
   [Table("zeugnisfach")]
   public class Zeugnisfach : IDBable
   {
      public Zeugnisfach()
      {
      }

      public int Id { get; private set; } 
      public string Bez { get; set; }
      public int Pos { get; set; }
      public bool AbschliessendesFach { get; set; }
      public string Fachart { get; set; }
      public bool Vorrueckungsfach { get; set; }
      //public int IdKlasse { get; set; }

      public Klasse Klasse { get; set; }

      public List<Unterrichtsfach> Unterrichtsfach { get; set; } = new List<Model.Unterrichtsfach>();

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

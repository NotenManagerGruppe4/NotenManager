//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace X_EM.Model
{
   using System;
   using System.Collections.Generic;

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
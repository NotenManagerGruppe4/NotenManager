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

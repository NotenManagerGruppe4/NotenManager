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

   public class Leistung
   {
      public Leistung()
      {
      }

      public int Id { get; set; }
      public DateTime Erhebungsdatum { get; set; }
      public int Notenstufe { get; set; }
      public string Tendenz { get; set; } = null;
      public DateTime LetzteÄnderung { get; set; } = DateTime.MinValue;

      //public int IdArt { get; set; }
      //public int IdFachLehrer { get; set; }
      //public int IdSchülerKlasse { get; set; }

      public UFachLehrer UFachLehrer { get; set; }
      public SchuelerKlasse SchuelerKlasse { get; set; }
      public Leistungsart Leistungsart { get; set; }
   }
}

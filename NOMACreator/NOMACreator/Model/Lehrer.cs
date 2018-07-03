using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Lehrer
   {
      public Lehrer()
      {
      }

      public int Id { get; set; }
      public string Kürzel { get; set; }
      public string Nachname { get; set; }
      public string Vorname { get; set; }
      public string Dienstbezeichnung { get; set; } = null;

      public List<UFachLehrer> UFachLehrer { get; set; } = new List<UFachLehrer>();
      public List<Klasse> Klassenleiter { get; set; } = new List<Klasse>();
      public List<Klasse> StellvertretenderKlassenleiter { get; set; } = new List<Klasse>();
   }
}

using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
   public class Klasse
   {
      public Klasse()
      {
      }

      public int Id { get; set; }
      public string Bez { get; set; }
      public int SJ { get; set; }
      //public int IdSchule { get; set; }
      //public int IdKlassenleiter { get; set; }
      //public int IdStvKlassenleiter { get; set; }

      public Schule Schule { get; set; }
      public List<Zeugnisfach> Fach { get; set; } = new List<Zeugnisfach>();
      public List<SchuelerKlasse> SchuelerKlasse { get; set; } = new List<SchuelerKlasse>();
      public Lehrer Klassenleiter { get; set; }
      public Lehrer StellvertretenderKlassenleiter { get; set; }
   }
}

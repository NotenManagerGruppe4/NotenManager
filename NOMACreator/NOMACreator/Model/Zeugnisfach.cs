using System;
using System.Collections.Generic;

namespace NOMACreator.Model
{
    public class Zeugnisfach
    {
        public Zeugnisfach()
        {
        }
    
        public int Id { get; set; }
        public string Bez { get; set; }
        public int Pos { get; set; }
        public bool AbschliessendesFach { get; set; }
        public string Fachart { get; set; }
        public bool Vorrueckungsfach { get; set; }
        //public int IdKlasse { get; set; }
    
        public Klasse Klasse { get; set; }

      public List<Unterrichtsfach> Unterrichtsfach { get; set; } = new List<Model.Unterrichtsfach>();
    }
}

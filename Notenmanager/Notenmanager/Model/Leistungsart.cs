//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace X_EM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Leistungsart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Leistungsart()
        {
            this.Leistungsgruppe = new HashSet<Leistungsgruppe>();
            this.Leistung = new HashSet<Leistung>();
        }
    
        public int Id { get; set; }
        public string Bez { get; set; }
        public double Gewichtung { get; set; }
        public string Gruppe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leistungsgruppe> Leistungsgruppe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leistung> Leistung { get; set; }
    }
}
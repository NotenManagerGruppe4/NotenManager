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
    
    public partial class SchuelerKlasse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchuelerKlasse()
        {
            this.Leistung = new HashSet<Leistung>();
        }
    
        public int Id { get; set; }
        public int IdSchueler { get; set; }
        public int IdKlasse { get; set; }
    
        public virtual Schueler Schueler { get; set; }
        public virtual Klasse Klasse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leistung> Leistung { get; set; }
    }
}

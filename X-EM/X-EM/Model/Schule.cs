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
    
    public partial class Schule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schule()
        {
            this.Klasse = new HashSet<Klasse>();
        }
    
        public int Id { get; set; }
        public string Bez { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klasse> Klasse { get; set; }
    }
}

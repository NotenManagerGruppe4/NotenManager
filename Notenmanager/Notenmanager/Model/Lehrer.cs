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
    
    public partial class Lehrer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lehrer()
        {
            this.Dienstbezeichnung = "NULL";
            this.UFachLehrer = new HashSet<UFachLehrer>();
            this.Klasse = new HashSet<Klasse>();
            this.Klasse1 = new HashSet<Klasse>();
        }
    
        public int Id { get; set; }
        public string Kürzel { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Dienstbezeichnung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UFachLehrer> UFachLehrer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klasse> Klasse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klasse> Klasse1 { get; set; }
    }
}
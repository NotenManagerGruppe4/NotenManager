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
    
    public partial class Schueler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schueler()
        {
            this.SchuelerKlasse = new HashSet<SchuelerKlasse>();
        }
    
        public int Id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public System.DateTime Geburtsdatum { get; set; }
        public string Geschlecht { get; set; }
        public string Konfession { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchuelerKlasse> SchuelerKlasse { get; set; }
    }
}

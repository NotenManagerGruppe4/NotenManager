//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

<<<<<<< HEAD
namespace X_EM
=======
namespace X_EM.Model
>>>>>>> ffd4214d6df6defe5bd5aa74435e6e17bd2b3e58
{
    using System;
    using System.Collections.Generic;
    
    public partial class Klasse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klasse()
        {
            this.Fach = new HashSet<Zeugnisfach>();
            this.SchuelerKlasse = new HashSet<SchuelerKlasse>();
        }
    
        public int Id { get; set; }
        public string Bez { get; set; }
        public int SJ { get; set; }
        public int IdSchule { get; set; }
        public int IdKlassenleiter { get; set; }
        public int IdStvKlassenleiter { get; set; }
    
        public virtual Schule Schule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zeugnisfach> Fach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchuelerKlasse> SchuelerKlasse { get; set; }
        public virtual Lehrer Lehrer { get; set; }
        public virtual Lehrer Lehrer1 { get; set; }
    }
}

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
    
    public partial class UFachLehrer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UFachLehrer()
        {
            this.Leistung = new HashSet<Leistung>();
        }
    
        public int Id { get; set; }
        public int Stunden { get; set; }
        public int IdUnterrichtsfach { get; set; }
        public int IdLehrer { get; set; }
        public System.DateTime EDatum { get; set; }
        public System.DateTime ADatum { get; set; }
    
        public virtual Unterrichtsfach Unterrichtsfach { get; set; }
        public virtual Lehrer Lehrer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leistung> Leistung { get; set; }
    }
}

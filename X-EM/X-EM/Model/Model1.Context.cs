﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace X_EM.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Model1Container : DbContext
    {
        public Model1Container()
            : base("name=MySQLSchuleNoma4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Klasse> KlasseSet { get; set; }
        public virtual DbSet<Schule> SchuleSet { get; set; }
        public virtual DbSet<Zeugnisfach> ZeugnisfachSet { get; set; }
        public virtual DbSet<Unterrichtsfach> UnterrichtsfachSet { get; set; }
        public virtual DbSet<Lehrer> LehrerSet { get; set; }
        public virtual DbSet<UFachLehrer> UFachLehrerSet { get; set; }
        public virtual DbSet<SchuelerKlasse> SchuelerKlasseSet { get; set; }
        public virtual DbSet<Schueler> SchuelerSet { get; set; }
        public virtual DbSet<Leistung> LeistungSet { get; set; }
        public virtual DbSet<Leistungsart> LeistungsartSet { get; set; }
        public virtual DbSet<Leistungsgruppe> LeistungsgruppeSet { get; set; }
    }
}

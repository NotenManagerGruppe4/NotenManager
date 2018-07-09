using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Notenmanager.Model
{

   [DbConfigurationType(typeof(MySqlEFConfiguration))]
   public partial class Context : DbContext
   {
      public Context()
          : base("name=MySQLSchuleNoma4")
      {
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

      public DbSet<T> GetDbSet<T>() where T : class, IDBable
      {
         Type t = typeof(T);
         DbSet re = null;

         if (t == typeof(Klasse))
            re = KlasseSet;
         else if (t == typeof(Schule))
            re = SchuleSet;
         else if (t == typeof(Zeugnisfach))
            re = ZeugnisfachSet;
         else if (t == typeof(Unterrichtsfach))
            re = UnterrichtsfachSet;
         else if (t == typeof(Lehrer))
            re = LehrerSet;
         else if (t == typeof(UFachLehrer))
            re = UFachLehrerSet;
         else if (t == typeof(SchuelerKlasse))
            re = SchuelerKlasseSet;
         else if (t == typeof(Schueler))
            re = SchuelerSet;
         else if (t == typeof(Leistung))
            re = LeistungSet;
         else if (t == typeof(Leistungsart))
            re = LeistungsartSet;
         else if (t == typeof(Leistungsgruppe))
            re = LeistungsgruppeSet;

         return re.Cast<T>();
      }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Klasse>()
            .HasRequired<Lehrer>(k => k.Klassenleiter)
            .WithMany(l => l.Klassenleiter)
            .HasForeignKey<int>(k => k.IdKlassenleiter)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Klasse>()
            .HasRequired<Lehrer>(k => k.StellvertretenderKlassenleiter)
            .WithMany(l => l.StellvertretenderKlassenleiter)
            .HasForeignKey<int>(k => k.IdStvKlassenleiter)
            .WillCascadeOnDelete(false);
      }
   }


}

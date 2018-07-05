namespace NOMACreator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenerateNoma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Klasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        SJ = c.Int(nullable: false),
                        Lehrer_Id = c.Int(),
                        Lehrer_Id1 = c.Int(),
                        Klassenleiter_Id = c.Int(),
                        Schule_Id = c.Int(),
                        StellvertretenderKlassenleiter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lehrers", t => t.Lehrer_Id)
                .ForeignKey("dbo.Lehrers", t => t.Lehrer_Id1)
                .ForeignKey("dbo.Lehrers", t => t.Klassenleiter_Id)
                .ForeignKey("dbo.Schules", t => t.Schule_Id)
                .ForeignKey("dbo.Lehrers", t => t.StellvertretenderKlassenleiter_Id)
                .Index(t => t.Lehrer_Id)
                .Index(t => t.Lehrer_Id1)
                .Index(t => t.Klassenleiter_Id)
                .Index(t => t.Schule_Id)
                .Index(t => t.StellvertretenderKlassenleiter_Id);
            
            CreateTable(
                "dbo.Zeugnisfaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Pos = c.Int(nullable: false),
                        AbschliessendesFach = c.Boolean(nullable: false),
                        Fachart = c.String(unicode: false),
                        Vorrueckungsfach = c.Boolean(nullable: false),
                        Klasse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klasses", t => t.Klasse_Id)
                .Index(t => t.Klasse_Id);
            
            CreateTable(
                "dbo.Unterrichtsfaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Stunden = c.Int(nullable: false),
                        Pos = c.Int(nullable: false),
                        Zeugnisfach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zeugnisfaches", t => t.Zeugnisfach_Id)
                .Index(t => t.Zeugnisfach_Id);
            
            CreateTable(
                "dbo.UFachLehrers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stunden = c.Int(nullable: false),
                        EDatum = c.DateTime(precision: 0),
                        ADatum = c.DateTime(precision: 0),
                        Lehrer_Id = c.Int(),
                        Unterrichtsfach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lehrers", t => t.Lehrer_Id)
                .ForeignKey("dbo.Unterrichtsfaches", t => t.Unterrichtsfach_Id)
                .Index(t => t.Lehrer_Id)
                .Index(t => t.Unterrichtsfach_Id);
            
            CreateTable(
                "dbo.Lehrers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kürzel = c.String(unicode: false),
                        Nachname = c.String(unicode: false),
                        Vorname = c.String(unicode: false),
                        Dienstbezeichnung = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Leistungs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Erhebungsdatum = c.DateTime(nullable: false, precision: 0),
                        Notenstufe = c.Int(nullable: false),
                        Tendenz = c.String(unicode: false),
                        LetzteÄnderung = c.DateTime(precision: 0),
                        Leistungsart_Id = c.Int(),
                        SchuelerKlasse_Id = c.Int(),
                        UFachLehrer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leistungsarts", t => t.Leistungsart_Id)
                .ForeignKey("dbo.SchuelerKlasses", t => t.SchuelerKlasse_Id)
                .ForeignKey("dbo.UFachLehrers", t => t.UFachLehrer_Id)
                .Index(t => t.Leistungsart_Id)
                .Index(t => t.SchuelerKlasse_Id)
                .Index(t => t.UFachLehrer_Id);
            
            CreateTable(
                "dbo.Leistungsarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Gewichtung = c.Double(nullable: false),
                        Gruppe = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Leistungsgruppes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Gewicht = c.String(unicode: false),
                        Leistungsart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leistungsarts", t => t.Leistungsart_Id)
                .Index(t => t.Leistungsart_Id);
            
            CreateTable(
                "dbo.SchuelerKlasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Klasse_Id = c.Int(),
                        Schueler_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klasses", t => t.Klasse_Id)
                .ForeignKey("dbo.Schuelers", t => t.Schueler_Id)
                .Index(t => t.Klasse_Id)
                .Index(t => t.Schueler_Id);
            
            CreateTable(
                "dbo.Schuelers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nachname = c.String(unicode: false),
                        Vorname = c.String(unicode: false),
                        Geburtsdatum = c.DateTime(nullable: false, precision: 0),
                        Geschlecht = c.String(unicode: false),
                        Konfession = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klasses", "StellvertretenderKlassenleiter_Id", "dbo.Lehrers");
            DropForeignKey("dbo.Klasses", "Schule_Id", "dbo.Schules");
            DropForeignKey("dbo.Klasses", "Klassenleiter_Id", "dbo.Lehrers");
            DropForeignKey("dbo.Unterrichtsfaches", "Zeugnisfach_Id", "dbo.Zeugnisfaches");
            DropForeignKey("dbo.UFachLehrers", "Unterrichtsfach_Id", "dbo.Unterrichtsfaches");
            DropForeignKey("dbo.Leistungs", "UFachLehrer_Id", "dbo.UFachLehrers");
            DropForeignKey("dbo.SchuelerKlasses", "Schueler_Id", "dbo.Schuelers");
            DropForeignKey("dbo.Leistungs", "SchuelerKlasse_Id", "dbo.SchuelerKlasses");
            DropForeignKey("dbo.SchuelerKlasses", "Klasse_Id", "dbo.Klasses");
            DropForeignKey("dbo.Leistungsgruppes", "Leistungsart_Id", "dbo.Leistungsarts");
            DropForeignKey("dbo.Leistungs", "Leistungsart_Id", "dbo.Leistungsarts");
            DropForeignKey("dbo.UFachLehrers", "Lehrer_Id", "dbo.Lehrers");
            DropForeignKey("dbo.Klasses", "Lehrer_Id1", "dbo.Lehrers");
            DropForeignKey("dbo.Klasses", "Lehrer_Id", "dbo.Lehrers");
            DropForeignKey("dbo.Zeugnisfaches", "Klasse_Id", "dbo.Klasses");
            DropIndex("dbo.SchuelerKlasses", new[] { "Schueler_Id" });
            DropIndex("dbo.SchuelerKlasses", new[] { "Klasse_Id" });
            DropIndex("dbo.Leistungsgruppes", new[] { "Leistungsart_Id" });
            DropIndex("dbo.Leistungs", new[] { "UFachLehrer_Id" });
            DropIndex("dbo.Leistungs", new[] { "SchuelerKlasse_Id" });
            DropIndex("dbo.Leistungs", new[] { "Leistungsart_Id" });
            DropIndex("dbo.UFachLehrers", new[] { "Unterrichtsfach_Id" });
            DropIndex("dbo.UFachLehrers", new[] { "Lehrer_Id" });
            DropIndex("dbo.Unterrichtsfaches", new[] { "Zeugnisfach_Id" });
            DropIndex("dbo.Zeugnisfaches", new[] { "Klasse_Id" });
            DropIndex("dbo.Klasses", new[] { "StellvertretenderKlassenleiter_Id" });
            DropIndex("dbo.Klasses", new[] { "Schule_Id" });
            DropIndex("dbo.Klasses", new[] { "Klassenleiter_Id" });
            DropIndex("dbo.Klasses", new[] { "Lehrer_Id1" });
            DropIndex("dbo.Klasses", new[] { "Lehrer_Id" });
            DropTable("dbo.Schules");
            DropTable("dbo.Schuelers");
            DropTable("dbo.SchuelerKlasses");
            DropTable("dbo.Leistungsgruppes");
            DropTable("dbo.Leistungsarts");
            DropTable("dbo.Leistungs");
            DropTable("dbo.Lehrers");
            DropTable("dbo.UFachLehrers");
            DropTable("dbo.Unterrichtsfaches");
            DropTable("dbo.Zeugnisfaches");
            DropTable("dbo.Klasses");
        }
    }
}

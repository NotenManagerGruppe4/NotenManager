namespace NOMACreator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenerateNoma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.klasse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        SJ = c.Int(nullable: false),
                        IdKlassenleiter = c.Int(nullable: false),
                        IdStvKlassenleiter = c.Int(nullable: false),
                        Schule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.lehrer", t => t.IdKlassenleiter)
                .ForeignKey("dbo.schule", t => t.Schule_Id)
                .ForeignKey("dbo.lehrer", t => t.IdStvKlassenleiter)
                .Index(t => t.IdKlassenleiter)
                .Index(t => t.IdStvKlassenleiter)
                .Index(t => t.Schule_Id);
            
            CreateTable(
                "dbo.zeugnisfach",
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
                .ForeignKey("dbo.klasse", t => t.Klasse_Id)
                .Index(t => t.Klasse_Id);
            
            CreateTable(
                "dbo.unterrichtsfach",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Stunden = c.Int(nullable: false),
                        Pos = c.Int(nullable: false),
                        Zeugnisfach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.zeugnisfach", t => t.Zeugnisfach_Id)
                .Index(t => t.Zeugnisfach_Id);
            
            CreateTable(
                "dbo.ufachlehrer",
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
                .ForeignKey("dbo.lehrer", t => t.Lehrer_Id)
                .ForeignKey("dbo.unterrichtsfach", t => t.Unterrichtsfach_Id)
                .Index(t => t.Lehrer_Id)
                .Index(t => t.Unterrichtsfach_Id);
            
            CreateTable(
                "dbo.lehrer",
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
                "dbo.leistung",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Erhebungsdatum = c.DateTime(nullable: false, precision: 0),
                        Notenstufe = c.Int(nullable: false),
                        Tendenz = c.String(maxLength: 1, storeType: "nvarchar"),
                        LetzteÄnderung = c.DateTime(precision: 0),
                        Leistungsart_Id = c.Int(),
                        SchuelerKlasse_Id = c.Int(),
                        UFachLehrer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.leistungsart", t => t.Leistungsart_Id)
                .ForeignKey("dbo.schuelerklasse", t => t.SchuelerKlasse_Id)
                .ForeignKey("dbo.ufachlehrer", t => t.UFachLehrer_Id)
                .Index(t => t.Leistungsart_Id)
                .Index(t => t.SchuelerKlasse_Id)
                .Index(t => t.UFachLehrer_Id);
            
            CreateTable(
                "dbo.leistungsart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Gewichtung = c.Double(nullable: false),
                        Gruppe = c.String(maxLength: 1, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.leistungsgruppe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                        Gewicht = c.String(unicode: false),
                        Leistungsart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.leistungsart", t => t.Leistungsart_Id)
                .Index(t => t.Leistungsart_Id);
            
            CreateTable(
                "dbo.schuelerklasse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Klasse_Id = c.Int(),
                        Schueler_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.klasse", t => t.Klasse_Id)
                .ForeignKey("dbo.schueler", t => t.Schueler_Id)
                .Index(t => t.Klasse_Id)
                .Index(t => t.Schueler_Id);
            
            CreateTable(
                "dbo.schueler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nachname = c.String(unicode: false),
                        Vorname = c.String(unicode: false),
                        Geburtsdatum = c.DateTime(nullable: false, precision: 0),
                        Geschlecht = c.String(maxLength: 1, storeType: "nvarchar"),
                        Konfession = c.String(maxLength: 1, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.schule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bez = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.klasse", "IdStvKlassenleiter", "dbo.lehrer");
            DropForeignKey("dbo.klasse", "Schule_Id", "dbo.schule");
            DropForeignKey("dbo.klasse", "IdKlassenleiter", "dbo.lehrer");
            DropForeignKey("dbo.unterrichtsfach", "Zeugnisfach_Id", "dbo.zeugnisfach");
            DropForeignKey("dbo.ufachlehrer", "Unterrichtsfach_Id", "dbo.unterrichtsfach");
            DropForeignKey("dbo.leistung", "UFachLehrer_Id", "dbo.ufachlehrer");
            DropForeignKey("dbo.schuelerklasse", "Schueler_Id", "dbo.schueler");
            DropForeignKey("dbo.leistung", "SchuelerKlasse_Id", "dbo.schuelerklasse");
            DropForeignKey("dbo.schuelerklasse", "Klasse_Id", "dbo.klasse");
            DropForeignKey("dbo.leistungsgruppe", "Leistungsart_Id", "dbo.leistungsart");
            DropForeignKey("dbo.leistung", "Leistungsart_Id", "dbo.leistungsart");
            DropForeignKey("dbo.ufachlehrer", "Lehrer_Id", "dbo.lehrer");
            DropForeignKey("dbo.zeugnisfach", "Klasse_Id", "dbo.klasse");
            DropIndex("dbo.schuelerklasse", new[] { "Schueler_Id" });
            DropIndex("dbo.schuelerklasse", new[] { "Klasse_Id" });
            DropIndex("dbo.leistungsgruppe", new[] { "Leistungsart_Id" });
            DropIndex("dbo.leistung", new[] { "UFachLehrer_Id" });
            DropIndex("dbo.leistung", new[] { "SchuelerKlasse_Id" });
            DropIndex("dbo.leistung", new[] { "Leistungsart_Id" });
            DropIndex("dbo.ufachlehrer", new[] { "Unterrichtsfach_Id" });
            DropIndex("dbo.ufachlehrer", new[] { "Lehrer_Id" });
            DropIndex("dbo.unterrichtsfach", new[] { "Zeugnisfach_Id" });
            DropIndex("dbo.zeugnisfach", new[] { "Klasse_Id" });
            DropIndex("dbo.klasse", new[] { "Schule_Id" });
            DropIndex("dbo.klasse", new[] { "IdStvKlassenleiter" });
            DropIndex("dbo.klasse", new[] { "IdKlassenleiter" });
            DropTable("dbo.schule");
            DropTable("dbo.schueler");
            DropTable("dbo.schuelerklasse");
            DropTable("dbo.leistungsgruppe");
            DropTable("dbo.leistungsart");
            DropTable("dbo.leistung");
            DropTable("dbo.lehrer");
            DropTable("dbo.ufachlehrer");
            DropTable("dbo.unterrichtsfach");
            DropTable("dbo.zeugnisfach");
            DropTable("dbo.klasse");
        }
    }
}

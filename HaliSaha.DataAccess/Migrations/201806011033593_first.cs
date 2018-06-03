namespace HaliSaha.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musteris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        Soyad = c.String(),
                        Telefon = c.String(),
                        MailAdresi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rezervasyons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MusteriId = c.Int(nullable: false),
                        SahaId = c.Int(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                        Saat = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Musteris", t => t.MusteriId, cascadeDelete: true)
                .ForeignKey("dbo.Sahas", t => t.SahaId, cascadeDelete: true)
                .Index(t => t.MusteriId)
                .Index(t => t.SahaId);
            
            CreateTable(
                "dbo.Sahas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervasyons", "SahaId", "dbo.Sahas");
            DropForeignKey("dbo.Rezervasyons", "MusteriId", "dbo.Musteris");
            DropIndex("dbo.Rezervasyons", new[] { "SahaId" });
            DropIndex("dbo.Rezervasyons", new[] { "MusteriId" });
            DropTable("dbo.Sahas");
            DropTable("dbo.Rezervasyons");
            DropTable("dbo.Musteris");
        }
    }
}

namespace HaliSaha.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rezervasyons", "Saat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rezervasyons", "Saat", c => c.Time(nullable: false, precision: 7));
        }
    }
}

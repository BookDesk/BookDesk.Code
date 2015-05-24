namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updtUsertblmodifydt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ModifiedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ModifiedDateTime");
        }
    }
}

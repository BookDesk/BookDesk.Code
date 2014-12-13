namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwoColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Publisher", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Book", "Year", c => c.String(nullable: false, maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Year");
            DropColumn("dbo.Book", "Publisher");
        }
    }
}

namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Book", "Author", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Book", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Book", "Locality", c => c.String(maxLength: 100));
            AlterColumn("dbo.Book", "City", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Book", "State", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "State", c => c.String());
            AlterColumn("dbo.Book", "City", c => c.String());
            AlterColumn("dbo.Book", "Locality", c => c.String());
            AlterColumn("dbo.Book", "Category", c => c.String());
            AlterColumn("dbo.Book", "Author", c => c.String());
            AlterColumn("dbo.Book", "Title", c => c.String());
        }
    }
}

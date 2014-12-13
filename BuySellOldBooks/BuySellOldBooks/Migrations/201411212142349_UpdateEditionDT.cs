namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditionDT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Edition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "Edition", c => c.Int(nullable: false));
        }
    }
}

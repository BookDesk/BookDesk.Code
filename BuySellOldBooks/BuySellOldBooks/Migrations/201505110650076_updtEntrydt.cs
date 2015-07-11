namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updtEntrydt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "EntryDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "EntryDateTime", c => c.DateTime(nullable: false));
        }
    }
}

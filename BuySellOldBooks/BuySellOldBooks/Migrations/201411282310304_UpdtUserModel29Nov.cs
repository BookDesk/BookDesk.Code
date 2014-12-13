namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdtUserModel29Nov : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String());
        }
    }
}

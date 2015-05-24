namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatUserTblModifDateProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "ModifiedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "ModifiedDateTime", c => c.DateTime(nullable: false));
        }
    }
}

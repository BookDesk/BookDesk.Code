namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookTableStateColDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "State", c => c.String(maxLength: 100));
        }
    }
}

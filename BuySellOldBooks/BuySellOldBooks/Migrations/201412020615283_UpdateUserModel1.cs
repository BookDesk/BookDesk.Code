namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "NickName", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Email", c => c.String(nullable: false));
            DropColumn("dbo.User", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Password", c => c.String(maxLength: 50));
            AlterColumn("dbo.User", "Email", c => c.String());
            AlterColumn("dbo.User", "NickName", c => c.String());
        }
    }
}

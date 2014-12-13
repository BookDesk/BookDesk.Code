namespace BuySellOldBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        NickName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Institution = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EntryDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        Edition = c.Int(nullable: false),
                        Category = c.String(),
                        Locality = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Price = c.Int(nullable: false),
                        Review = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EntryDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageID = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        From = c.String(),
                        To = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EntryDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.BookUser",
                c => new
                    {
                        Book_BookID = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookID, t.User_UserId })
                .ForeignKey("dbo.Book", t => t.Book_BookID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Book_BookID)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.MessageUser",
                c => new
                    {
                        Message_MessageID = c.String(nullable: false, maxLength: 128),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_MessageID, t.User_UserId })
                .ForeignKey("dbo.Message", t => t.Message_MessageID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Message_MessageID)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MessageUser", new[] { "User_UserId" });
            DropIndex("dbo.MessageUser", new[] { "Message_MessageID" });
            DropIndex("dbo.BookUser", new[] { "User_UserId" });
            DropIndex("dbo.BookUser", new[] { "Book_BookID" });
            DropForeignKey("dbo.MessageUser", "User_UserId", "dbo.User");
            DropForeignKey("dbo.MessageUser", "Message_MessageID", "dbo.Message");
            DropForeignKey("dbo.BookUser", "User_UserId", "dbo.User");
            DropForeignKey("dbo.BookUser", "Book_BookID", "dbo.Book");
            DropTable("dbo.MessageUser");
            DropTable("dbo.BookUser");
            DropTable("dbo.City");
            DropTable("dbo.Category");
            DropTable("dbo.Message");
            DropTable("dbo.Book");
            DropTable("dbo.User");
        }
    }
}

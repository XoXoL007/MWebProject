namespace WebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablesOrdersAndUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        BookId = c.Int(),
                        DateOfIssue = c.DateTime(nullable: false),
                        EstimatedDeliveryDate = c.DateTime(nullable: false),
                        DateOfCompletion = c.DateTime(nullable: false),
                        Books_Id = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Books_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Books_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFirstName = c.String(nullable: false, maxLength: 150),
                        UserLastName = c.String(nullable: false, maxLength: 150),
                        UserEmail = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "Books_Id", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "Users_Id" });
            DropIndex("dbo.Orders", new[] { "Books_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
        }
    }
}

namespace WebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(maxLength: 100, unicode: false),
                    LastName = c.String(maxLength: 100, unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AuthorId = c.Int(nullable: false),
                    Title = c.String(nullable: false, maxLength: 150, unicode: false),
                    Pages = c.Int(),
                    Price = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.sysdiagrams",
                c => new
                {
                    diagram_id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 128),
                    principal_id = c.Int(nullable: false),
                    version = c.Int(),
                    definition = c.Binary(),
                })
                .PrimaryKey(t => t.diagram_id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}

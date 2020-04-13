namespace INT1448.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageBookTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 500),
                        Caption = c.String(nullable: false, maxLength: 500),
                        IsDefault = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        FileSize = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookImages", "BookId", "dbo.Books");
            DropIndex("dbo.BookImages", new[] { "BookId" });
            DropTable("dbo.BookImages");
        }
    }
}

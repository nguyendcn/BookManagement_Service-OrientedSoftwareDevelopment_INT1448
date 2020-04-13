namespace INT1448.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_annotation_no_mapped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PublisherID", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "PublisherID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Books", "PublisherID");
            AddForeignKey("dbo.Books", "PublisherID", "dbo.Publishers", "ID", cascadeDelete: true);
        }
    }
}

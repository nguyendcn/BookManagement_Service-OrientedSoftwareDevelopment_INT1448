namespace INT1448.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_annotation_no_mapped : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Books", "PublisherID");
            AddForeignKey("dbo.Books", "PublisherID", "dbo.Publishers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}

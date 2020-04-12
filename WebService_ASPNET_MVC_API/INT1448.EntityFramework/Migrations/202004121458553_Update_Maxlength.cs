namespace INT1448.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Maxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookCategories", "Name", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.BookCategories", "Alias", c => c.String(nullable: false, maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookCategories", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AlterColumn("dbo.BookCategories", "Name", c => c.String(nullable: false, maxLength: 256));
        }
    }
}

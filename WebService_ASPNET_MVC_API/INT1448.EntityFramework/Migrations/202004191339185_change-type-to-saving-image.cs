namespace INT1448.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypetosavingimage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookImages", "Caption");
            DropColumn("dbo.BookImages", "IsDefault");
            DropColumn("dbo.BookImages", "DateCreated");
            DropColumn("dbo.BookImages", "SortOrder");
            DropColumn("dbo.BookImages", "FileSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookImages", "FileSize", c => c.Long(nullable: false));
            AddColumn("dbo.BookImages", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.BookImages", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookImages", "IsDefault", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookImages", "Caption", c => c.String(nullable: false, maxLength: 500));
        }
    }
}

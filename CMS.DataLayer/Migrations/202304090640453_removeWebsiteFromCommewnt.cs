namespace CMS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeWebsiteFromCommewnt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PageComments", "WebSite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PageComments", "WebSite", c => c.String(maxLength: 200));
        }
    }
}

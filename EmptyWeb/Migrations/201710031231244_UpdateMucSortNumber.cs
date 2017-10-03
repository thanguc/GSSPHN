namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMucSortNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mucs", "SortNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mucs", "SortNumber");
        }
    }
}

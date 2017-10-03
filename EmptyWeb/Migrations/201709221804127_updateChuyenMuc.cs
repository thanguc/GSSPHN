namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateChuyenMuc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChuyenMucs", "NoiDung", c => c.String());
            AddColumn("dbo.ChuyenMucs", "Url", c => c.String());
            AddColumn("dbo.ChuyenMucs", "SortNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChuyenMucs", "SortNumber");
            DropColumn("dbo.ChuyenMucs", "Url");
            DropColumn("dbo.ChuyenMucs", "NoiDung");
        }
    }
}

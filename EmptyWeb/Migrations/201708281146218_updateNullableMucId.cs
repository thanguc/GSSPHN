namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNullableMucId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs");
            DropIndex("dbo.ChuyenMucs", new[] { "MucId" });
            AlterColumn("dbo.ChuyenMucs", "MucId", c => c.Guid());
            CreateIndex("dbo.ChuyenMucs", "MucId");
            AddForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs", "MucId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs");
            DropIndex("dbo.ChuyenMucs", new[] { "MucId" });
            AlterColumn("dbo.ChuyenMucs", "MucId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ChuyenMucs", "MucId");
            AddForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs", "MucId", cascadeDelete: true);
        }
    }
}

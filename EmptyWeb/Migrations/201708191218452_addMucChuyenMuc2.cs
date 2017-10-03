namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMucChuyenMuc2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuyenMucs",
                c => new
                    {
                        ChuyenMucId = c.Guid(nullable: false),
                        TieuDe = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                        MucId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChuyenMucId)
                .ForeignKey("dbo.Mucs", t => t.MucId, cascadeDelete: true)
                .Index(t => t.MucId);
            
            CreateTable(
                "dbo.Mucs",
                c => new
                    {
                        MucId = c.Guid(nullable: false),
                        TieuDe = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MucId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs");
            DropIndex("dbo.ChuyenMucs", new[] { "MucId" });
            DropTable("dbo.Mucs");
            DropTable("dbo.ChuyenMucs");
        }
    }
}

namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTrinhdo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrinhDoes",
                c => new
                    {
                        TrinhDoId = c.Int(nullable: false, identity: true),
                        TenTrinhDo = c.String(),
                    })
                .PrimaryKey(t => t.TrinhDoId);
            
            AddColumn("dbo.DangKyGiaSus", "TrinhDoID", c => c.Int());
            AddColumn("dbo.TimGiaSus", "TrinhDoId", c => c.Int());
            CreateIndex("dbo.DangKyGiaSus", "TrinhDoID");
            CreateIndex("dbo.TimGiaSus", "TrinhDoId");
            AddForeignKey("dbo.DangKyGiaSus", "TrinhDoID", "dbo.TrinhDoes", "TrinhDoId");
            AddForeignKey("dbo.TimGiaSus", "TrinhDoId", "dbo.TrinhDoes", "TrinhDoId");
            DropColumn("dbo.DangKyGiaSus", "TrinhDo");
            DropColumn("dbo.TimGiaSus", "TrinhDo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimGiaSus", "TrinhDo", c => c.Int());
            AddColumn("dbo.DangKyGiaSus", "TrinhDo", c => c.Int());
            DropForeignKey("dbo.TimGiaSus", "TrinhDoId", "dbo.TrinhDoes");
            DropForeignKey("dbo.DangKyGiaSus", "TrinhDoID", "dbo.TrinhDoes");
            DropIndex("dbo.TimGiaSus", new[] { "TrinhDoId" });
            DropIndex("dbo.DangKyGiaSus", new[] { "TrinhDoID" });
            DropColumn("dbo.TimGiaSus", "TrinhDoId");
            DropColumn("dbo.DangKyGiaSus", "TrinhDoID");
            DropTable("dbo.TrinhDoes");
        }
    }
}

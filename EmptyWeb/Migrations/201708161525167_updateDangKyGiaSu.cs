namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDangKyGiaSu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DangKyGiaSus", "GioiTinh", c => c.Int());
            AlterColumn("dbo.DangKyGiaSus", "TrinhDo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DangKyGiaSus", "TrinhDo", c => c.Int(nullable: false));
            AlterColumn("dbo.DangKyGiaSus", "GioiTinh", c => c.Int(nullable: false));
        }
    }
}

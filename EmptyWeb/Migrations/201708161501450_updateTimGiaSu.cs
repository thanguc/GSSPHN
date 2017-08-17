namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTimGiaSu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimGiaSus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        Email = c.String(),
                        YeuCau = c.String(),
                        GioiTinh = c.Int(),
                        TrinhDo = c.Int(),
                        TrinhDoKhac = c.String(),
                        DonVi = c.String(),
                        ChuyenNganh = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DangKyGiaSus", "NgayTao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DangKyGiaSus", "NgayTao");
            DropTable("dbo.TimGiaSus");
        }
    }
}

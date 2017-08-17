namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DangKyGiaSus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(),
                        NgaySinh = c.String(),
                        GioiTinh = c.Int(nullable: false),
                        QueQuanID = c.Int(),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        Email = c.String(),
                        AnhThe = c.String(),
                        TrinhDo = c.Int(nullable: false),
                        TrinhDoKhac = c.String(),
                        DonVi = c.String(),
                        ChuyenNganh = c.String(),
                        GioiThieu = c.String(),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QueQuans", t => t.QueQuanID)
                .Index(t => t.QueQuanID);
            
            CreateTable(
                "dbo.QueQuans",
                c => new
                    {
                        QueQuanID = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                    })
                .PrimaryKey(t => t.QueQuanID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        UserroleID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Userroles", t => t.UserroleID, cascadeDelete: true)
                .Index(t => t.UserroleID);
            
            CreateTable(
                "dbo.Userroles",
                c => new
                    {
                        UserroleID = c.Guid(nullable: false),
                        Roletitle = c.String(),
                    })
                .PrimaryKey(t => t.UserroleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserroleID", "dbo.Userroles");
            DropForeignKey("dbo.DangKyGiaSus", "QueQuanID", "dbo.QueQuans");
            DropIndex("dbo.Users", new[] { "UserroleID" });
            DropIndex("dbo.DangKyGiaSus", new[] { "QueQuanID" });
            DropTable("dbo.Userroles");
            DropTable("dbo.Users");
            DropTable("dbo.QueQuans");
            DropTable("dbo.DangKyGiaSus");
        }
    }
}

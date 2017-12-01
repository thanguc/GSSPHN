namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuyenMucs",
                c => new
                    {
                        ChuyenMucId = c.Guid(nullable: false),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                        Url = c.String(),
                        SortNumber = c.Int(),
                        MucId = c.Guid(),
                    })
                .PrimaryKey(t => t.ChuyenMucId)
                .ForeignKey("dbo.Mucs", t => t.MucId)
                .Index(t => t.MucId);
            
            CreateTable(
                "dbo.Mucs",
                c => new
                    {
                        MucId = c.Guid(nullable: false),
                        TieuDe = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                        SortNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MucId);
            
            CreateTable(
                "dbo.DangKyGiaSus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(),
                        NgaySinh = c.String(),
                        GioiTinh = c.Int(),
                        QueQuanID = c.Int(),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        Email = c.String(),
                        AnhThe = c.String(),
                        TrinhDoID = c.Int(),
                        TrinhDoKhac = c.String(),
                        DonVi = c.String(),
                        ChuyenNganh = c.String(),
                        GioiThieu = c.String(),
                        TrangThai = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QueQuans", t => t.QueQuanID)
                .ForeignKey("dbo.TrinhDoes", t => t.TrinhDoID)
                .Index(t => t.QueQuanID)
                .Index(t => t.TrinhDoID);
            
            CreateTable(
                "dbo.QueQuans",
                c => new
                    {
                        QueQuanID = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                    })
                .PrimaryKey(t => t.QueQuanID);
            
            CreateTable(
                "dbo.TrinhDoes",
                c => new
                    {
                        TrinhDoId = c.Int(nullable: false, identity: true),
                        TenTrinhDo = c.String(),
                    })
                .PrimaryKey(t => t.TrinhDoId);
            
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
                        TrinhDoId = c.Int(),
                        TrinhDoKhac = c.String(),
                        DonVi = c.String(),
                        ChuyenNganh = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TrinhDoes", t => t.TrinhDoId)
                .Index(t => t.TrinhDoId);
            
            CreateTable(
                "dbo.SystemLogs",
                c => new
                    {
                        SystemLogId = c.Guid(nullable: false),
                        Content = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SystemLogId);
            
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
            DropForeignKey("dbo.TimGiaSus", "TrinhDoId", "dbo.TrinhDoes");
            DropForeignKey("dbo.DangKyGiaSus", "TrinhDoID", "dbo.TrinhDoes");
            DropForeignKey("dbo.DangKyGiaSus", "QueQuanID", "dbo.QueQuans");
            DropForeignKey("dbo.ChuyenMucs", "MucId", "dbo.Mucs");
            DropIndex("dbo.Users", new[] { "UserroleID" });
            DropIndex("dbo.TimGiaSus", new[] { "TrinhDoId" });
            DropIndex("dbo.DangKyGiaSus", new[] { "TrinhDoID" });
            DropIndex("dbo.DangKyGiaSus", new[] { "QueQuanID" });
            DropIndex("dbo.ChuyenMucs", new[] { "MucId" });
            DropTable("dbo.Userroles");
            DropTable("dbo.Users");
            DropTable("dbo.SystemLogs");
            DropTable("dbo.TimGiaSus");
            DropTable("dbo.TrinhDoes");
            DropTable("dbo.QueQuans");
            DropTable("dbo.DangKyGiaSus");
            DropTable("dbo.Mucs");
            DropTable("dbo.ChuyenMucs");
        }
    }
}

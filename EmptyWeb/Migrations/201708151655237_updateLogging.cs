namespace EmptyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLogging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemLogs",
                c => new
                    {
                        SystemLogId = c.Guid(nullable: false),
                        Content = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SystemLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemLogs");
        }
    }
}

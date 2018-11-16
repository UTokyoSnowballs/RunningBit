namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrunningroute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RunningRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(maxLength: 128),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUserId)
                .Index(t => t.AspNetUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RunningRoutes", "AspNetUserId", "dbo.AspNetUsers");
            DropIndex("dbo.RunningRoutes", new[] { "AspNetUserId" });
            DropTable("dbo.RunningRoutes");
        }
    }
}

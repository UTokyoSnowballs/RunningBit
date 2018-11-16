namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testSleepbetaDbContext : DbMigration
    {
        public override void Up()
        {
           
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiaryDatas", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserQuestions", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TokenManagements", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FitbitDatas", "AspNetUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserQuestions", new[] { "AspNetUserId" });
            DropIndex("dbo.TokenManagements", new[] { "AspNetUserId" });
            DropIndex("dbo.FitbitDatas", new[] { "AspNetUserId" });
            DropIndex("dbo.DiaryDatas", new[] { "AspNetUserId" });
            DropTable("dbo.UserQuestions");
            DropTable("dbo.TokenManagements");
            DropTable("dbo.FitbitDatas");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DiaryDatas");
        }
    }
}

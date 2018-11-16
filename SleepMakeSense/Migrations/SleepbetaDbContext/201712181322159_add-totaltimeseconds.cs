namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtotaltimeseconds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RunningRoutes", "TotalTimeSeconds", c => c.Single(nullable: false));
            AddColumn("dbo.RunningRoutes", "TotalDistanceMeters", c => c.String());
            DropColumn("dbo.RunningRoutes", "Latitude");
            DropColumn("dbo.RunningRoutes", "Longitude");
            DropColumn("dbo.RunningRoutes", "Distance");
            DropColumn("dbo.RunningRoutes", "HeartRateBpm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RunningRoutes", "HeartRateBpm", c => c.Int(nullable: false));
            AddColumn("dbo.RunningRoutes", "Distance", c => c.Single(nullable: false));
            AddColumn("dbo.RunningRoutes", "Longitude", c => c.Single(nullable: false));
            AddColumn("dbo.RunningRoutes", "Latitude", c => c.Single(nullable: false));
            DropColumn("dbo.RunningRoutes", "TotalDistanceMeters");
            DropColumn("dbo.RunningRoutes", "TotalTimeSeconds");
        }
    }
}

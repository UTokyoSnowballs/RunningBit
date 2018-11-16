namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RunningRoutes", "TimeStamp", c => c.String());
            AddColumn("dbo.RunningRoutes", "Latitude", c => c.String());
            AddColumn("dbo.RunningRoutes", "Longitude", c => c.String());
            AddColumn("dbo.RunningRoutes", "Distance", c => c.String());
            AddColumn("dbo.RunningRoutes", "HeartRateBpm", c => c.String());
            
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.RunningRoutes", "HeartRateBpm");
            DropColumn("dbo.RunningRoutes", "Distance");
            DropColumn("dbo.RunningRoutes", "Longitude");
            DropColumn("dbo.RunningRoutes", "Latitude");
            DropColumn("dbo.RunningRoutes", "TimeStamp");
        }
    }
}

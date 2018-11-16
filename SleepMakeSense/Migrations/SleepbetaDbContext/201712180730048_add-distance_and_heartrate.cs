namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddistance_and_heartrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RunningRoutes", "Distance", c => c.Single(nullable: false));
            AddColumn("dbo.RunningRoutes", "HeartRateBpm", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RunningRoutes", "HeartRateBpm");
            DropColumn("dbo.RunningRoutes", "Distance");
        }
    }
}

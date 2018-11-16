namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedspeeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RunningRoutes", "AverageSpeed", c => c.String());
            AddColumn("dbo.RunningRoutes", "InstantaneousSpeed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RunningRoutes", "InstantaneousSpeed");
            DropColumn("dbo.RunningRoutes", "AverageSpeed");
        }
    }
}

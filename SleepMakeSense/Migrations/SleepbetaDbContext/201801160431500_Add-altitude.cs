namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addaltitude : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RunningRoutes", "Altitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RunningRoutes", "Altitude");
        }
    }
}

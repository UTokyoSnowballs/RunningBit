namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedlastname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DiaryDatas", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiaryDatas", "LastName", c => c.String());
        }
    }
}

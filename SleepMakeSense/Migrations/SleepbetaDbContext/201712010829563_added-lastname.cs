namespace SleepMakeSense.Migrations.SleepbetaDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlastname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiaryDatas", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiaryDatas", "LastName");
        }
    }
}

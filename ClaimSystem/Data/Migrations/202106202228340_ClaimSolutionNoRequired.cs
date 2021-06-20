namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClaimSolutionNoRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Claims", "SolutionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Claims", "SolutionDate", c => c.DateTime(nullable: false));
        }
    }
}

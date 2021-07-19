namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeptoTableUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Departments", "Desc", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Departments", "Room");
            DropColumn("dbo.Departments", "ProcessingPlant");
            DropColumn("dbo.Departments", "Workshop");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "Workshop", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Departments", "ProcessingPlant", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Departments", "Room", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Departments", "Desc");
            DropColumn("dbo.Departments", "Name");
        }
    }
}

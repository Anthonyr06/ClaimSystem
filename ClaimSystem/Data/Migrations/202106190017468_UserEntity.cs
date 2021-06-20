namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Customers", "Password");
        }
    }
}

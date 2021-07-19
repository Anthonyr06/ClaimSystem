namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressIdFKFieldDropped : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "AddressId");
            DropColumn("dbo.Employees", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "AddressId", c => c.Int(nullable: false));
        }
    }
}

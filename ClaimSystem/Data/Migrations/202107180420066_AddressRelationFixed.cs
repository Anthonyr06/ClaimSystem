namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressRelationFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "EmployeeId", "dbo.Addresses");
            DropForeignKey("dbo.Claims", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Claims", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropIndex("dbo.Employees", new[] { "EmployeeId" });
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Employees");
            AddColumn("dbo.Customers", "AddressId", c => c.Int());
            AddColumn("dbo.Employees", "AddressId", c => c.Int());
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employees", "EmployeeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Employees", "EmployeeId");
            CreateIndex("dbo.Customers", "AddressId");
            CreateIndex("dbo.Employees", "AddressId");
            AddForeignKey("dbo.Customers", "AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Employees", "AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Claims", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Claims", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Claims", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Claims", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Employees", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "AddressId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Employees", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "AddressId");
            DropColumn("dbo.Customers", "AddressId");
            AddPrimaryKey("dbo.Employees", "EmployeeId");
            AddPrimaryKey("dbo.Customers", "CustomerId");
            CreateIndex("dbo.Employees", "EmployeeId");
            CreateIndex("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Claims", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.Claims", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "EmployeeId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.Addresses", "AddressId");
        }
    }
}

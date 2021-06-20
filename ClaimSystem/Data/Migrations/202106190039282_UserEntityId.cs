namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEntityId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "CustomerId", newName: "Id");
            RenameColumn(table: "dbo.Employees", name: "EmployeeId", newName: "Id");
            RenameIndex(table: "dbo.Customers", name: "IX_CustomerId", newName: "IX_Id");
            RenameIndex(table: "dbo.Employees", name: "IX_EmployeeId", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employees", name: "IX_Id", newName: "IX_EmployeeId");
            RenameIndex(table: "dbo.Customers", name: "IX_Id", newName: "IX_CustomerId");
            RenameColumn(table: "dbo.Employees", name: "Id", newName: "EmployeeId");
            RenameColumn(table: "dbo.Customers", name: "Id", newName: "CustomerId");
        }
    }
}

﻿namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressType = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 200),
                        Neighborhood = c.String(nullable: false, maxLength: 200),
                        City = c.String(nullable: false, maxLength: 200),
                        Country = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Cedula = c.String(nullable: false, maxLength: 11),
                        Name = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 254),
                        Password = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AddressId = c.Int(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.Cedula, unique: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        Dish = c.String(nullable: false, maxLength: 100),
                        Desc = c.String(nullable: false, maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        SolutionDate = c.DateTime(),
                        Solution = c.String(maxLength: 200),
                        ClaimTypeId = c.Int(nullable: false),
                        ClaimStateId = c.Int(nullable: false),
                        ClaimPriorityId = c.Int(nullable: false),
                        EmployeeId = c.Int(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.ClaimPriorities", t => t.ClaimPriorityId, cascadeDelete: true)
                .ForeignKey("dbo.ClaimStates", t => t.ClaimStateId, cascadeDelete: true)
                .ForeignKey("dbo.ClaimTypes", t => t.ClaimTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.ClaimTypeId)
                .Index(t => t.ClaimStateId)
                .Index(t => t.ClaimPriorityId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ClaimPriorities",
                c => new
                    {
                        ClaimPriorityId = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ClaimPriorityId);
            
            CreateTable(
                "dbo.ClaimStates",
                c => new
                    {
                        ClaimStateId = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ClaimStateId);
            
            CreateTable(
                "dbo.ClaimTypes",
                c => new
                    {
                        ClaimTypeId = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ClaimTypeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Cedula = c.String(nullable: false, maxLength: 11),
                        Name = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 254),
                        Password = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false),
                        PositionId = c.Int(nullable: false),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.Cedula, unique: true)
                .Index(t => t.PositionId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Desc = c.String(nullable: false, maxLength: 500),
                        Salary = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Desc = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Positions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Claims", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Claims", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Claims", "ClaimTypeId", "dbo.ClaimTypes");
            DropForeignKey("dbo.Claims", "ClaimStateId", "dbo.ClaimStates");
            DropForeignKey("dbo.Claims", "ClaimPriorityId", "dbo.ClaimPriorities");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Positions", new[] { "DepartmentId" });
            DropIndex("dbo.Positions", new[] { "Name" });
            DropIndex("dbo.Employees", new[] { "AddressId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "Cedula" });
            DropIndex("dbo.Claims", new[] { "CustomerId" });
            DropIndex("dbo.Claims", new[] { "EmployeeId" });
            DropIndex("dbo.Claims", new[] { "ClaimPriorityId" });
            DropIndex("dbo.Claims", new[] { "ClaimStateId" });
            DropIndex("dbo.Claims", new[] { "ClaimTypeId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.Customers", new[] { "Cedula" });
            DropTable("dbo.Departments");
            DropTable("dbo.Positions");
            DropTable("dbo.Employees");
            DropTable("dbo.ClaimTypes");
            DropTable("dbo.ClaimStates");
            DropTable("dbo.ClaimPriorities");
            DropTable("dbo.Claims");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}

namespace ClaimSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ClaimSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ClaimSystem.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
        }

        protected override void Seed(AppDbContext context)
        {
            var Addresses = new List<Address>
            {
                new Address {AddressType = AddressType.Casa, Country = "Republica Dominicana", City = "Distrito Nacional", Neighborhood = "Mata Hambre", Street = "Muertico", Number = 80},
                new Address {AddressType = AddressType.Casa, Country = "Republica Dominicana", City = "Distrito Nacional", Neighborhood = "Piantini", Street = "Los prados", Number = 15}
            };

            Addresses.ForEach(a => context.Addresses.AddOrUpdate(a));


            var Departments = new List<Department>
            {
                new Department {Name = "Cocina", Desc = "Lugar donde se crean los alimentos"},
                new Department {Name = "Bar", Desc = "Lugar donde se crean las bebidas"},
            };

            Departments.ForEach(d => context.Departments.AddOrUpdate(d));


            var Positions = new List<Position>
            {
                new Position {Name = "Cocinero", Desc = "Persona que cocina comida", Salary = 45000, DepartmentId = 1},
                new Position {Name = "Batender", Desc = "Persona que sirve bebidas", Salary = 30000, DepartmentId = 2},
            };

            Positions.ForEach(p => context.Positions.AddOrUpdate(p));


            var ClaimTypes = new List<ClaimType>
            {
                new ClaimType {Desc = "Comida mal estado"},
                new ClaimType {Desc = "Bebida mal estado"},
                new ClaimType {Desc = "Comida fría"},
                new ClaimType {Desc = "Bebida fría estaba caliente"},
                new ClaimType {Desc = "Bebida caliente estaba fría"},
                new ClaimType {Desc = "Utensilios sucios"},
                new ClaimType {Desc = "Mal trato del personal"},
                new ClaimType {Desc = "Otros..."}
            };

            ClaimTypes.ForEach(ct => context.ClaimTypes.AddOrUpdate(ct));


            var ClaimStates = new List<ClaimState>
            {
                new ClaimState {Desc = "Abierta"},
                new ClaimState {Desc = "En Proceso"},
                new ClaimState {Desc = "Cerrada"},
                new ClaimState {Desc = "Re-Abierta"}
            };

            ClaimStates.ForEach(cs => context.ClaimStates.AddOrUpdate(cs));


            var ClaimPriorities = new List<ClaimPriority>
            {
                new ClaimPriority {Desc = "Normal"},
                new ClaimPriority {Desc = "Media Alta"},
                new ClaimPriority {Desc = "Alta"}
            };

            ClaimPriorities.ForEach(cp => context.ClaimPriorities.AddOrUpdate(cp));

            context.SaveChanges();


            var Employee = new Employee
            {
                Cedula = "00000000000",
                Name = "Jose",
                LastName = "Moris",
                Email = "jose@gmail.com",
                Password = "827ccb0eea8a706c4c34a16891f84e7b", //es: 12345
                PhoneNumber = "8294055466",
                AdmissionDate = DateTime.Now,
                PositionId = 1,
                AddressId = 1
            };

            context.Employees.AddOrUpdate(Employee);

            context.SaveChanges();
        }
    }
}

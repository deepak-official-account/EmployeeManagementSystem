namespace DemoProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        Salary = c.Double(nullable: false),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Employees");
        }
    }
}

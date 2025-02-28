namespace DemoProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAndEmployeeMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Salary = c.Double(nullable: false),
                        Department = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}

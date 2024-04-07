namespace CPMS.API.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ProjectManagerId = c.Int(nullable: false),
                        ProjectManager_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Employees", t => t.ProjectManager_EmployeeId)
                .Index(t => t.ProjectManager_EmployeeId);
            
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.EmployeeId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectManager_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProjectEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProjectEmployees", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ProjectEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.ProjectEmployees", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ProjectManager_EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropTable("dbo.ProjectEmployees");
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}

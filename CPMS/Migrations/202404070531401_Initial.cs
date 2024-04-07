
namespace CPMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
                .Index(t => t.DepartmentId)
                .Index(t => t.Project_ProjectId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectManager_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Projects", new[] { "ProjectManager_EmployeeId" });
            DropIndex("dbo.Employees", new[] { "Project_ProjectId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}

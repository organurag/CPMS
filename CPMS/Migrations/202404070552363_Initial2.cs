namespace CPMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.Employees", new[] { "Project_ProjectId" });
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
            
            DropColumn("dbo.Employees", "Project_ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Project_ProjectId", c => c.Int());
            DropForeignKey("dbo.ProjectEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProjectEmployees", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.ProjectEmployees", new[] { "ProjectId" });
            DropTable("dbo.ProjectEmployees");
            CreateIndex("dbo.Employees", "Project_ProjectId");
            AddForeignKey("dbo.Employees", "Project_ProjectId", "dbo.Projects", "ProjectId");
        }
    }
}

namespace Stomatology.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferenceToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "ApplicationUserId");
            AddForeignKey("dbo.Appointments", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "ApplicationUserId" });
            DropColumn("dbo.Appointments", "ApplicationUserId");
        }
    }
}

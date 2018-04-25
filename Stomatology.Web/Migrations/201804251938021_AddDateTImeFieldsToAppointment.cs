namespace Stomatology.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTImeFieldsToAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Appointments", "DeleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "DeleteDate");
            DropColumn("dbo.Appointments", "UpdateDate");
            DropColumn("dbo.Appointments", "CreateDate");
        }
    }
}

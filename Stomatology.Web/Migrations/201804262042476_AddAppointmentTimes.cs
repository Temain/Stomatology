namespace Stomatology.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppointmentTimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "EndTime");
            DropColumn("dbo.Appointments", "StartTime");
        }
    }
}

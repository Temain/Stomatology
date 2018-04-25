namespace Stomatology.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Age = c.Int(nullable: false),
                        Phone = c.String(),
                        SourceType = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Speciality = c.String(),
                        Phone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.ServiceAppointments",
                c => new
                    {
                        Service_ServiceId = c.Int(nullable: false),
                        Appointment_AppointmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ServiceId, t.Appointment_AppointmentId })
                .ForeignKey("dbo.Services", t => t.Service_ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointment_AppointmentId, cascadeDelete: true)
                .Index(t => t.Service_ServiceId)
                .Index(t => t.Appointment_AppointmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.ServiceAppointments", "Appointment_AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.ServiceAppointments", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.Appointments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ServiceAppointments", new[] { "Appointment_AppointmentId" });
            DropIndex("dbo.ServiceAppointments", new[] { "Service_ServiceId" });
            DropIndex("dbo.Appointments", new[] { "StaffId" });
            DropIndex("dbo.Appointments", new[] { "CustomerId" });
            DropTable("dbo.ServiceAppointments");
            DropTable("dbo.Staffs");
            DropTable("dbo.Services");
            DropTable("dbo.Customers");
            DropTable("dbo.Appointments");
        }
    }
}

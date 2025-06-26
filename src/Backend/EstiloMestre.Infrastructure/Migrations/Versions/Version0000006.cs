using System.Data;
using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(6, "Add tables for registering schedule appointments")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        CreateTable("EmployeeWorkingHours")
            .WithColumn("EmployeeId").AsInt64().NotNullable()
            .ForeignKey("Employees", "Id").OnDelete(Rule.Cascade)
            .WithColumn("DayOfWeek").AsInt32().NotNullable()
            .WithColumn("StartTime").AsCustom("TIME").NotNullable()
            .WithColumn("EndTime").AsCustom("TIME").NotNullable()
            .WithColumn("IsDayOff").AsBoolean().NotNullable().WithDefaultValue(false);

        CreateTable("EmployeeWorkingHourOverrides")
            .WithColumn("EmployeeId").AsInt64().NotNullable()
            .ForeignKey("Employees", "Id").OnDelete(Rule.Cascade)
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("StartTime").AsCustom("TIME").Nullable()
            .WithColumn("EndTime").AsCustom("TIME").Nullable()
            .WithColumn("IsDayOff").AsBoolean().NotNullable().WithDefaultValue(false);

        CreateTable("Bookings")
            .WithColumn("CustomerId").AsInt64().NotNullable()
            .ForeignKey("Users", "Id").OnDelete(Rule.Cascade)
            .WithColumn("EmployeeId").AsInt64().NotNullable()
            .ForeignKey("Employees", "Id").OnDelete(Rule.Cascade)
            .WithColumn("BarbershopId").AsInt64().NotNullable()
            .ForeignKey("Barbershops", "Id").OnDelete(Rule.Cascade)
            .WithColumn("BarbershopServiceId").AsInt64().NotNullable()
            .ForeignKey("BarbershopServices", "Id").OnDelete(Rule.Cascade)
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("StartTime").AsCustom("TIME").NotNullable()
            .WithColumn("EndTime").AsCustom("TIME").NotNullable();

        Create.Index("IX_EmployeeWorkingHours_EmployeeId_DayOfWeek")
            .OnTable("EmployeeWorkingHours")
            .OnColumn("EmployeeId").Ascending()
            .OnColumn("DayOfWeek").Ascending();

        Create.Index("IX_EmployeeWorkingHourOverrides_EmployeeId_Date")
            .OnTable("EmployeeWorkingHourOverrides")
            .OnColumn("EmployeeId").Ascending()
            .OnColumn("Date").Ascending();
    }
}
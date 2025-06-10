using System.Data;
using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(4, "Add base properties to ServicesEmployee table")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        if (Schema.Table("ServicesEmployee").Exists()) Delete.Table("ServicesEmployee");

        CreateTable("ServicesEmployee")
           .WithColumn("EmployeeId")
           .AsInt64()
           .NotNullable()
           .ForeignKey("Employees", "Id")
           .OnDelete(Rule.Cascade)
           .WithColumn("BarbershopServiceId")
           .AsInt64()
           .NotNullable()
           .ForeignKey("BarbershopServices", "Id")
           .OnDelete(Rule.Cascade);

        Create.Index("IX_ServiceEmployee_EmployeeId_BarbershopServicesId_Unique")
           .OnTable("ServicesEmployee")
           .OnColumn("EmployeeId")
           .Ascending()
           .OnColumn("BarbershopServiceId")
           .Ascending()
           .WithOptions()
           .Unique();
    }
}

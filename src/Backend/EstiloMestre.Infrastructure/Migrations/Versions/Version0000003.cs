using System.Data;
using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(3, "create table to associate employees with barbershop services")]
public class Version0000003 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("ServicesEmployee")
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

        Create.PrimaryKey("PK_ServicesEmployee")
           .OnTable("ServicesEmployee")
           .Columns("EmployeeId", "BarbershopServiceId");
    }
}

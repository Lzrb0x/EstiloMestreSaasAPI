using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(2, "create table associative entity BarbershopService")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("BarbershopServices")
            .WithColumn("Price")
            .AsDecimal(18, 2)
            .NotNullable()
            .WithColumn("Duration")
            .AsTime()
            .NotNullable()
            .WithColumn("DescriptionOverride")
            .AsString(255)
            .Nullable()
            .WithColumn("BarbershopId")
            .AsInt64()
            .NotNullable()
            .ForeignKey("FK_BarbershopService_BarbershopId", "Barbershops", "Id")
            .WithColumn("ServiceId")
            .AsInt64()
            .NotNullable()
            .ForeignKey("FK_BarbershopService_ServiceId", "Services", "Id");

        Create.Index("IX_BarbershopService_BarbershopId_ServiceId_Unique")
            .OnTable("BarbershopServices")
            .OnColumn("BarbershopId")
            .Ascending()
            .OnColumn("ServiceId")
            .Ascending()
            .WithOptions()
            .Unique();
    }
}

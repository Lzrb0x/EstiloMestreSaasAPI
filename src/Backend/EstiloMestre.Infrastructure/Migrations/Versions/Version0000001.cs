using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(1, "create table to save the users information")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("Phone").AsString(255).NotNullable()
            .WithColumn("IsOwner").AsBoolean().NotNullable()
            .WithColumn("UserIdentifier").AsGuid().NotNullable();

        CreateTable("Owners")
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Owner_UserId", "Users", "Id")
            .OnDelete(System.Data.Rule.Cascade);

        CreateTable("Barbershops")
            .WithColumn("BarbershopName").AsString(255).NotNullable()
            .WithColumn("Address").AsString(255).NotNullable()
            .WithColumn("Phone").AsString(255).Nullable()
            .WithColumn("OwnerId").AsInt64().NotNullable().ForeignKey("FK_Barbershop_OwnerId", "Owners", "Id");


        Create.UniqueConstraint("UQ_Owner_UserId").OnTable("Owners").Column("UserId");
    }
}
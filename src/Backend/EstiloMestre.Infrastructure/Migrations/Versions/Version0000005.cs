using FluentMigrator;

namespace EstiloMestre.Infrastructure.Migrations.Versions;

[Migration(5, "Add isComplete property to users table")]
public class Version0000005 : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AlterColumn("Password").AsString(2000).Nullable()
            .AlterColumn("Email").AsString(2000).Nullable()
            .AlterColumn("Phone").AsString(2000).NotNullable()
            .AddColumn("IsComplete").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
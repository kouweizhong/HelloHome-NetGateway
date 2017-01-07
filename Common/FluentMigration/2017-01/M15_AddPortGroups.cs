using System.Data.Entity.Migrations.Model;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,1,7, 22, 26)]
    public class M15_AddPortGroups : Migration
    {
        public override void Up()
        {
            Create.Table("PortGroup")
                .WithColumn("portGroupId").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsAnsiString(255).NotNullable()
                .WithColumn("type").AsAnsiString(1).NotNullable();

            Create.Table("PortGroupPort")
                .WithColumn("portGroupPortId").AsInt32().PrimaryKey().Identity()
                .WithColumn("portGroupId").AsInt32().NotNullable()
                .WithColumn("portId").AsInt32().NotNullable();
            Create.ForeignKey("FK_PortGroupPort_PortGroup")
                .FromTable("PortGroupPort").ForeignColumn("portGroupId")
                .ToTable("PortGroup").PrimaryColumn("portGroupId");
            Create.ForeignKey("FK_PortGroupPort_Port")
                .FromTable("PortGroupPort").ForeignColumn("portId")
                .ToTable("Port").PrimaryColumn("portId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PortGroupPort_PortGroup").OnTable("PortGroupPort");
            Delete.ForeignKey("FK_PortGroupPort_Port").OnTable("PortGroupPort");
            Delete.Table("PortGroupPort");

            Delete.Table("PortGroup");
        }
    }
}
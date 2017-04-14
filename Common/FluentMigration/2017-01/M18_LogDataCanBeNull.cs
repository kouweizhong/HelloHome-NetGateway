using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,2,2,22,15)]
    public class M18_LogDataCanBeNull : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Column("data").OnTable("Log").AsAnsiString(255).Nullable();
        }
    }
}
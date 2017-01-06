using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2016,12,22,21,50)]
    public class M13_MakesRfFieldsTinyInts :ForwardOnlyMigration
    {
        public override void Up()
        {
            Rename.Column("RfAddress").OnTable("Node").To("rfAddress");
            Alter.Column("rfAddress").OnTable("Node").AsByte().NotNullable();
            Alter.Column("rfNetwork").OnTable("Node").AsByte().NotNullable();
        }
    }
}
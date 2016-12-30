using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2016,12,22,21,39)]
    public class M12_RenameNodeNetworkId : ForwardOnlyMigration
    {
        public override void Up()
        {
            Rename.Column("networkId").OnTable("Node").To("rfNetwork");
        }
    }
}
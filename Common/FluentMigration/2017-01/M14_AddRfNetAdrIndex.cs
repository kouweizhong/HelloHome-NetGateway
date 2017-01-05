using FluentMigrator;
using FluentMigrator.Expressions;

namespace HelloHome.Common.FluentMigration
{
    [HhMigration(2017,1,5, 21, 57)]
    public class M14_AddRfNetAdrIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.UniqueConstraint("UC_Node_RfNetworkAddress")
                .OnTable("Node")
                .Columns("rfAddress", "rfNetwork");
        }
    }
}
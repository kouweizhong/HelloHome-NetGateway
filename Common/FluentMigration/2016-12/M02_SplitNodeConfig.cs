using System;
using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,8,21,24)]
	public class M02_SplitNodeConfig : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Create.Table ("NodeConfig")
				  .WithColumn ("nodeId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("name").AsAnsiString (50).Nullable ()
				  .WithColumn ("emonCmsNodeId").AsInt16 ().Nullable ()
				  .WithColumn ("version").AsAnsiString (20).Nullable ();
			Create.ForeignKey ("nodeConfig_Node").FromTable ("NodeConfig").ForeignColumn ("nodeId").ToTable ("Node").PrimaryColumn ("nodeId");
			Execute.Sql ("INSERT NodeConfig (nodeId, name, emonCmsNodeId, version) SELECT nodeId, name, emonCmsNodeId, version FROM Node;");
			Delete.Column ("emonCmsNodeId").FromTable ("Node");
			Delete.Column ("name").FromTable ("Node");
			Delete.Column ("version").FromTable ("Node");
}
	}
}

using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration (2016, 7, 23, 22, 37)]
	public class CreateNodeConfigTable: Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_NodeConfig_Node").OnTable("NodeConfig");
			Delete.Table ("NodeConfig");
		}

		public override void Up ()
		{
			Create.Table ("NodeConfig")
				  .WithColumn ("NodeId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("Name").AsString (50).Nullable ()
				  .WithColumn ("EmonCmsNodeId").AsInt16 ().Nullable ();
			Create.ForeignKey ("FK_NodeConfig_Node")
				  .FromTable ("NodeConfig").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}


using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,7,23,22,50)]
	public class CreateNodeFactTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_NodeFact_Node").OnTable ("NodeFact");
			Delete.Table ("NodeFact");
		}

		public override void Up ()
		{
			Create.Table ("NodeFact")
				  .WithColumn ("NodeId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("LastStartup").AsDateTime ().NotNullable ()
				  .WithColumn ("MaxUpTime").AsFloat ().Nullable ()
				  .WithColumn ("Version").AsString (10).NotNullable ();
			Create.ForeignKey ("FK_NodeFact_Node")
				  .FromTable ("NodeFact").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}


using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,7,23,22,50)]
	public class CreateNodeFactTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_NodeFact_Node").OnTable ("NodeFacts");
			Delete.Table ("NodeFacts");
		}

		public override void Up ()
		{
			Create.Table ("NodeFacts")
				  .WithColumn ("NodeId").AsInt32 ().PrimaryKey ()
				  .WithColumn ("LastStartup").AsDateTime ().NotNullable ()
				  .WithColumn ("MaxUpTime").AsFloat ().NotNullable ()
				  .WithColumn ("Version").AsString (10).NotNullable ();
			Create.ForeignKey ("FK_NodeFact_Node")
				  .FromTable ("NodeFacts").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}


using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration (2016, 8, 1, 23, 35)]
	public class CreateNodeLatestValuesTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_NodeLatestValues_Node").OnTable ("NodeLatestValues");
			Delete.Table ("NodeLatestValues");
		}

		public override void Up ()
		{
			Create.Table ("NodeLatestValues")
				.WithColumn ("nodeId").AsInt32 ().PrimaryKey ()
				.WithColumn ("Temperature").AsFloat ().Nullable ()
				.WithColumn ("Humidity").AsFloat ().Nullable ()
				.WithColumn ("Pressure").AsFloat ().Nullable ()
				.WithColumn ("VIn").AsFloat ().Nullable ()
				.WithColumn ("SendErrorCount").AsInt32 ().Nullable ();
			Create.ForeignKey ("FK_NodeLatestValues_Node")
	  			.FromTable ("NodeLatestValues").ForeignColumn ("NodeId")
				.ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}


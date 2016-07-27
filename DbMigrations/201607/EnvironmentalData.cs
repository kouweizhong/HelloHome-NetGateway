using System;
using FluentMigrator;

namespace DbMigrations
{
	[HhMigration(2016,7,24,20,7)]
	public class CreateEnvironmentalDataTable : Migration
	{
		public override void Down ()
		{
			Delete.ForeignKey ("FK_EnvironmentalData_Node").OnTable ("EnvironmentalData");
			Delete.Table ("EnvironmentalData");
		}

		public override void Up ()
		{
			Create.Table ("EnvironmentalData")
				  .WithColumn ("Id").AsInt32 ().PrimaryKey ().Identity ()
				  .WithColumn ("NodeId").AsInt32 ()
				  .WithColumn ("Date").AsDateTime ().NotNullable ()
				  .WithColumn ("Temperature").AsFloat ().Nullable ()
				  .WithColumn ("Humidity").AsFloat ().Nullable ()
				  .WithColumn ("AtmosphericPressure").AsFloat ().Nullable ();
			Create.ForeignKey ("FK_EnvironmentalData_Node")
				  .FromTable ("EnvironmentalData").ForeignColumn ("NodeId")
				  .ToTable ("Node").PrimaryColumn ("Id");
		}
	}
}


using System;
namespace HelloHome.Common.FluentMigration
{
	[AttributeUsage(AttributeTargets.Class)]
	public class HhMigrationAttribute : FluentMigrator.MigrationAttribute
	{
		public HhMigrationAttribute (int year, int month, int day, int hour, int minute)
			:base(CalculateValue(year,month,day,hour,minute))
		{
		}

		static long CalculateValue (int year, int month, int day, int hour, int minute)
		{
			return year * 100000000L + month * 1000000L + day * 10000L + hour * 100L + minute;
		}

	}
}


using System;

namespace Common.Extentions
{
	public static class DateTimeExtentions
	{
		static readonly DateTime epochRef = new DateTime(1970,1,1);

		public static long ToEpoch(this DateTime dt){
			return (long)((epochRef-dt).TotalSeconds);
		} 
	}
}


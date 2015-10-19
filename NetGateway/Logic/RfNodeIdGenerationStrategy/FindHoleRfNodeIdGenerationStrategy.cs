using System;
using System.Linq;

namespace NetHhGateway.Logic.RfNodeIdGenerationStrategy
{
	public class FindHoleRfNodeIdGenerationStrategy : IRfNodeIdGenerationStrategy
	{
		Random _rnd;

		public FindHoleRfNodeIdGenerationStrategy ()
		{
			MaxSupportedConcurrentRequest = 5;
			_rnd = new Random ();
		}

		public int MaxSupportedConcurrentRequest { get; set; }

		#region IRfNodeIdGeneratorStrategy implementation

		public byte FindRfAddress (System.Collections.Generic.IList<byte> exisitingRfAddresses)
		{
			var maxExisting = exisitingRfAddresses.Max ();
			var holes = Enumerable.Range(1, maxExisting).Select(i => (byte)i).Where (i => !exisitingRfAddresses.Contains(i)).ToList ();
			if (holes.Count >= MaxSupportedConcurrentRequest) {
				return (byte)holes [_rnd.Next(holes.Count - 1)];
			}
			return (byte)(_rnd.Next(maxExisting+1, Math.Min(maxExisting+1+MaxSupportedConcurrentRequest, byte.MaxValue)));
		}

		#endregion	

	}
}


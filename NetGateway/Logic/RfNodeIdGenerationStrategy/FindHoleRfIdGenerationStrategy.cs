using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy
{
	public class FindHoleRfIdGenerationStrategy : IRfIdGenerationStrategy
	{
	    private readonly IListRfIdsQuery _listRfIdQuery;
	    readonly Random _rnd;

		public FindHoleRfIdGenerationStrategy (IListRfIdsQuery listRfIdQuery)
		{
		    _listRfIdQuery = listRfIdQuery;
		    MaxSupportedConcurrentRequest = 5;
			_rnd = new Random ();
		}

		public int MaxSupportedConcurrentRequest { get; set; }

		#region IRfNodeIdGeneratorStrategy implementation

		public byte FindRfAddress (System.Collections.Generic.IList<byte> exisitingRfAddresses)
		{
			if (!exisitingRfAddresses.Any ())
				return (byte)1;
			var maxExisting = exisitingRfAddresses.Max ();
			var holes = Enumerable.Range(1, maxExisting).Select(i => (byte)i).Where (i => !exisitingRfAddresses.Contains(i)).ToList ();
			if (holes.Count >= MaxSupportedConcurrentRequest) {
				return (byte)holes [_rnd.Next(holes.Count - 1)];
			}
			return (byte)(_rnd.Next(maxExisting+1, Math.Min(maxExisting+1+MaxSupportedConcurrentRequest, 250)));
		}

	    public async Task<byte> FindAvailableRfAddressAsync(byte network, CancellationToken cToken, byte suggestion = 0)
	    {
	        var exisitingIds = await _listRfIdQuery.ExecuteAsync(network, cToken);
	        if (!exisitingIds.Any ())
	            return 1;
	        if (suggestion != 0 && !exisitingIds.Contains(suggestion))
	            return suggestion;

	        var maxExisting = exisitingIds.Max ();

	        var holes = Enumerable.Range(1, maxExisting).Select(i => (byte)i).Where (i => !exisitingIds.Contains(i)).ToList ();
	        if (holes.Count >= MaxSupportedConcurrentRequest) {
	            return holes [_rnd.Next(holes.Count - 1)];
	        }
	        return (byte)(_rnd.Next(maxExisting+1, Math.Min(maxExisting+1+MaxSupportedConcurrentRequest, 250)));
	    }

	    #endregion

	}
}


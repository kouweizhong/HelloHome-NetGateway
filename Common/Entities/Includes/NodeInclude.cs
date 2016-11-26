using System;
using System.Linq;
using System.Data.Entity;

namespace HelloHome.Common.Entities.Includes
{
	[Flags]
	public enum NodeInclude
	{
		None = 0,
		All = ~None,
		Facts = 1,
		Config = 2,
		Ports = 4,
		LatestValues = 8,
	}

	public static class NodeExtentions
	{
		public static IQueryable<Node> Include (this IQueryable<Node> query, NodeInclude include)
		{
			if (include.HasFlag (NodeInclude.Facts))
				query = query.Include (_ => _.NodeFacts);
			if (include.HasFlag (NodeInclude.Config))
				query = query.Include (_ => _.Configuration);
			if (include.HasFlag (NodeInclude.Ports))
				query = query.Include (_ => _.Ports);
			if (include.HasFlag (NodeInclude.LatestValues))
				query = query.Include (_ => _.LatestValues);
			return query;
		}
	}
}

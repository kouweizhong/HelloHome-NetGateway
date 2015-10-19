using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Collections.Generic;

namespace NetHhGateway.Processors
{

	public class ProcessorComparer : IComparer<IMessageProcessor>
	{

		private readonly Dictionary<Type, int> _priorityProcessors;

		public ProcessorComparer ()
		{
			_priorityProcessors = new Dictionary<Type, int> {
				{ typeof(UpdateUpTimeProcessor), 1 }				
			};
		}

		#region IComparer implementation

		public int Compare (IMessageProcessor x, IMessageProcessor y)
		{
			var xType = x.GetType ();
			var yType = y.GetType ();
			if (!_priorityProcessors.ContainsKey (xType))
				_priorityProcessors.Add (xType, 999);
			if (!_priorityProcessors.ContainsKey (yType))
				_priorityProcessors.Add (yType, 999);
			return _priorityProcessors[xType].CompareTo(_priorityProcessors[yType]);
		}

		#endregion
	}
}

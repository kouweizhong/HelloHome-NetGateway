using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Pipeline.Configuration
{
	public class PipelineModuleCollection : ConfigurationElementCollection, IEnumerable<PipelineModuleElement>
	{

		#region implemented abstract members of ConfigurationElementCollection

		protected override ConfigurationElement CreateNewElement ()
		{
			return new PipelineModuleElement ();
		}

		protected override object GetElementKey (ConfigurationElement element)
		{
			return ((PipelineModuleElement)element).Name;
		}

		#endregion

		public new IEnumerator<PipelineModuleElement> GetEnumerator ()
		{
			for (var i = 0; i < this.Count; i++) {
				yield return this.BaseGet (i) as PipelineModuleElement;
			}
		}
	}
}


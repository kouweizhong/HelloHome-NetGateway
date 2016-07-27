using System;
using System.Collections.Generic;
using log4net;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Pipeline
{
	public class GatewayPipeline : IGatewayPipeline, IDisposable
	{
		readonly static ILog log = LogManager.GetLogger(typeof(GatewayPipeline).Name);
		readonly IPipelineModuleFactory _pipelineModuleFactory;
		readonly HelloHomeDbContext _dbContext;
		readonly LinkedList<IPipelineModule> _modules = new LinkedList<IPipelineModule> ();


		public GatewayPipeline (IPipelineModuleFactory pipelineModuleFactory, HelloHomeDbContext dbContext)
		{
			_dbContext = dbContext;
			log.Debug ($"Injected with DbContext with hash {dbContext.ContextId}");
			_pipelineModuleFactory = pipelineModuleFactory;
			_modules.AddLast(pipelineModuleFactory.Create<LoadContextModule>());			
			_modules.AddLast(pipelineModuleFactory.Create<UpdateStatisticsModule>());			
			_modules.AddLast(pipelineModuleFactory.Create<ProcessMessageModule>());			
		}

		public void Process(ProcessingContext context)
		{
			var le = _modules.First;
			while (le != null) {
				var next = le.Value.Process (context, le.Next?.Value);

				//Ending flow
				if (next == null) {
					log.Debug ($"Module {le.Value.GetType ().Name} cancelled the pipeline by returning null");
					le = null;
					continue;
				}
				if (le.Next == null) {
					le = null;
					continue;
				}

				//Expected flow
				if (le.Next.Value == next) {
					le = le.Next;
					continue;
				}

				//Insert next into pipeline and continue
				log.Debug ($"Module {le.GetType ().Name} asked for insertion of module {next.GetType ().Name} into the pipeline");
				_modules.AddAfter (le, next);
				le = le.Next;
			}
			_dbContext.SaveChanges ();
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			var m = _modules.First;
			do 
				_pipelineModuleFactory.Release(m.Value); 
			while ((m = m.Next) != null);
		}

		#endregion
	}
}


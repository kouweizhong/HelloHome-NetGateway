using System.Web.Http;
using System.Collections.Generic;
using HelloHome.Common.Entities;
using System.Linq;

namespace WebApi.Controllers
{
	public class NodeController : ApiController
	{
		public IEnumerable<Node> Get(){
			using (var dbContext = new HelloHomeDbContext ()) {
				return dbContext.Nodes.ToList ();
			}
		}

	}
}


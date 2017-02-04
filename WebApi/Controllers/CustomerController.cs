using System.Web.Http;
using System.Collections.Generic;

namespace WebApi.Controllers
{
	public class CustomerController : ApiController
	{
		public IEnumerable<int> Get ()
		{
			return new List<int> { 1, 2, 3 };
		}
	}
}


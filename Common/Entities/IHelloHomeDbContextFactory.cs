using System;

namespace HelloHome.Common.Entities
{
	public interface IHelloHomeDbContextFactory
	{
		HelloHomeDbContext Create();
		void Release(HelloHomeDbContext context);
	}
}


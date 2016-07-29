﻿using System;
using HelloHome.Common.Entities;
using NUnit.Framework;

namespace IntegrationTests.Common.Entities.Configuration
{
	public abstract class EntityConfigurationTest<T> where T : class
	{
		HelloHomeDbContext ctx;

		public EntityConfigurationTest ()
		{
			ctx = new HelloHomeDbContext ();
		}

		[Test]
		public void CanCreateEntity ()
		{
			var e = CreateEntity ();
			ctx.Set<T> ().Add (e);
			ctx.SaveChanges ();
		}

		protected virtual T CreateEntity ()
		{
			return FizzWare.NBuilder.Builder<T>.CreateNew().Build();
		}
	}
}


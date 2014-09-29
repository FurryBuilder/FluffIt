using System;
using System.Linq;
using System.Text;

using Fluff.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.StringBuilderExtensionsTests
{
	[TestClass]
	public class GivenStringBuilderExtensions
	{
		[TestMethod]
		public void WhenLast_ThenLastChar()
		{
			var builder = new StringBuilder("abcd");

			Assert.AreEqual('d', builder.ToEnumerable().Last());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void WhenHeadWithMatchingPredicate_ThenLastCharIsRemoved()
		{
			var builder = new StringBuilder(string.Empty);

			builder.ToEnumerable().Last();
		}
	}
}

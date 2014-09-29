using System.Collections.Generic;

using Fluff.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.DictionaryExtensionsTests
{
	[TestClass]
	public class GivenDictionary
	{
		[TestMethod]
		public void WhenGetValueOrDefaultWithValue_ThenValue()
		{
			var sut = new Dictionary<int, double>();

			sut.Add(1, 5.6);

			var val = sut.GetOrDefault(1);

			Assert.AreEqual(5.6, val);
		}

		[TestMethod]
		public void WhenGetValueOrDefaultWithoutValue_ThenDefault()
		{
			var sut = new Dictionary<int, double>();

			sut.Add(1, 5.6);

			var val = sut.GetOrDefault(0);

			Assert.AreEqual(default(double), val);
		}
	}
}
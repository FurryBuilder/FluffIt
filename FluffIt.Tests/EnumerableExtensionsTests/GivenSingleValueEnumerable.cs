using System.Collections.Generic;
using System.Linq;

using Fluff.Extensions;
using Fluff.Patterns;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.EnumerableExtensionsTests
{
	[TestClass]
	public class GivenSingleValueEnumerable
	{
		private class FakeComparer : DefaultProvider<FakeComparer>, IEqualityComparer<int>
		{
			public bool Equals(int x, int y)
			{
				return x == y - 1;
			}

			public int GetHashCode(int obj)
			{
				return obj.GetHashCode();
			}
		}

		[TestMethod]
		public void WhenForEach_ThenLoop()
		{
			var count = 0;
			var list = new[] { 1 };

			list.ForEach(i => ++count);

			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void WhenNone_ThenFalse()
		{
			var list = new[] { 1 };

			var result = list.None();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void WhenNoneWithPredicate_ThenTrue()
		{
			var list = new[] { 1 };

			var result = list.None(i => i != 1);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void WhenFirstWithComparer_ThenMatch()
		{
			var list = new [] { 0 };

			var result = list.First(FakeComparer.Default, 1);

			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void WhenFirstOrDefaultWithComparerValid_ThenValue()
		{
			var list = new [] { 1 };

			var result = list.FirstOrDefault(FakeComparer.Default, 2);

			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void WhenFirstOrDefaultWithComparerInvalid_ThenDefault()
		{
			var list = new[] { 1 };

			var result = list.FirstOrDefault(FakeComparer.Default, 1);

			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void WhenFirstOrDefaultWithComparerAndDefaultValid_ThenValue()
		{
			var list = new[] { 1 };

			var result = list.FirstOrDefault(FakeComparer.Default, 2, () => 3);

			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void WhenFirstOrDefaultWithComparerAndDefaultInvalid_ThenDefault()
		{
			var list = new[] { 1 };

			var result = list.FirstOrDefault(FakeComparer.Default, 1, () => 3);

			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public void WhenDo_ThenRun()
		{
			var list = new [] { 0 };

			var values = string.Empty;

			list
				.Do(v => values += v + ":")
				.ForEach(v => values += v + ",");

			Assert.AreEqual("0:0,", values);
		}

		[TestMethod]
		public void WhenPrepend_ThenInsertFirst()
		{
			var list = new [] { 0 };

			var result = list.Prepend(-1).ToArray();

			Assert.AreEqual(2, result.Length);
			Assert.AreEqual(-1, result[0]);
			Assert.AreEqual(0, result[1]);
		}

		[TestMethod]
		public void WhenAppend_ThenInsertLast()
		{
			var list = new [] { 0 };

			var result = list.Append(1).ToArray();

			Assert.AreEqual(2, result.Length);
			Assert.AreEqual(0, result[0]);
			Assert.AreEqual(1, result[1]);
		}

		[TestMethod]
		public void WhenDistribute_ThenRunFirst()
		{
			var list = new [] { 0 };

			var isCalled = false;

			list.Distribute(_ => isCalled = true, _ => isCalled = false);

			Assert.IsTrue(isCalled);
		}

		[TestMethod]
		public void WhenDistributeWithOverflow_ThenRunFirst()
		{
			var list = new [] { 0 };

			var isCalled = false;

			list.DistributeWithOverflow(_ => isCalled = false, _ => isCalled = true, _ => isCalled = false);

			Assert.IsTrue(isCalled);
		}

		[TestMethod]
		public void WhenDistributeWithOverflowWithNoFunc_ThenRunFirst()
		{
			var list = new [] { 0 };

			var isCalled = false;

			list.DistributeWithOverflow(_ => isCalled = true);

			Assert.IsTrue(isCalled);
		}
	}
}

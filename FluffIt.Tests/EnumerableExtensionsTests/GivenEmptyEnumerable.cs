using System;
using System.Collections.Generic;
using System.Linq;

using Fluff.Extensions;
using Fluff.Patterns;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.EnumerableExtensionsTests
{
	[TestClass]
	public class GivenEmptyEnumerable
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
		public void WhenForEach_ThenSkip()
		{
			var list = new int[] { };

			list.ForEach(i => Assert.Fail());
		}

		[TestMethod]
		public void WhenNone_ThenTrue()
		{
			var list = new int[] { };

			var result = list.None();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void WhenNoneWithPredicate_ThenSkip()
		{
			var list = new int[] { };

			var result = list.None(i => false);

			Assert.IsTrue(result);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void WhenFirstWithComparer_ThenSkip()
		{
			var list = new int[] { };

			list.First(FakeComparer.Default, 0);
		}

		[TestMethod]
		public void WhenFirstOrDefaultWithComparer_ThenSkip()
		{
			var list = new int[] { };

			var result = list.FirstOrDefault(FakeComparer.Default, 0);

			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void WhenDo_ThenSkip()
		{
			var list = new int[] { };

			var isCalled = false;

			list
				.Do(_ => isCalled = true)
				.ForEach(_ => { });

			Assert.IsFalse(isCalled);
		}

		[TestMethod]
		public void WhenPrepend_ThenInsertFirst()
		{
			var list = new int[] { };

			var result = list.Prepend(0).ToArray();

			Assert.AreEqual(1, result.Length);
			Assert.AreEqual(0, result[0]);
		}

		[TestMethod]
		public void WhenAppend_ThenInsertLast()
		{
			var list = new int[] { };

			var result = list.Append(0).ToArray();

			Assert.AreEqual(1, result.Length);
			Assert.AreEqual(0, result[0]);
		}

		[TestMethod]
		public void WhenDistribute_ThenSkip()
		{
			var list = new int[] { };

			var isCalled = false;

			list.Distribute(_ => isCalled = true);

			Assert.IsFalse(isCalled);
		}

		[TestMethod]
		public void WhenDistributeWithOverflow_ThenSkip()
		{
			var list = new int[] { };

			var isCalled = false;

			list.DistributeWithOverflow(_ => isCalled = true, _ => isCalled = true);

			Assert.IsFalse(isCalled);
		}
	}
}

using Fluff.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.ObjectExtensionsTests
{
	[TestClass]
	public class GivenValidObject
	{
		[TestMethod]
		public void WhenDefaultNotNullInput_ThenDelegateNotCalled()
		{
			var s = "a";

			s.Default(() => { Assert.Fail(); return null; });
		}

		[TestMethod]
		public void WhenSelectOrDefaultValidInput_ThenSelectCalled()
		{
			var s = "a";

			s = s.SelectOrDefault(_ => "1");

			Assert.IsNotNull(s);
			Assert.AreEqual(s, "1");
		}

		[TestMethod]
		public void WhenSelectOrDefaultValidInputWithDefault_ThenSelectCalled()
		{
			var s = "a";

			s = s.SelectOrDefault(_ => "1", () => "2");

			Assert.IsNotNull(s);
			Assert.AreEqual(s, "1");
		}

		[TestMethod]
		public void WhenMaybeNonNullInput_ThenDelegateCalled()
		{
			const string s = "a";
			var validator = false;

			s.Maybe(_ => validator = true);

			Assert.IsTrue(validator);
		}

		[TestMethod]
		public void WhenAsNonNullInput_ThenDelegateCalled()
		{
			const string s = "a";
			var validator = false;

			s.As<object>(_ => validator = true);

			Assert.IsTrue(validator);
		}

		[TestMethod]
		public void WhenDoubleCheckedLocked_ThenCheckBeforeLock()
		{
			var validator = false;

			"a".DoubleCheckedLocked(s => s.Equals("a"), _ => validator = true);

			Assert.IsTrue(validator);
		}
	}
}

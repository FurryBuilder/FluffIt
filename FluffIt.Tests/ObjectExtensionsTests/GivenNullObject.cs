using Fluff.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluff.Tests.Extensions.ObjectExtensionsTests
{
	[TestClass]
	public class GivenNullObject
	{
		[TestMethod]
		public void WhenDefaultNullInput_ThenDelegate()
		{
			string s = null;

			s = s.Default(() => "1");

			Assert.AreEqual("1", s);
		}

		[TestMethod]
		public void WhenSelectOrDefaultNullInput_ThenDelegateNotCalled()
		{
			string s = null;

			s = s.SelectOrDefault(_ => "1");

			Assert.IsNull(s);
		}

		[TestMethod]
		public void WhenSelectOrDefaultNullInputWithDefault_ThenDefaultValueCalled()
		{
			string s = null;

			s = s.SelectOrDefault(_ => "1", () => "d");

			Assert.IsNotNull(s);
			Assert.AreEqual(s, "d");
		}

		[TestMethod]
		public void WhenMaybeNullInput_ThenDelegateNotCalled()
		{
			string s = null;

			s.Maybe(_ => Assert.Fail());
		}

		[TestMethod]
		public void WhenAsNullInput_ThenNull()
		{
			const string s = "a";

			Assert.IsNull(s.As<GivenNullObject>());
		}

		[TestMethod]
		public void WhenAsNullInput_ThenDelegateNotCalled()
		{
			const string s = "a";

			s.As<GivenNullObject>(_ => Assert.Fail());
		}
	}
}

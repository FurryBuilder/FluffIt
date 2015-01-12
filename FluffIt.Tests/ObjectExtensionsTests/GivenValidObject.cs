//////////////////////////////////////////////////////////////////////////////////
//
// The MIT License (MIT)
//
// Copyright (c) 2014 Furry Builder
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//////////////////////////////////////////////////////////////////////////////////

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluffIt.Tests.ObjectExtensionsTests
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
		public void WhenMaybeAsNonNullInput_ThenDelegateCalled()
		{
			const string s = "a";
			var validator = false;

			s.MaybeAs((object _) => validator = true);

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

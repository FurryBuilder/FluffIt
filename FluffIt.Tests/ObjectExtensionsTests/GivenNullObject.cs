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

			s.As((GivenNullObject _) => Assert.Fail());
		}
	}
}

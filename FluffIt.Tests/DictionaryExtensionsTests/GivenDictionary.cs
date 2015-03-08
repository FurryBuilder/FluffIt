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

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluffIt.Tests.DictionaryExtensionsTests
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
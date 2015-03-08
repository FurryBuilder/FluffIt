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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluffIt.Tests.EnumerableExtensionsTests
{
    [TestClass]
    public class GivenEmptyEnumerable
    {
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
        [ExpectedException(typeof (InvalidOperationException))]
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

        private class FakeComparer : IEqualityComparer<int>
        {
            public static readonly FakeComparer Default = new FakeComparer();

            public bool Equals(int x, int y)
            {
                return x == y - 1;
            }

            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}